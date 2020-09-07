using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using GameClient.Patterns.Observer;

namespace GameClient
{
    public partial class GameRoomWindow : Form
    {
        HubConnection _connection;

        public GameRoomWindow()
        {
            InitializeComponent();
            Global.addForm();
        }
        private async void GameRoomWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            await exitRoom();
            Global.removeForm();
        }

        private async void updateInterface()
        {
            HttpClient client = new HttpClient();

            string url = "http://localhost:5000/api/gamerooms/" + Global.CurrentGameRoom.Id;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            JObject resultJObject = JObject.Parse(result);
            GameRoom currentGameRoom = resultJObject.ToObject<GameRoom>();
            if (currentGameRoom.UserHostId != 0)
            {
                if (Global.Profile.Id == currentGameRoom.UserHostId)
                {
                    startButton.Show();
                }
                string getHostUrl= "http://localhost:5000/api/users/" + currentGameRoom.UserHostId;
                var responseUserHost = await client.GetAsync(getHostUrl);
                var resultUserHost = await responseUserHost.Content.ReadAsStringAsync();
                JObject resultJObjectUserHost = JObject.Parse(resultUserHost);
                User userHost = resultJObjectUserHost.ToObject<User>();

                hostNameField.Text = userHost.Username;
            }

            if (currentGameRoom.UserJoinerId != 0)
            {
                if (Global.Profile.Id == currentGameRoom.UserJoinerId)
                {
                    startButton.Hide();
                }
                string getJoinerUrl = "http://localhost:5000/api/users/" + currentGameRoom.UserJoinerId;
                var responseUserJoiner = await client.GetAsync(getJoinerUrl);
                var resultUserJoiner = await responseUserJoiner.Content.ReadAsStringAsync();
                JObject resultJObjectUserJoiner = JObject.Parse(resultUserJoiner);
                User userJoiner = resultJObjectUserJoiner.ToObject<User>();

                joinerTextField.Text = userJoiner.Username;
            }
        }

        private void HandleEvents()
        {
            _connection.On<User>("GameRoomJoined", (user) =>
            {
                joinerTextField.Text = user.Username;
                Global.CurrentGameRoom.UserJoinerId = user.Id;
            });

            _connection.On<User>("GameRoomLeft", (user) =>
            {
                joinerTextField.Text = string.Empty;
                Global.CurrentGameRoom.UserJoinerId = 0;

            });

            _connection.On<User>("GameRoomHostLeft", (user) =>
            {
                hostNameField.Text = joinerTextField.Text;
                joinerTextField.Text = string.Empty;
                Global.CurrentGameRoom.UserHostId = user.Id;
                Global.CurrentGameRoom.UserJoinerId = 0;
            });

            _connection.On<Game>("GameStarted", (game) =>
            {
                if (Global.startGame(game) == true)
                {
                    Global.exitGameRoom();
                    CreateBattleUnitWindow createBattleUnitWindow = new CreateBattleUnitWindow();
                    createBattleUnitWindow.Show();
                    Close();
                }
            });
        }

        private async void GameRoomWindow_Load(object sender, EventArgs e)
        {
            if (Global.CurrentGameRoom.Name != null)
            {
                this.gameRoomHeading.Text = Global.CurrentGameRoom.Name;
                this.startingGoldTextField.Text = Global.CurrentGameRoom.StartingGold.ToString();
                switch (Global.CurrentGameRoom.MapSize)
                {
                    case GameRoom.MapSizeEnum.Small:
                        this.mapSizeTextField.Text = "Small";
                        break;
                    case GameRoom.MapSizeEnum.Medium:
                        this.mapSizeTextField.Text = "Medium";
                        break;
                    case GameRoom.MapSizeEnum.Large:
                        this.mapSizeTextField.Text = "Large";
                        break;

                }

                updateInterface();

                _connection = Subscriber.PrepareConnection();
                HandleEvents();
                await Subscriber.SubscribeToGameRoom(Global.CurrentGameRoom);
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }

        }

        private async Task exitRoom()
        {
            if (Global.CurrentGameRoom != null)
            {
                HttpClient client = new HttpClient();

                JObject exitRoomObj = new JObject();
                exitRoomObj["userId"] = Global.Profile.Id;
                exitRoomObj["gameRoomId"] = Global.CurrentGameRoom.Id;
                string url = "http://localhost:5000/api/gamerooms/exit";
                var response = await client.PostAsync(url, new StringContent(exitRoomObj.ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode == true)
                {
                    await Subscriber.UnsubscribeFromGameRoom(Global.CurrentGameRoom);
                    Global.exitGameRoom();
                }
            }
            
        }

        private async void exitButton_Click(object sender, EventArgs e)
        {
            await exitRoom();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            if (Global.CurrentGameRoom.UserJoinerId != 0 && Global.CurrentGameRoom.UserHostId != 0)
            {
                string url = "http://localhost:5000/api/games/create";
                HttpClient client = new HttpClient();

                JObject createGameObj = new JObject();

                createGameObj["gameRoomId"] = Global.CurrentGameRoom.Id;
                createGameObj["hostId"] = Global.CurrentGameRoom.UserHostId;
                createGameObj["joinerId"] = Global.CurrentGameRoom.UserJoinerId;

                var response = await client.PostAsync(url, new StringContent(createGameObj.ToString(), Encoding.UTF8, "application/json"));
                var result = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
