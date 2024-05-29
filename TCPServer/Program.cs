using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace TCPServer
{
    class Server
    {
        static Dictionary<int, List<TcpClient>> clientGroups = new Dictionary<int, List<TcpClient>>();
        static string password;
        static string ipAddress;
        static int port;
        static int bufferSize;
        static void LoadConfig(string filePath)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        if (parts[0] == "Password")
                            password = parts[1];
                        else if (parts[0] == "IP")
                            ipAddress = parts[1];
                        else if (parts[0] == "Port")
                            port = int.Parse(parts[1]);
                        else if (parts[0] == "BufferSize")
                            bufferSize = int.Parse(parts[1]);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Config file not found: {filePath}");
            }
        }
        static async Task Main(string[] args)
        {
            LoadConfig(Path.GetFullPath("Server.cfg"));
            TcpListener listener = new TcpListener(IPAddress.Parse(ipAddress), port);
            listener.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Task.Run(async () =>
                {
                    byte[] buffer = new byte[1];

                    using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                    {
                        try
                        {
                            await client.GetStream().ReadAsync(buffer, 0, buffer.Length, cts.Token);
                        }
                        catch (OperationCanceledException)
                        {
                            Console.WriteLine("Timeout expired while reading data from the network stream.");
                            client.Close();
                            return;
                        }
                    }

                    byte connectionType = buffer[0];

                    if (connectionType == 0)
                    {
                        Console.WriteLine("Client connected.");
                        _ = Task.Run(() => HandleClientAsync(client));
                    }
                    else if (connectionType == 1)
                    {
                        Console.WriteLine("Client command connected.");
                        _ = Task.Run(() => HandleClientCommandAsync(client));
                    }
                });
            }
        }

        static async Task HandleClientCommandAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            byte[] receivedPasswordBytes = new byte[password.Length];
            int bytesRead = await stream.ReadAsync(receivedPasswordBytes, 0, receivedPasswordBytes.Length);
            string receivedPassword = Encoding.UTF8.GetString(receivedPasswordBytes);
            try
            {
                if (receivedPassword == password)
                {

                    while (true)
                    {
                        byte[] commandBuffer = new byte[1024];
                        int commandBytesRead = await stream.ReadAsync(commandBuffer, 0, commandBuffer.Length);
                        string command = Encoding.UTF8.GetString(commandBuffer, 0, commandBytesRead).Trim().ToLower();
                        if (commandBytesRead == 0)
                        {
                            Console.WriteLine("Client command disconnected.");
                            return;
                        }

                        if (command.StartsWith("getgroupmembercount"))
                        {
                            string[] parts = command.Split(' ');
                            byte[] buffer;

                            if (parts.Length > 2) 
                            {
                                if (int.TryParse(parts[1], out int groupNumber))
                                {
                                    if (clientGroups.Count > groupNumber)
                                    {
                                        if (clientGroups[groupNumber] != null)
                                        {
                                            buffer = Encoding.UTF8.GetBytes(clientGroups[groupNumber].Count.ToString());
                                            await stream.WriteAsync(buffer, 0, buffer.Length);
                                            continue;
                                        }
                                    }
                                }
                            }
                     
                            buffer = Encoding.UTF8.GetBytes("-1");
                            await stream.WriteAsync(buffer, 0, buffer.Length);
                        }


                    }
                }
                else
                {
                    Console.WriteLine($"Client command provided incorrect password. Disconnecting. {receivedPassword}");
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client command error: {ex.Message}");
                Console.WriteLine("Client command disconnecting.");
                client.Close();
            }

        }

        static async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            byte[] receivedPasswordBytes = new byte[password.Length];
            int bytesRead = await stream.ReadAsync(receivedPasswordBytes, 0, receivedPasswordBytes.Length);
            string receivedPassword = Encoding.UTF8.GetString(receivedPasswordBytes);

            if (receivedPassword == password)
            {
                byte[] groupBuffer = new byte[4];
                bytesRead = await stream.ReadAsync(groupBuffer, 0, groupBuffer.Length);
                int groupNumber = BitConverter.ToInt32(groupBuffer, 0);
                Console.WriteLine($"Client joined group {groupNumber}");

                if (!clientGroups.ContainsKey(groupNumber))
                {
                    clientGroups[groupNumber] = new List<TcpClient>();
                    _ = Task.Run(() => HandleGroupAsync(groupNumber));
                }
                clientGroups[groupNumber].Add(client);

            }
            else
            {
                Console.WriteLine("Client provided incorrect password. Disconnecting.");
                client.Close();
            }
        }

        static async Task HandleGroupAsync(int groupNumber)
        {
            int i = 0;
            while (true)
            {
                if (clientGroups[groupNumber].Count == 0)
                {
                    clientGroups.Remove(groupNumber);
                    Console.WriteLine($"Group {groupNumber} removed.");
                    return;
                }
                try
                {  
                    for (; i < clientGroups[groupNumber].Count; i++)
                    {
                        var client = clientGroups[groupNumber][i];
                        NetworkStream stream = client.GetStream();
                        byte[] buffer = new byte[1];
                        int bytesRead = 1;
                        Task.Run(async () =>
                        {
                            while (true)
                            {
                                buffer = new byte[bufferSize];
                                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                                if (bytesRead == 0)
                                {
                                    Console.WriteLine("Client disconnected.");
                                    clientGroups[groupNumber].Remove(client);
                                    i--;
                                    return;
                                }

                                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                Console.WriteLine($"Received message from group {groupNumber}: {message}");

                                foreach (var groupClient in clientGroups[groupNumber])
                                {
                                    if (groupClient != client)
                                    {
                                        NetworkStream groupStream = groupClient.GetStream();
                                        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                                        await groupStream.WriteAsync(messageBytes, 0, messageBytes.Length);
                                    }
                                }
                                await Task.Delay(100);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                await Task.Delay(500);
            }
        }
    }

}
