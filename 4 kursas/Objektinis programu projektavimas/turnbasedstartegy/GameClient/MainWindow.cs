using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.SignalR.Client;
using GameClient.Patterns.Observer;
using Patterns.ItteratorPattern;

namespace GameClient
{
    public partial class MainWindow : Form
    {
        HubConnection _connection;

        public MainWindow()
        {
            InitializeComponent();
            Global.addForm();
        }

        private void updateInterface()
        {
            if (Global.Profile == null) //if user is not logged in
            {
                if (tabsControl.TabPages.Contains(profileTab))
                {
                    tabsControl.TabPages.Remove(profileTab);
                }
                logoutButton.Hide();
                usernameText.Hide();

                loginButton.Show();
                registerButton.Show();
            }
            else
            {
                if (!tabsControl.TabPages.Contains(profileTab))
                {
                    tabsControl.TabPages.Add(profileTab);
                }
                loginButton.Hide();
                registerButton.Hide();

                usernameText.Show();
                usernameText.Text = "Hi, " + Global.Profile.Username;
            }
        }

        private async void updateGameRoomsList()
        {

            HttpClient client = new HttpClient();

            string url = "http://localhost:5000/api/gamerooms";
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            JArray resultJArray = JArray.Parse(result);
            GameRoom[] gameRoomsList= resultJArray.ToObject<GameRoom[]>();

            GameRoomAggregate aggregate = new GameRoomAggregate(gameRoomsList);
            Iterator i = aggregate.CreateIterator();
            GameRoom gr = i.First() as GameRoom;
            while (gr != null)
            {
                gameRoomsListbox.Items.Add(gr);
                gr = i.Next() as GameRoom;
            }
        }

        private void handleEvents()
        {
            _connection.On<GameRoom>("GameRoomCreated", (gameRoom) =>
            {
                this.gameRoomsListbox.Items.Add(gameRoom);
            });

            _connection.On<GameRoom>("GameRoomDeleted", (gameRoom) =>
            {
                foreach (GameRoom item in this.gameRoomsListbox.Items)
                {
                    if (item.Id == gameRoom.Id)
                    {
                        gameRoomsListbox.Items.Remove(item);
                    }
                }
            });
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            this.updateInterface();
            this.updateGameRoomsList();

            _connection = Subscriber.PrepareConnection();
            handleEvents();
            await Subscriber.SubscribeToGameRoomCreationAndDeletion();
        }

        private async void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            await Subscriber.UnsubscribeFromGameRoomCreationAndDeletion();
            Global.removeForm();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            if (Global.logout())
            {
                this.updateInterface();
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void createRoomButton_Click(object sender, EventArgs e)
        {
            if (Global.isLoggedIn())
            {
                CreateGameRoomWindow createRoomWindow = new CreateGameRoomWindow();
                createRoomWindow.Show();
                this.Close();
            }
        }

        private async void joinRoomButton_Click(object sender, EventArgs e)
        {
            if (Global.isLoggedIn() == true)
            {
                GameRoom selectedGameRoom = (GameRoom)this.gameRoomsListbox.SelectedItem;
                if (selectedGameRoom != null)
                {
                    HttpClient client = new HttpClient();

                    JObject joinRoomObj = new JObject();
                    joinRoomObj["userId"] = Global.Profile.Id;
                    joinRoomObj["gameRoomId"] = selectedGameRoom.Id;
                    string url = "http://localhost:5000/api/gamerooms/join";
                    var response = await client.PostAsync(url, new StringContent(joinRoomObj.ToString(), Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode == true)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        JObject resultJObject = JObject.Parse(result);
                        GameRoom joinedGameRoom = resultJObject.ToObject<GameRoom>();

                        if (Global.joinGameRoom(joinedGameRoom))
                        {
                            GameRoomWindow gameRoomWindow = new GameRoomWindow();
                            gameRoomWindow.Show();
                            this.Close();
                        }
                    }
                }
                
            }
        }
    }
}
