using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPS_IHM
{
    public partial class Start : Form
    {
        private BackgroundWorker thread;
        NetworkStream serverStream = default(NetworkStream);
        TcpClient clientSocket = new TcpClient();

        public Start()
        {
            InitializeComponent();
            thread = new BackgroundWorker();
            thread.WorkerReportsProgress = true;
            thread.WorkerSupportsCancellation = true;
            thread.DoWork += Thread_DoWork;
            thread.ProgressChanged += Thread_ProgressChanged;
            thread.RunWorkerCompleted += Thread_RunWorkerCompleted;

            MessageAdd(new Message { Text = "Connexion au serveur...", Color = Brushes.White });
            clientSocket.Connect("127.0.0.1", 8888);
            serverStream = clientSocket.GetStream();

            byte[] outStream = Encoding.ASCII.GetBytes("lancement");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            thread.RunWorkerAsync();
        }

        private void MessageAdd(Message message)
        {
            throw new NotImplementedException();
        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
