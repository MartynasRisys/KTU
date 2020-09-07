using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Models;

namespace GameClient
{
    public partial class RegisterWindow : Form
    {
        public RegisterWindow()
        {
            InitializeComponent();
            Global.addForm();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RegisterWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.removeForm();
        }

        private async void registerButton_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            string username = usernameField.Text;
            string password = passwordField.Text;
            string repeatPassword = repeatPasswordField.Text;
            if (password == repeatPassword)
            {
                JObject registerObj = new JObject();
                registerObj["username"] = username;
                registerObj["password"] = password;
                string url = "http://localhost:5000/api/users/register";
                var response = await client.PostAsync(url, new StringContent(registerObj.ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode == true)
                {
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
            }
            
        }
    }
}
