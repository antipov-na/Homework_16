using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Homework_16_1
{
    class GenerateClientsViewModel : ICloseRequest
    {
        private RelayCommand generateCommand;
        private RelayCommand cancelCommand;
        private int individualAmount;
        private int legalAmount;


        public RelayCommand GenerateCommand
        {
            get
            {
                return this.generateCommand ??
                  (this.generateCommand = new RelayCommand(obj =>
                  {
                      Stopwatch stopwatch = new Stopwatch();
                      stopwatch.Start();
                      MainWindowViewModel.Bank.GenerateClients(this.individualAmount, this.legalAmount);
                      stopwatch.Stop();
                      MessageBox.Show($"{stopwatch.ElapsedMilliseconds} мс.");
                      (obj as Window).Close();
                  }));
            }
        }

        public int LegalAmount
        {
            get
            {
                return this.legalAmount;
            }
            set
            {
                legalAmount = value;
            }
        }

        public int IndividualAmount
        {
            get
            {
                return this.individualAmount;
            }
            set
            {
                this.individualAmount = value;
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return this.cancelCommand ??
                  (this.cancelCommand = new RelayCommand(obj =>
                  {
                      (obj as Window).Close();
                      
                  }));
            }
        }

        public event EventHandler<CloseRequestArgs> CloseRequest;
    }
}
