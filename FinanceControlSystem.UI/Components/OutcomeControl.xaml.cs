using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System.Windows;
using System.Windows.Controls;

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for OutcomeControl.xaml
    /// </summary>
    public partial class OutcomeControl : UserControl
    {
        private DataStorage _dataStorage;
        public OutcomeControl()
        {
            InitializeComponent();
            _dataStorage = new DataStorage();
            _dataStorage = DataStorage.LoadFromJson();
            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }
            FillComboBoxAccountOfPayments();
        }
        private void FillComboBoxAccountOfPayments()
        {

            ComboBoxPaymentType.Items.Clear();

            List<string> financeTypesNames = GetListOfFinanceTypesForComboBoxAccountOfPayments();
            ComboBoxPaymentType.ItemsSource = financeTypesNames;
        }



        private void ButtonAddOutcome_Click(object sender, RoutedEventArgs e)
        {
            string outcomeCategory = "";
            string accountOfOutcome = "";
            decimal outcomeValue = decimal.Parse(TextBoxOutcomeValue.Text);
            if (ComboBoxCategoryType.SelectedItem is null || ComboBoxPaymentType.SelectedItem is null)
            {
                ComboBoxCategoryType.SelectedIndex = 1;
                ComboBoxPaymentType.SelectedIndex = 1;
            }
            else
            {
                outcomeCategory = ComboBoxCategoryType.SelectedItem.ToString();
                accountOfOutcome = ComboBoxPaymentType.SelectedItem.ToString();
            }

            string descriptionCategory = TextBoxOutcomeDesciption.Text;
            ListViewOutcome.Items.Add(new ItemForAddIntoListView { Outcome = outcomeValue.ToString(), Category = outcomeCategory, Account = accountOfOutcome, Description = descriptionCategory });
            TransactionModel transaction = new TransactionModel()
            {
                Name = descriptionCategory,
                Type = TransactionType.Outcome,
               // Account = (ClientsFinanceType)ComboBoxPaymentType.SelectedItem,
                Summ = outcomeValue,
                
            };

            //Column1 data1 = new Column1 { Column1 = $"{outcomeValue}", Column2 = $"{outcomeCategory}" };
            //OutcomeControl data2 = new OutcomeControl { Column2 = $"{outcomeValue}", Column2 = $"{outcomeCategory}" };
            //ListViewOutcome.Items.Clear();
            //ListViewOutcome.Items.Add(data1);

        }
        private void ComboBoxPaymentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private List<string> GetListOfFinanceTypesForComboBoxAccountOfPayments()
        {
            List<string> financeTypesNames = new List<string>();
            List<ClientsFinanceModel> clients = _dataStorage.GetAllClientModels();

            foreach (ClientsFinanceModel client in clients)
            {
                financeTypesNames.Add(client.Name);
            }

            return financeTypesNames;
        }

        private void ButtonAddOutcome_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
    public class ItemForAddIntoListView
    {
        public string Outcome { get; set; }
        public string Category { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
    }
}
