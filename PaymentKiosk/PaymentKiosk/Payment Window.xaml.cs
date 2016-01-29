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
    /// Interaction logic for Payment_Window.xaml
    /// </summary>
    public partial class Payment_Window : Window
    {
        decimal origtotal;
        public Payment_Window(decimal amount)
        {
            InitializeComponent();
            origtotal = amount;
            textBoxAmount.Text = origtotal.ToString();
            textBoxAmount.IsReadOnly = true;
            radioButton0.IsChecked = true;
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
                    MainWindow.SuccessfulTransaction();
                    SmsService.SendSms(textBoxCustTel.Text, "Payment Successful: $"+textBoxAmount.Text);
                    this.Close();
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message+"\n\n Please retry the transaction");
                SmsService.SendSms(textBoxCustTel.Text, f.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner = null;
        }

        private void radioButton10_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton0.IsChecked == true) { radioButton0.IsChecked = false; }
            if (radioButton5.IsChecked == true) { radioButton5.IsChecked = false; }
            textBoxAmount.Text = (Math.Round(origtotal-(origtotal * .10m),2)).ToString();
        }

        private void radioButton5_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton0.IsChecked == true) { radioButton0.IsChecked = false; }
            if (radioButton10.IsChecked == true) { radioButton10.IsChecked = false; }
            textBoxAmount.Text = (Math.Round(origtotal-(origtotal * .05m),2)).ToString();
        }

        private void radioButton0_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton10.IsChecked == true) { radioButton10.IsChecked = false; }
            if (radioButton5.IsChecked == true) { radioButton5.IsChecked = false; }
            textBoxAmount.Text = origtotal.ToString();
        }
    }
}
