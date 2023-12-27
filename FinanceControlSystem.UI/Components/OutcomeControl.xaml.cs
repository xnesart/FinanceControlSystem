using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
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
           
            //ComboBoxPaymentType.Items.Clear();
            List<string> financeTypesNames = GetListOfFinanceTypesForComboBoxAccountOfPayments();
            
            foreach(var financeTypeName in financeTypesNames)
            {
                //ComboBoxPaymentType.DataContext = financeType;

                ComboBoxPaymentType.Items.Add(financeTypeName);
            }
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

        private void ComboBoxPaymentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
