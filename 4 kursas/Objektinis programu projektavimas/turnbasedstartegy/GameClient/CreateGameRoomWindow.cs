using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Models;

namespace GameClient
{
    public partial class CreateGameRoomWindow : Form
    {
        public CreateGameRoomWindow()
        {
            InitializeComponent();
            Global.addForm();
        }

        private void CreateGameRoomWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.removeForm();
        }

        private async void createRoomButton_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:5000/api/gamerooms/create";
            HttpClient client = new HttpClient();

            string roomName = this.roomNameField.Text;
            int startingGold = Int32.Parse(this.startingMoneyField.Text);
            long userId = Global.Profile.Id;
            long mapSize = 0;
            if (mediumMapRadioButton.Checked)
            {
                mapSize = 1;
            }
            else if (largeMapRadioButton.Checked)
            {
                mapSize = 2;
            }

            JObject createRoomObj = new JObject();
            createRoomObj["roomName"] = roomName;
            createRoomObj["startingGold"] = startingGold;
            createRoomObj["userHostId"] = userId;
            createRoomObj["mapSize"] = mapSize;

            var response = await client.PostAsync(url, new StringContent(createRoomObj.ToString(), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            JObject resultJObject = JObject.Parse(result);
            GameRoom createdGameRoom = resultJObject.ToObject<GameRoom>();

            if (Global.createGameRoom(createdGameRoom) == true)
            {
                    GameRoomWindow gameRoomWindow = new GameRoomWindow();
                    gameRoomWindow.Show();
                    this.Close();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
