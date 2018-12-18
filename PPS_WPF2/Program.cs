using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PPS_WPF
{
    public class Program
    {
        public static bool _leave = false;
        static void Main(string[] args)
        {
            int serverPort = 8888;
            IPAddress serverAddress = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = null;

            try
            {
                serverSocket = new TcpListener(serverAddress, serverPort);
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
                        Console.WriteLine("\n");

                        //do
                        //{

                        /* Thread du MaitreHotel */
                        /*Thread threadHotelMaster = new Thread(() => PPS_DLL.HotelMaster.Welcome());
                        threadHotelMaster.Start();
                        Thread.Sleep(5000);

                        /* Thread du Chef de cuisine */
                        /*Thread threadCookChief = new Thread(() => PPS_DLL.CookCHief.CallCook());
                        threadChef.Start();
                        Thread.Sleep(5000);

                        /* Thread du Cuisinier */
                        /*Thread.Sleep(5000);
                        Thread threadCook = new Thread(() => PPS_DLL.cook.Cook());
                        threadCook.Start();

                        /* Thread du Serveur */
                        /*Thread threadServer = new Thread(() => PPS_DLL.Server.TakeOrder());
                        threadHeadWaiter.Start();
                        Thread.Sleep(5000);

                        /* Thread du Client */
                        /*Thread.Sleep(5000);
                        Thread threadCustomer = new Thread(() => PPS_DLL.Customer.Pay());*/
                        // } while ();

                        Thread.Sleep(5000);
                        _leave = true;
                        Console.WriteLine("\n");
                        //Model.Resultat();
                        Console.ReadKey();
                    }
                    client.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Exception : {0}", error);
            }
            finally
            {
                serverSocket.Stop();
            }
        }
    }
}