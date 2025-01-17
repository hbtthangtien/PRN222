using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FetchData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient httpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnViewHTML_Click(object sender, RoutedEventArgs e)
        {
            string uri = txtURL.Text;
            try
            {
                string responseBody = await httpClient.GetStringAsync(uri);
                txtContent.Text = responseBody.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtURL.Clear();
            txtContent.Clear();
        }

        private void btnCloese_Click(object sender, RoutedEventArgs e) => Close();
    }
}