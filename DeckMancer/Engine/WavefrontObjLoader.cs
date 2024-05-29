using System.Linq;
using Assimp;
using OpenTK.Mathematics;

namespace DeckMancer.Engine
{
    public class WavefrontObjLoader
    {
        public static void ExportToObj(string filePath, float[] vert, int[] indices, float[] texCoord, float[] normal)
        {
            Vector3D[] vertices = Enumerable.Range(0, vert.Length / 3)
              .Select(i => new Vector3D(vert[i * 3], vert[i * 3 + 1], vert[i * 3 + 2]))
              .ToArray();

            Vector3D[] normals = Enumerable.Range(0, normal.Length / 3)
                .Select(i => new Vector3D(normal[i * 3], normal[i * 3 + 1], normal[i * 3 + 2]))
                .ToArray();

            Vector2D[] texCoords = Enumerable.Range(0, texCoord.Length / 2)
                .Select(i => new Vector2D(texCoord[i * 2], texCoord[i * 2 + 1]))
                .ToArray();

            Scene scene = new Scene();
            scene.RootNode = new Node("Root");

            Mesh triangle = new Mesh();

            Face[] faces = Enumerable.Range(0, indices.Length / 3)
           .Select(i => indices.Skip(i * 3).Take(3).ToArray())
           .Select(arr => new Face(arr))
           .ToArray();

            triangle.Vertices.AddRange(vertices);
            triangle.Faces.AddRange(faces);
            triangle.MaterialIndex = 0;
            triangle.Normals.AddRange(normals);
            triangle.TextureCoordinateChannels[0].AddRange(texCoords.Select(tc => new Vector3D(tc.X, tc.Y, 0.0f)));

            scene.Meshes.Add(triangle);
            scene.RootNode.MeshIndices.Add(0);

            Material mat = new Material();
            mat.Name = "None";
            scene.Materials.Add(mat);

            AssimpContext context = new AssimpContext();
            context.ExportFile(scene, filePath, "obj");
        }
     
        public static void ImportFromObj(string filePath, out Vector3[] vertices, out uint[] indices, out Vector2[] texCoords)
        {
            AssimpContext context = new AssimpContext();
            Scene scene = context.ImportFile(filePath, PostProcessSteps.Triangulate );
            ExportFormatDescription[] exportDescs = context.GetSupportedExportFormats();
            if (scene != null && scene.HasMeshes)
            {
                Mesh mesh = scene.Meshes[0];

                vertices = mesh.Vertices.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
                int[] intIndices = mesh.Faces.SelectMany(face => face.Indices).ToArray();
                indices = intIndices.Select(index => (uint)index).ToArray();
                if (mesh.HasTextureCoords(0))
                    texCoords = mesh.TextureCoordinateChannels[0].Select(tc => new Vector2(tc.X, tc.Y)).ToArray();
                else
                    texCoords = null;
            }
            else
            {
                vertices = null;
                indices = null;
                texCoords = null;
            }
            
        }
    }
}
