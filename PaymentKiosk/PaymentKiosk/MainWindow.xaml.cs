using PaymentKiosk.Core.Domain;
using PaymentKiosk.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        decimal total;

        public MainWindow()
        {
            InitializeComponent();

            comboBoxPRod.ItemsSource = Coffee.coffee;
            comboBoxPRod.DisplayMemberPath = "Description";

            comboBoxQuant.ItemsSource = Quantity.quantity;

            dataGrid.ItemsSource = runninglist;
            dataGrid.IsReadOnly = true;
            dataGrid.Columns.Add(addColumn("Item", "Description"));
            dataGrid.Columns.Add(addColumn("Price", "amount"));
        }

        private DataGridTextColumn addColumn(string header, string source)
        {
            DataGridTextColumn x = new DataGridTextColumn();
            x.Header = header;
            x.Binding = new Binding(source);
            return x;
        }

        private void buttonCharge_Click(object sender, RoutedEventArgs e)
        {
            Payment_Window paywin = new Payment_Window(total);
            paywin.Owner = this;
            paywin.Show();
        }

        public ObservableCollection<Product> runninglist = new ObservableCollection<Product>();

        public void buttonAddItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (var i = 0; i < (int)comboBoxQuant.SelectedItem; i++)
                {
                    runninglist.Add((Product)comboBoxPRod.SelectedItem);
                }

                comboBoxQuant.SelectedItem = null;
                comboBoxPRod.SelectedItem = null;

                total = 0;
                foreach (var x in runninglist)
                {
                    total += x.amount;
                }

                textBlockTotalAmt.Text = total.ToString();
            }
            catch
            {
                MessageBox.Show("Please enter a product and a quantity.");
            }
        }

        public static void SuccessfulTransaction()
        {
            var message = "Payment Successful";
            MessageBox.Show(message);
        }

    }
}
