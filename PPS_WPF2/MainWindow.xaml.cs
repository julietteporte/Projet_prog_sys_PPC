using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PPS_WPF
{
    public partial class MainWindow 
    {
        private BackgroundWorker thread;
        private NetworkStream serverStream = default(NetworkStream);
        private TcpClient clientSocket = new TcpClient();

        public BackgroundWorker Thread { get => Thread1; set => Thread1 = value; }
        public NetworkStream ServerStream { get => serverStream; set => serverStream = value; }
        public BackgroundWorker Thread1 { get => thread; set => thread = value; }

        public MainWindow()
        {
            InitializeComponent();

            Thread = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            

            clientSocket.Connect("127.0.0.1", 8888);
            ServerStream = clientSocket.GetStream();

            byte[] outStream = Encoding.ASCII.GetBytes("lancement");
            ServerStream.Write(outStream, 0, outStream.Length);
            ServerStream.Flush();

            Thread.DoWork += Thread_DoWork;
            Thread.RunWorkerCompleted += Thread_RunWorkerCompleted;

            Thread.RunWorkerAsync();
        }

        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            Byte[] bytes = new Byte[256];
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                ServerStream = clientSocket.GetStream();
                int i;
                while ((i = ServerStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(bytes, 0, i);
                    Dictionary<string, string> paramList = message.Split('&').Select(m => m.Split('|')).ToDictionary(m => m.FirstOrDefault(), m => m.Skip(1).FirstOrDefault());
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageAdd(new Message { Text = "Connexion terminée"});
        }

        private void MessageAdd(Message message)
        {
            throw new NotImplementedException();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}