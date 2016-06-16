using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class StateObject
    {
        public const int BufferSize = 2024;
        public byte[] Buffer = new byte[BufferSize];
        public int Position;
    }
}