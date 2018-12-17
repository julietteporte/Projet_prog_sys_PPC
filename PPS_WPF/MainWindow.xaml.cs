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

        private void Start_Simulation_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Nb_Clients_Choice.Text, out Nb_Clients)
                && int.TryParse(Nb_Waiters_Choice.Text, out Nb_Waiters)
                && int.TryParse(Nb_Cooks_Choice.Text, out Nb_Cooks)
                && int.TryParse(Nb_Waiters_Choice.Text, out Nb_Waiters)
                && (Scenario_Choice.SelectedIndex >= 0))
            {

            }
            else
            {
                Nb_Clients_Random_Click(sender, e);
                Nb_Waiters_Random_Click(sender, e);
                Nb_Cooks_Random_Click(sender, e);
                Nb_Headwaiters_Random_Click(sender, e);
            }

            Parametres.NumberClients = int.Parse(Nb_Clients_Choice.Text);
            Parametres.NumberWaiters = int.Parse(Nb_Waiters_Choice.Text);
            Parametres.NumberCooks = int.Parse(Nb_Cooks_Choice.Text);
            Parametres.NumberHeadwaiters = int.Parse(Nb_Waiters_Choice.Text);
            Parametres.NumberScenario = Scenario_Nb;

            /*
             * PARAMETRE A METTRE EN PLACE
             * */
            //Parametres.NumberGroupCustomer = int.Parse(Nb_GroupCustomer.Text);

            Results results = new Results();
            results.Show();
            MainWindow1.IsEnabled = false;
        }

        private void Nb_Clients_Random_Click(object sender, RoutedEventArgs e)
        {
            Nb_Clients_Choice.Text = nb_random.Next(1, 12).ToString();
        }

        private void Nb_Waiters_Random_Click(object sender, RoutedEventArgs e)
        {
            Nb_Waiters_Choice.Text = nb_random.Next(1, 12).ToString();
        }

        private void Nb_Cooks_Random_Click(object sender, RoutedEventArgs e)
        {
            Nb_Cooks_Choice.Text = nb_random.Next(1, 12).ToString();
        }

        private void Check_Int_Validity(string Entered_Value, int Destination_Value)
        {
            if (int.TryParse(Entered_Value, out Destination_Value))
            {
            }
            else
            {
                MessageBox.Show("Veuillez saisir un nombre valide entre 1 et 100.");
            }
        }

        private void Nb_Clients_Choice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Check_Int_Validity(Nb_Clients_Choice.Text, Nb_Clients);
            }
        }

        private void Nb_Waiters_Choice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Check_Int_Validity(Nb_Waiters_Choice.Text, Nb_Waiters);
            }
        }

        private void Nb_Cooks_Choice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Check_Int_Validity(Nb_Cooks_Choice.Text, Nb_Cooks);
            }
        }

        private void Scenario_Random_Click(object sender, RoutedEventArgs e)
        {
            int nb_scenario = 0;
            foreach (var scenario in Scenario_Choice.Items)
            {
                nb_scenario += 1;
            }
            Scenario_Nb = nb_random.Next(0, nb_scenario);
            Scenario_Choice.SelectedIndex = Scenario_Nb;
        }

        private void MainWindow1_Closed(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void Scenario_Choice_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Scenario_Nb = Scenario_Choice.SelectedIndex;
        }

        private void Nb_Headwaiters_Random_Click(object sender, RoutedEventArgs e)
        {
            Nb_Headwaiters_Choice.Text = nb_random.Next(1, 12).ToString();
        }

        private void Nb_Headwaiters_Choice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Check_Int_Validity(Nb_Headwaiters_Choice.Text, Nb_Headwaiters);
            }
        }
    }
}