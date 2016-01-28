using PaymentKiosk.Core.Domain;
using PaymentKiosk.Core.Services;
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

namespace PaymentKiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer
            {
                Name = textBoxCustName.Text,
                Telephone = textBoxCustTel.Text
            };

            var creditCard = new CreditCard
            {
                CardNum = textBoxCCNum.Text,
                Expiration = textBoxExpiration.Text
            };

            try
            {
                bool success = MoneyService.Charge(customer, creditCard, decimal.Parse(textBoxAmount.Text));
                if (success)
                {
                    var message = "Payment Successful";
                    MessageBox.Show(message);
                    SmsService.SendSms(textBoxCustTel.Text, message);
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
                SmsService.SendSms(textBoxCustTel.Text, f.Message);
            }
        }
    }
}
