using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Gurock.SmartInspect;

namespace Carlink
{
    class SocketClient
    {
        private string _ip;
        private int _port;
        const int LENGTH_TO_CUT = 4;
        private Socket _sender;
        public SocketClient(string ipAddress, int port)
        {
            _sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            _ip = ipAddress;
            _port = port;

        }

        public void Write(byte[] dataBytes)
        {
            try
            {
                byte[] mySendBytes = data_append_dataLength(dataBytes);
                Console.WriteLine(mySendBytes.Length);
                Console.WriteLine(mySendBytes[0]+" "+mySendBytes[1]);
                _sender.Connect(_ip,_port);
                _sender.Send(mySendBytes);
                _sender.Shutdown(SocketShutdown.Both);
                _sender.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                SiAuto.Main.LogException(ex);
            }
        }

        byte[] data_append_dataLength(byte[] byteArray)
        {
            //byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
            //byte[] data_length = int_to_hex_little_endian(byteArray.Length);
            byte[] data_length = System.BitConverter.GetBytes(UInt16.Parse(byteArray.Length.ToString()));
            Console.WriteLine("data_length.Length-" + data_length.Length);
            byte[] rv = new byte[data_length.Length + byteArray.Length];
            System.Buffer.BlockCopy(data_length, 0, rv, 0, data_length.Length);
            System.Buffer.BlockCopy(byteArray, 0, rv, data_length.Length, byteArray.Length);
            //SiAuto.Main.AddCheckpoint("original length:" + byteArray.Length);
            //SiAuto.Main.AddCheckpoint("data_append_dataLength length:"+rv.Length);
            return rv;
        }
        byte[] int_to_hex_little_endian(int length)
        {
            var reversedBytes = System.Net.IPAddress.NetworkToHostOrder(length);
            string hex = reversedBytes.ToString("x");
            string trimmed = hex.Substring(0, LENGTH_TO_CUT);
            //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            //Byte[] bytes = encoding.GetBytes(trimmed);
            byte[] bytes = StringToByteArray(trimmed);
            //string str = System.Text.Encoding.ASCII.GetString(bytes);
            return bytes;
            //return HexAsciiConvert(trimmed);
        }
        byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
