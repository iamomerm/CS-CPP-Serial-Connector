using System;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace SerialConnector
{
    [Guid("5B0AC0AF-C14F-4DC0-929E-DC78C6054AC6")]
    public interface SCInterface
    {
        int Connect(string Port, int Baudrate);
        int Write(string Data);
        string Read();
        int Disconnect();
    }
}

namespace SerialConnector
{
    [Guid("FE015E82-B17A-4250-9F0A-8ECFB233CCF7")]
    public class SC : SCInterface
    {
        private SerialPort SerPort;
        public int Connect(string Port, int Baudrate)
        {
            Console.WriteLine("Connecting to Port: {0}, Baudrate: {1}...", Port, Baudrate);

            try
            {
		Port = "COM" + Port
                SerPort = new SerialPort(Port, Baudrate, Parity.None, 8, StopBits.One);
                SerPort.Handshake = Handshake.None;
                SerPort.WriteTimeout = 500;
                SerPort.Open();
                return 0;
            }

            catch (Exception Ex) 
            {
                Console.WriteLine("[Connect] Error: " + Ex.Message);
                return -1; 
            }
        }

        public int Write(string Data)
        {
            try
            {
                SerPort.Write(Data);
                return 0;
            }

            catch (Exception Ex) 
            {
                Console.WriteLine("[Write] Error: " + Ex.Message);
                return -1; 
            }
        }

        public string Read()
        {
            try { return SerPort.ReadExisting(); }
            catch (Exception Ex)
            {
                Console.WriteLine("[Read] Error: " + Ex.Message);
                return "NULL"; 
            }
        }

        public int Disconnect()
        {
            try { SerPort.Close(); return 0; }
            catch (Exception Ex)
            {
                Console.WriteLine("[Disconnect] Error: " + Ex.Message);
                return -1; 
            }
        }
    }
}
