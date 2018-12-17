using Console_NET_Project.Model;
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
    public class MainKevin
    {
        public static bool _leave = false;
        static void Main(string[] args)
        {
            int serverPort = 8888;
            IPAddress serverAddress = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = null;
            RestaurantModel restaurant;

            try
            {
                serverSocket = new TcpListener(serverAddress, serverPort);

                // Start the socket
                serverSocket.Start();

                // BUFFER FOR READING DATA
                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    TcpClient client = serverSocket.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // CONVERT DATA BYTES TO ASCII STRING.
                        data = Encoding.ASCII.GetString(bytes, 0, i);

                        /* 
                         * PARTIE CONTROLEUR 
                        */

                        int nbGroupCustomer = Int32.Parse(data);
                        restaurant = new RestaurantModel(8, 2, 3, 2);
                        restaurant.AfficherGroup(8);
                        restaurant.AfficherHeadWaiter(2);
                        restaurant.AfficherCook(3);
                        restaurant.AfficherWaiter(2);
                        Console.WriteLine("\n");
                        do
                        {
                            /* Thread du MaitreHotel */
                            Thread threadMaitreHotel = new Thread(() => restaurant.maitreHotel.PlaceCustomer(restaurant.listTable, restaurant.listGroupCustomerWaiting));
                            threadMaitreHotel.Start();

                            Thread.Sleep(5000);
                            /* Thread du HeadWaiter */
                            Thread threadHeadWaiter = new Thread(() => restaurant.headWaiter.TakeOrder(restaurant.listCommand));
                            threadHeadWaiter.Start();

                            //Thread.Sleep(10000);
                            /* Thread du Chef */
                            Thread.Sleep(5000);
                            Thread threadChef = new Thread(() => restaurant.chef.DispatchTask(restaurant.listCook, restaurant.listCommand, restaurant.listGroupCustomerWaiting));
                            threadChef.Start();

                            /* Thread du Cuisinier */
                            Thread.Sleep(5000);
                            Thread threadCook = new Thread(() => restaurant.cook.Cooking(restaurant.listCommand, restaurant.listCook, restaurant.listGroupCustomerWaiting));
                            threadCook.Start();

                            /* Thred du Waiter */
                            Thread.Sleep(5000);
                            Thread threadWaiter = new Thread(() => restaurant.waiter.PutSomething(restaurant.listCommand, restaurant.listTable));
                            threadWaiter.Start();
                        } while (restaurant.listGroupCustomerWaiting.Count != 0);

                        Thread.Sleep(5000);
                        _leave = true;
                        Console.WriteLine("\n");
                        restaurant.Resultat();
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