using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Common.Net.Commands;

namespace Common.Net
{
    public static class SocketParser
    {
        public static bool TryDeserialize(byte[] buffer, int bytesRead, out CommandBase command)
        {
            MemoryStream stream = new MemoryStream(buffer, 0, bytesRead);

            var intSize = sizeof(Int32);

            byte[] sizeb = new byte[intSize];

            stream.Read(sizeb, 0, intSize);

            Int32 size = BitConverter.ToInt32(sizeb, 0);

            stream.Position = intSize;

            BinaryFormatter formatter = new BinaryFormatter();

            command = formatter.Deserialize(stream) as CommandBase;

            return true;
        }

        public static bool  TrySerialize(CommandBase command, out byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream objStream = new MemoryStream();
            formatter.Serialize(objStream, command);
            var intSize = sizeof(Int32);
            var size = (int) objStream.Position;
            byte[] sizeb = BitConverter.GetBytes(size);

            MemoryStream outStream = new MemoryStream();
            outStream.Write(sizeb, 0, intSize);
            outStream.Write(objStream.ToArray(),0, (int)objStream.Length);

            data = outStream.ToArray();

            return true;
        }
    }
}