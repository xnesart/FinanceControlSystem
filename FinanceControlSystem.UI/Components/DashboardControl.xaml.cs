using FinanceControlSystem.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl
    {
        private DataStorage _dataStorage;
        public DashboardControl()
        {
            InitializeComponent();
            _dataStorage = new DataStorage();
            _dataStorage = DataStorage.LoadFromJson();
            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }
            ShowAmountOfRub();
            ShowOutcome();
        }


        private void ShowAmountOfRub()
        {
            if(_dataStorage != null)
            {
                LabelDashboardCountOfRubValue.Content = _dataStorage.GetBalanceFromDebetAndCashModels();
            }
            
        }
        private void ShowOutcome()
        {
            if (_dataStorage != null)
            {
                LabelDashboardOutcomeValue.Content = _dataStorage.CalculateOutcome();

            }
        }

        private void ShowIncome()
        {
            if (_dataStorage != null)
            {
                LabelDashboardIncomeValue.Content = _dataStorage.CalculateIncome();
            }
        }

        private void ShowDebt()
        {
            if (_dataStorage != null)
            {
                LabelDashboardDebtValue.Content = _dataStorage.CalculateDebt();

            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            _dataStorage = DataStorage.LoadFromJson();
            ShowAmountOfRub();
            ShowOutcome();
        }
        public void GetUpdate()
        {
            _dataStorage = DataStorage.LoadFromJson();
            ShowAmountOfRub();
            ShowOutcome();
            ShowIncome();
            ShowDebt();
        }
    }
}
