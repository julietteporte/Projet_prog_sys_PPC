using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PPS_Sockets
{
    class Program
    {
        static void Main(string[] args)
        {
            int serverPort = 8888;
            IPAddress serverAddress = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = null;

            try
            {
                serverSocket = new TcpListener(serverAddress, serverPort);

                // START SOCKET
                serverSocket.Start();
                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    TcpClient client = serverSocket.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        byte[] msg;
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("IN: {0}", data);

                        string messageRetour = "cc";
                        stream.Write(Encoding.ASCII.GetBytes(messageRetour), 0, messageRetour.Length);

                    }
                    client.Close();
                    Console.WriteLine("cc le client");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Exception: {0}", error);
            }
            finally
            {
                serverSocket.Stop();
            }
        }


    }
}

