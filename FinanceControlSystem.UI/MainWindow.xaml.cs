
using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Models;
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

        private void TabItemDashboard_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}