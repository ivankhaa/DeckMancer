using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DeckMancer.Core
{
    public class ClientServer
    {
        private TcpClient client;
        private NetworkStream stream;
        private TcpClient commandClient;
        private NetworkStream commandStream;
        private string ipAddress;
        private int port;
        private string password;
        private int groupNumber;

        public ClientServer(string ipAddress, int port, string password, int groupNumber)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.password = password;
            this.groupNumber = groupNumber;
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(ipAddress, port);
                stream = client.GetStream();

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] groupBytes = BitConverter.GetBytes(groupNumber);

                await stream.WriteAsync(new byte[] { 0 }, 0, 1);
                await stream.WriteAsync(passwordBytes, 0, passwordBytes.Length);
                await stream.WriteAsync(groupBytes, 0, groupBytes.Length);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> CommandConnectAsync()
        {
            try
            {
                commandClient = new TcpClient();
                await commandClient.ConnectAsync(ipAddress, port);
                stream = client.GetStream();

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                await stream.WriteAsync(new byte[] { 1 }, 0, 1);
                await commandStream.WriteAsync(passwordBytes, 0, passwordBytes.Length);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error command connecting to server: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> SendAsync(string message, bool commandStream = false)
        {
            NetworkStream stream;
            try
            {
                if (commandStream)
                    stream = this.commandStream;
                else
                    stream = this.stream;
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                return false;
            }
        }

        public async Task<string> ReceiveAsync(bool commandStream = false)
        {
            NetworkStream stream;
            try
            {
                if (commandStream)
                    stream = this.commandStream;
                else
                    stream = this.stream;
                byte[] buffer = new byte[16384];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                return receivedMessage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving message: {ex.Message}");
                return null;
            }
        }
        public async Task<int?> GetGroupMemberCountAsync(int group)
        {
            if (await SendAsync($"getgroupmembercount {group}", true))
            {
                var message = await ReceiveAsync(true);
                if (message != null)
                {
                    if (int.TryParse(message, out int groupNumber))
                    {
                        return groupNumber;
                    }
                }
            }
            return null;
        }

        public void Disconnect()
        {
            if (client != null)
            {
                stream.Close();
                client.Close();
            }
        }
        public void DisconnectCommand()
        {
            if (client != null)
            {
                commandStream.Close();
                commandClient.Close();
            }
        }
    }
}
