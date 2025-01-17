using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCP_Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            const int port = 8080;
            string host = "127.0.0.1";
            ExecuteServer(host, port);

        }
        static void ProcessMessage(object parm)
        {
            string data;
            int count;
            try
            {
                TcpClient? Client = parm as TcpClient;
                IPEndPoint ip = Client.Client.RemoteEndPoint as IPEndPoint;
                Byte[] bytes = new Byte[256];
                NetworkStream stream = Client.GetStream();
                Console.WriteLine($"Connected by [{ip.Address}:{ip.Port}]");
                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes);
                    Console.WriteLine($"Received by [{ip.Address}:{ip.Port}]: {data} at {DateTime.Now:t}");
                    data = $"{data.ToUpper()}";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine($"Sent to [{ip.Address}:{ip.Port}]: {data} at {DateTime.Now:t}");
                }
                Client.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                Console.WriteLine("Waiting message...");
            }
        }
        static void ExecuteServer(string host, int port)
        {
            int Count = 0;
            TcpListener server = null;
            try
            {
                Console.Title = "Server Application";
                IPAddress address = IPAddress.Parse(host);
                server = new TcpListener(address, port);
                server.Start();
                Console.WriteLine(new string('*', 40));
                Console.WriteLine("Waiting for a connection... ");
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine($"Number of client connected: {++Count}");
                    Console.WriteLine(new string('*', 40));
                    Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                    thread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
            finally
            {
                server.Stop();
                Console.WriteLine("Server stop! Please any key to exit");
            }
            Console.Read();
        }
    }
}
