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
using PPS_DLL;
using PPS_DLL.Service;

namespace PPS_WPF
{
    public partial class MainWindow : Window
    {
        private BackgroundWorker thread;
        private NetworkStream serverStream = default(NetworkStream);
        private TcpClient clientSocket = new TcpClient();

        public MainWindow()
        {
            InitializeComponent();

            thread = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            thread.DoWork += Thread_DoWork;
            thread.ProgressChanged += Thread_ProgressChanged;
            thread.RunWorkerCompleted += Thread_RunWorkerCompleted;

            clientSocket.Connect("127.0.0.1", 8888);
            serverStream = clientSocket.GetStream();

            byte[] outStream = Encoding.ASCII.GetBytes("lancement");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            thread.RunWorkerAsync();

            List<Business.Scenario> scenarioList = Scenario.select();
            ScenarioChoice.ItemsSource = scenarioList;
            ScenarioChoice.SelectedItem = scenarioList.First();
            //Scenario_Choice.SelectedItem = scenarioList.Last();
            //Scenario_Choice.SelectedItem = scenarioList.Where(i => i.id == 2).FirstOrDefault();
            ScenarioChoice.DisplayMemberPath = "Name";
        }

        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            Byte[] bytes = new Byte[256];
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                serverStream = clientSocket.GetStream();
                int i;
                while ((i = serverStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(bytes, 0, i);
                    Dictionary<string, string> paramList = message.Split('&').Select(m => m.Split('|')).ToDictionary(m => m.FirstOrDefault(), m => m.Skip(1).FirstOrDefault());

                    worker.ReportProgress(0, new Message { Text = "Lancement de l'affichage des résultats...", Color = Brushes.White });
                }
                Thread.Sleep(1000);
            }
        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageAdd(new Message { Text = "Connexion au serveur terminée.", Color = Brushes.White/*, Author = "Client"*/ });
        }

        private void Thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Message message = e.UserState as Message;
            MessageAdd(new Message { Text = message.Text, Color = message.Color, Author = message.Author });
        }

        private void MessageAdd(Message message)
        {
            Run newLine = new Run("> " /*+ DateTime.Now.ToString("T") + " ["*/ + message.Author + /*"] " +*/ message.Text + "\n") { Foreground = message.Color };
            messageBox.Inlines.InsertBefore(messageBox.Inlines.FirstInline, newLine);
        }

        private void Start1_Closed(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void ScenarioChoiceSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Scenario_Nb = ScenarioChoice.SelectedIndex;
        }
    }
}