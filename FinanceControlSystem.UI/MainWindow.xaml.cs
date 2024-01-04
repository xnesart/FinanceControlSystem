
using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Models;
using FinanceControlSystem.UI.Components;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace FinanceControlSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountClient _accountClient;
        private DataStorage _dataStorage;

        public MainWindow()
        {
            InitializeComponent();
            _dataStorage = new DataStorage();
            _dataStorage = DataStorage.LoadFromJson();
            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }
            _accountClient = new AccountClient();
            
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _dataStorage = DataStorage.LoadFromJson();
            if (File.Exists("DataStorageVault.json"))
            {
                DashboardTab.GetUpdate();
                Outcome.GetUpdate();
                Income.GetUpdate();
            }
        }
    }
}