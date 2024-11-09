using Newtonsoft.Json;
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

namespace WpfApp1
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = username.Text;
            var pass = password.Text;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:4000/users/authenticate");
            var content = new StringContent("{    \"username\": \"" + user + "\",\r\n    \"password\": \"" + pass + "\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            Console.WriteLine(response.Content.ReadAsStringAsync());
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content.ReadAsStringAsync().Result);

            if (myDeserializedClass.token != null)
            {
                Status.Text = "Yes";
            }
            else
            {
                Status.Text = "No";
            }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public int id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string username { get; set; }
            public string token { get; set; }
        }


    }
}