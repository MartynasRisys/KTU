using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Models;

namespace GameClient
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
            Global.addForm();
        }
        private void LoginWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.removeForm();
        }

        private void backToMainButton_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            
            string username = this.usernameField.Text;
            string password = this.passwordField.Text;
            JObject loginObj = new JObject();
            loginObj["username"] = username;
            loginObj["password"] = password;
            string url = "http://localhost:5000/api/users/login";
            var response = await client.PostAsync(url, new StringContent(loginObj.ToString(), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            JObject resultJObject = JObject.Parse(result);
            User loginProfile = resultJObject.ToObject<User>();

            if (loginProfile.Username != null)
            {
                if (Global.login(loginProfile) == true)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }

            }

        }
    }
}
