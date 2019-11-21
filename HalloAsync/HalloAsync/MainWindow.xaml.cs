using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bg;
        public MainWindow()
        {
            InitializeComponent();
            bg = new BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.ProgressChanged += Bg_ProgressChanged;
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            //      bg.RunWorkerAsync();
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Fertig");
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //UI Thread
            pb1.Value = e.ProgressPercentage;
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            //Thread
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                bg.ReportProgress(i);
            }
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(400);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            b.IsEnabled = false;
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(40);
                }
                b.Dispatcher.Invoke(() => b.IsEnabled = true);
            });
        }

        CancellationTokenSource cts;
        private void StartTaskmitTS(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            b.IsEnabled = false;

            cts = new CancellationTokenSource();
            var ts = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(40);
                    Task.Factory.StartNew(() => pb1.Value = i,
                        cts.Token, TaskCreationOptions.None, ts);

                    if (cts.IsCancellationRequested)
                    {
                        //clean up
                        break;
                    }
                }
                Task.Factory.StartNew(() => b.IsEnabled = true,
                   CancellationToken.None, TaskCreationOptions.None, ts);
            });
        }


        private void Abort(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                await Task.Delay(400);
            }
        }

        private async void StartAsyncAwaitSQL(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            var conString = "Server=.\\SQLEXPRESS;Database=Northwind;Trusted_Connection=true;";
            using var con = new SqlConnection(conString);

            await con.OpenAsync(cts.Token);

            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM EMPLOYEES;WAITFOR DELAY '0:0:10';";
            try
            {
                int count = (int)await cmd.ExecuteScalarAsync(cts.Token);
                MessageBox.Show($"{count} Employees in DB");
            }
            catch (Exception ex)
            {
                if (cts.IsCancellationRequested)
                    MessageBox.Show($"Erfolgreich abgebrochen");
                else
                    MessageBox.Show($"ERROR: {ex.Message}");
            }

        } //<- Dispose()

        private async void StartAlteLangsameMethode(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{await CalcZahlAsync(3)}");
        }

        public Task<long> CalcZahlAsync(int nummer)
        {
            return Task<long>.Run(() => CalcZahl(nummer));
        }

        public long CalcZahl(int nummer)
        {
            Thread.Sleep(5000);
            return nummer * 745734;
        }

    }
}
