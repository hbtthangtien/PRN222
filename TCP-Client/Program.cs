using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string serverAddress = "127.0.0.1";
            const int port = 8080;
            ConnectServer(serverAddress, port);
            

        }
        static void ConnectServer(string serverAddress, int port)
        {
            string message, responseData;
            int bytes;
            try
            {
                TcpClient tcpClient = new TcpClient(serverAddress, port);
                Console.Title = "Client Application";
                NetworkStream stream = null;
                while (true)
                {
                    Console.WriteLine("Input message<press Enter to exit>: ");
                    message = Console.ReadLine();
                    if(message == string.Empty)
                    {
                        break;
                    }
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message+"");
                    stream = tcpClient.GetStream();
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Sent: {message}");
                    data = new byte[256];
                    bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine($"Received: {responseData}");
                }
                tcpClient.Close();
            }catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
