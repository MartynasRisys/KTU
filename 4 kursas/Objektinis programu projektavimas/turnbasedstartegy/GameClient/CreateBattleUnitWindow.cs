using Models;
using Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using GameClient.Patterns.Observer;
using Patterns.TemplateMethod;
using Patterns.ItteratorPattern;
using Views.StatePattern;

namespace GameClient
{
    public partial class CreateBattleUnitWindow : Form
    {
        HubConnection _connection;

        private int selectedIndex;
        private List<BattleUnitView> battleUnits = new List<BattleUnitView>();
        private MapView map = null;
        private Turn CurrentTurn = new Turn(0, Global.CurrentGame.Id);
        private TurnActionTypesEnum currentAction = TurnActionTypesEnum.None;

        public CreateBattleUnitWindow()
        {
            InitializeComponent();
            Global.addForm();
        }

        public void updateMap()
        {
            mapLayout.Invalidate();
        }

        private async void CreateBattleUnitWindow_Load(object sender, EventArgs e)
        {
            await getMapView(Global.CurrentGame.MapId);
            await getMapObstacles(Global.CurrentGame.MapId);
            UpdatePlayerTurnText();
            updateMap();

            _connection = GameSubscriber.PrepareConnection();
            HandleEvents();
            await GameSubscriber.SubscribeToGame(Global.CurrentGame);
        }

        public void UpdatePlayerTurnText()
        {
            //Update Your turn or Enemy turn text
            if (Global.Profile.Id == Global.CurrentGame.HostId && CurrentTurn.PlayerHostEnded == true ||
                (Global.Profile.Id == Global.CurrentGame.JoinerId && (CurrentTurn.PlayerJoinerEnded == true || CurrentTurn.PlayerHostEnded == false)))
            {
                playerTurn.Text = "Enemy turn";
            }

            if (Global.Profile.Id == Global.CurrentGame.HostId && CurrentTurn.PlayerHostEnded == false ||
                (Global.Profile.Id == Global.CurrentGame.JoinerId && CurrentTurn.PlayerHostEnded == true && CurrentTurn.PlayerJoinerEnded == false)
                || CurrentTurn.Number == 0)
            {
                playerTurn.Text = "Your turn";
            }
            turnLabel.Text = "Turn " + CurrentTurn.Number;
        }

        public async Task getMapObstacles(long mapId)
        {
            string url = "http://localhost:5000/api/maps/" + mapId + "/obstacles";
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            JArray resultJArray = JArray.Parse(result);
            Obstacle[] obstaclesList = resultJArray.ToObject<Obstacle[]>();
            List<ObstacleView> obstacleViews = new List<ObstacleView>();
            ObstacleAggregate aggregate = new ObstacleAggregate(obstaclesList);
            Iterator i = aggregate.CreateIterator();
            Obstacle obs = i.First() as Obstacle;
            while (obs != null)
            {
                obstacleViews.Add(new ObstacleView(obs.X, obs.Y));
                obs = i.Next() as Obstacle;
            }


            map.addObstacles(obstacleViews);
        }

        public async Task getMapBattleUnits(long mapId)
        {
            string url = "http://localhost:5000/api/battle_units/map/" + mapId;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            JArray resultJArray = JArray.Parse(result);
            BattleUnit[] battleUnitsList = resultJArray.ToObject<BattleUnit[]>();

            BattleUnitAggregate aggregate = new BattleUnitAggregate(battleUnitsList);
            Iterator i = aggregate.CreateIterator();
            BattleUnit bu = i.First() as BattleUnit;
            while (bu != null)
            {
                if (bu.BattleUnitType == BattleUnitTypeEnum.Square)
                {
                    map.addBattleUnit(new SquareView(bu.PlayerId, bu.X, bu.Y));
                }
                //TRIANGLE
                else if (bu.BattleUnitType == BattleUnitTypeEnum.Triangle)
                {
                    map.addBattleUnit(new TriangleView(bu.PlayerId, bu.X, bu.Y));
                }
                //CIRCLE
                else if (bu.BattleUnitType == BattleUnitTypeEnum.Circle)
                {
                    map.addBattleUnit(new CircleView(bu.PlayerId, bu.X, bu.Y));
                }
                bu = i.Next() as BattleUnit;
            }
        }

        public async Task getMapView(long mapId)
        {
            string url = "http://localhost:5000/api/maps/" + mapId;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            JObject resultObject = JObject.Parse(result);
            Map mapModel = resultObject.ToObject<Map>();

            map = new MapView(mapModel.Size);
        }

        private void battleUnitTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = battleUnitTypeComboBox.SelectedIndex;
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            if (map.selectedField.X > 0 && map.getSelectedMapUnit() == null)
            {
                string url = "http://localhost:5000/api/battle_units/create";
                HttpClient client = new HttpClient();

                JObject createBattleUnitObj = new JObject();

                createBattleUnitObj["selectedBattleUnitTypeIndex"] = this.selectedIndex;
                createBattleUnitObj["mapId"] = Global.CurrentGame.MapId;
                createBattleUnitObj["playerId"] = Global.Profile.Id;
                createBattleUnitObj["x"] = map.selectedField.X;
                createBattleUnitObj["y"] = map.selectedField.Y;
                createBattleUnitObj["specialAbilityUsed"] = false;

                var listViewItem = new ListViewItem(this.selectedIndex.ToString());
                battleUnitsListView.Items.Add(listViewItem);
                battleUnitsListView.Update();

                var response = await client.PostAsync(url, new StringContent(createBattleUnitObj.ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    //SQUARE
                    if (selectedIndex == 0)
                    {
                        map.addBattleUnit(new SquareView(Global.Profile.Id, map.selectedField.X, map.selectedField.Y));
                    }
                    //TRIANGLE
                    else if (selectedIndex == 1)
                    {
                        map.addBattleUnit(new TriangleView(Global.Profile.Id, map.selectedField.X, map.selectedField.Y));
                    }
                    //CIRCLE
                    else if (selectedIndex == 2)
                    {
                        map.addBattleUnit(new CircleView(Global.Profile.Id, map.selectedField.X, map.selectedField.Y));
                    }
                    map.resetSelectedField();
                    updateMap();
                }
            }
        }

        private async void finishButton_Click(object sender, EventArgs e)
        {
            string url = "";
            HttpClient client = new HttpClient();
            if (Global.CurrentGame.JoinerId == Global.Profile.Id && CurrentTurn.PlayerJoinerEnded == false && (CurrentTurn.PlayerHostEnded == true || CurrentTurn.Number == 0))
            {
                url = "http://localhost:5000/api/turns/game/" + Global.CurrentGame.Id + "/endJoiner";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
            }
            if (Global.CurrentGame.HostId == Global.Profile.Id && CurrentTurn.PlayerHostEnded == false)
            {
                url = "http://localhost:5000/api/turns/game/" + Global.CurrentGame.Id + "/endHost";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
            }

            
        }

        private void battleUnitsListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public Point convertMapLayoutPoint(Point clickedPoint)
        {
            Size mapLayoutSize = mapLayout.Size;
            int mapSize = map.getSize();
            int fieldWidth = mapLayoutSize.Width / mapSize;
            int fieldHeight = mapLayoutSize.Height / mapSize;
            int mapX = clickedPoint.X / fieldWidth;
            int mapY = clickedPoint.Y / fieldHeight;
            return new Point(mapX, mapY);
        }

        private void mapLayout_Click(object sender, EventArgs e)
        {
            
        }

        private void mapLayout_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Size mapLayoutSize = mapLayout.Size;
            if (map != null)
            {
                int mapSize = map.getSize();
                int fieldWidth = mapLayoutSize.Width / mapSize;
                int fieldHeight = mapLayoutSize.Height / mapSize;

                //GRID DRAW
                Pen linesPen = new Pen(Color.Black, 1);

                for (int i = 0; i < mapSize + 1; i++)
                {
                    Point x1 = new Point(0, fieldHeight * i);
                    Point x2 = new Point(mapLayoutSize.Width, fieldHeight * i);

                    canvas.DrawLine(linesPen, x1, x2);

                    Point y1 = new Point(fieldWidth * i, 0);
                    Point y2 = new Point(fieldWidth * i, mapLayoutSize.Height);

                    canvas.DrawLine(linesPen, y1, y2);
                }

                //DRAW IMAPUNITS
                Image rockImage = Properties.Resources.rock;
                Image squareImage = Properties.Resources.square;
                Image triangleImage = Properties.Resources.triangle;
                Image circleImage = Properties.Resources.circle;
                Image enemySquareImage = Properties.Resources.enemy_square;
                Image enemyTriangleImage = Properties.Resources.enemy_triangle;
                Image enemyCircleImage = Properties.Resources.enemy_circle;

                for (int k = 0; k < mapSize; k++)
                {
                    for (int l = 0; l < mapSize; l++)
                    {
                        Point point = new Point(k, l);
                        IMapUnitView mapUnit = map.getMapUnit(point);
                        if (mapUnit != null)
                        {
                            if (mapUnit.Type == MapUnitTypeEnum.Obstacle)
                            {
                                canvas.DrawImage(rockImage, point.X * fieldWidth + 1, point.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
                            }
                            else if (mapUnit.Type == MapUnitTypeEnum.BattleUnit)
                            {
                                BattleUnitView battleUnit = mapUnit as BattleUnitView;
                                if (battleUnit.getType() == BattleUnitTypeEnum.Square)
                                {
                                    if (battleUnit.PlayerId == Global.Profile.Id)
                                    {
                                        canvas.DrawImage(squareImage, point.X * fieldWidth + 1, point.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
                                    }
                                    else
                                    {
                                        canvas.DrawImage(enemySquareImage, point.X * fieldWidth + 1, point.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
                                    }
                                }
                                if (battleUnit.getType() == BattleUnitTypeEnum.Circle)
                                {
                                    if (battleUnit.PlayerId == Global.Profile.Id)
                                    {
                                        canvas.DrawImage(circleImage, point.X * fieldWidth + 1, point.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
                                    }
                                    else
                                    {
                                        canvas.DrawImage(enemyCircleImage, point.X * fieldWidth + 1, point.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
                                    }
                                }
                                if (battleUnit.getType() == BattleUnitTypeEnum.Triangle)
                                {
                                    if (battleUnit.PlayerId == Global.Profile.Id)
                                    {
                                        canvas.DrawImage(triangleImage, point.X * fieldWidth + 1, point.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
                                    }
                                    else
                                    {
                                        canvas.DrawImage(enemyTriangleImage, point.X * fieldWidth + 1, point.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
                                    }
                                }
                            }
                        }
                    }
                }

                drawSelectedField(canvas, fieldWidth, fieldHeight);
            }

        }

        public void drawSelectedField(Graphics canvas, int fieldWidth, int fieldHeight)
        {
            SolidBrush selectBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 0));
            if (map.selectedField.X > 0)
            {
                canvas.FillRectangle(selectBrush, map.selectedField.X * fieldWidth + 1, map.selectedField.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
            }
            if (map.selectedFieldAdditional.X > 0)
            {
                selectBrush = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
                canvas.FillRectangle(selectBrush, map.selectedFieldAdditional.X * fieldWidth + 1, map.selectedFieldAdditional.Y * fieldHeight + 1, fieldWidth - 1, fieldHeight - 1);
            }
        }

        private void mapLayout_MouseClick(object sender, MouseEventArgs e)
        {
            if (map.usingSecondSelect == true)
            {
                map.setSelectedFieldAdditional(convertMapLayoutPoint(e.Location));
            }
            else
            {
                map.setSelectedField(convertMapLayoutPoint(e.Location));
            }
            updateMap();

            IMapUnitView mapUnit = map.getSelectedMapUnit();
            if (mapUnit != null && mapUnit.Type == MapUnitTypeEnum.BattleUnit)
            {
                BattleUnitView battleUnit = mapUnit as BattleUnitView;
                if (battleUnit.PlayerId == Global.Profile.Id)
                {
                    stateLabel.Text = "Current state: " + battleUnit.State.GetName();
                    moveButton.Show();
                    attackButton.Show();
                    endTurnButton.Show();

                    if (battleUnit.getType() == BattleUnitTypeEnum.Square)
                    {
                        specialAbilityTitle.Show();
                        specialAbilityText.Show();
                        specialAbilityButton.Show();
                        specialAbilityText.Text = "Clone";
                        if (map.usingSecondSelect == false)
                        {
                            if (battleUnit.SpecialAbilityUsed == true)
                            {
                                specialAbilityButton.Text = "Can't use";
                            }
                            else
                            {
                                specialAbilityButton.Text = "Use";
                            }
                        }
                    } else if (battleUnit.getType() == BattleUnitTypeEnum.Triangle)
                    {
                        specialAbilityTitle.Show();
                        specialAbilityText.Show();
                        specialAbilityButton.Show();
                        specialAbilityButton.Text = "Restore location";
                    }
                }
                else
                {
                    moveButton.Hide();
                    attackButton.Hide();
                    endTurnButton.Hide();
                    confirmActionButton.Hide();
                    cancelActionButton.Hide();
                    specialAbilityButton.Hide();
                    specialAbilityTitle.Hide();
                    specialAbilityText.Hide();
                }
            }
            else
            {
                moveButton.Hide();
                attackButton.Hide();
                endTurnButton.Hide();
                confirmActionButton.Hide();
                cancelActionButton.Hide();
                specialAbilityButton.Hide();
                specialAbilityTitle.Hide();
                specialAbilityText.Hide();
            }

        }

        private void mapLayout_MouseHover(object sender, EventArgs e)
        {
        }

        private void mapLayout_MouseEnter(object sender, EventArgs e)
        {
        }

        private async void specialAbilityButton_Click(object sender, EventArgs e)
        {
            BattleUnitView battleUnit = GetCurrentBattleUnit();

            if (battleUnit != null && battleUnit.getType() != BattleUnitTypeEnum.Circle)
            {
                if (battleUnit.SpecialAbilityUsed)
                {
                    UpdateInformationalLabel("This unit has already used it's special ability this turn");

                    return;
                }

                if (map.usingSecondSelect)
                {
                    IMapUnitView mapUnitAdditional = map.getSelectedAdditionalMapUnit();
                    if (mapUnitAdditional == null)
                    {
                        battleUnit.SpecialAbilityUsed = true;

                        //Clone
                        BattleUnitView clonedUnit = battleUnit.Clone();
                        clonedUnit.X = map.selectedFieldAdditional.X;
                        clonedUnit.Y = map.selectedFieldAdditional.Y;

                        string url = "http://localhost:5000/api/battle_units/create";
                        HttpClient client = new HttpClient();

                        JObject createBattleUnitObj = new JObject();

                        createBattleUnitObj["selectedBattleUnitTypeIndex"] = 0;
                        createBattleUnitObj["mapId"] = Global.CurrentGame.MapId;
                        createBattleUnitObj["playerId"] = clonedUnit.PlayerId;
                        createBattleUnitObj["x"] = clonedUnit.X;
                        createBattleUnitObj["y"] = clonedUnit.Y;
                        createBattleUnitObj["specialAbilityUsed"] = clonedUnit.SpecialAbilityUsed;

                        var listViewItem = new ListViewItem(0.ToString());
                        battleUnitsListView.Items.Add(listViewItem);
                        battleUnitsListView.Update();

                        var response = await client.PostAsync(url, new StringContent(createBattleUnitObj.ToString(), Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            clonedUnit.State.TransitionTo(battleUnit, new UsedAction());
                            map.addBattleUnit(clonedUnit);
                        }
                        else
                        {
                            battleUnit.SpecialAbilityUsed = false;
                        }
                        battleUnit.State.TransitionTo(battleUnit, new UsedAction());
                        specialAbilityButton.Hide();
                        specialAbilityTitle.Hide();
                        specialAbilityText.Hide();
                        map.usingSecondSelect = false;
                        map.resetSelectedFieldAdditional();
                        map.resetSelectedField();
                        updateMap();
                    }
                }
                else
                {
                    if (battleUnit.getType() == BattleUnitTypeEnum.Square)
                    {
                        map.usingSecondSelect = true;
                        specialAbilityButton.Text = "Clone";
                    } else if (battleUnit.getType() == BattleUnitTypeEnum.Triangle)
                    {
                        TriangleView triangle = battleUnit as TriangleView;

                        if (!triangle.HasAnyMemento())
                        {
                            UpdateInformationalLabel("No locations currently stored");

                            return;
                        }

                        triangle.RestoreLocation();

                        battleUnit.State.TransitionTo(battleUnit, new UsedAction());
                        createTurnAction(battleUnit.X, battleUnit.Y);
                    }
                }
            }
        }

        private void endTurnButton_Click(object sender, EventArgs e)
        {
            if (!IsActionAvailable(States.UsedAction))
            {
                return;
            }

            BattleUnitView battleUnit = GetCurrentBattleUnit();

            if (battleUnit != null)
            {
                battleUnit.State.TransitionTo(battleUnit, new EndedTurn());

                map.usingSecondSelect = false;
                map.resetSelectedFieldAdditional();
                map.resetSelectedField();
                updateMap();
            }
        }

        public void handleMoveTurnAction(TurnAction action)
        {
            updateMap();
        }

        private void HandleEvents()
        {
            _connection.On<Turn>("GameTurnUpdate", (turn) =>
            {
                handleTurnUpdate(turn);
            });

            _connection.On<TurnAction>("TurnActionCreated", (turnAction) =>
            {
                if (turnAction.TurnActionType == TurnActionTypesEnum.Move)
                {
                    TurnActionHandler handler = new MoveTurnActionHandler();
                    handler.HandleTurnAction(map, turnAction);
                    updateMap();
                }
                else if (turnAction.TurnActionType == TurnActionTypesEnum.Attack)
                {
                    TurnActionHandler handler = new AttackTurnActionHandler();
                    handler.HandleTurnAction(map, turnAction);
                }
                

            });

            //_connection.On<User>("GameRoomHostLeft", (user) =>
            //{
            //    hostNameField.Text = joinerTextField.Text;
            //    joinerTextField.Text = string.Empty;
            //    Global.CurrentGameRoom.UserHostId = user.Id;
            //    Global.CurrentGameRoom.UserJoinerId = 0;
            //});

            //_connection.On<Game>("GameStarted", (game) =>
            //{
            //    if (Global.startGame(game) == true)
            //    {
            //        Global.exitGameRoom();
            //        CreateBattleUnitWindow createBattleUnitWindow = new CreateBattleUnitWindow();
            //        createBattleUnitWindow.Show();
            //        Close();
            //    }
            //});
        }

        public async void handleTurnUpdate(Turn turn)
        {
            if (CurrentTurn.Number == 0 && turn.Number != 0)
            {
                await getMapBattleUnits(Global.CurrentGame.MapId);
                updateMap();
            }
            CurrentTurn = turn;
            UpdatePlayerTurnText();
        }

        private bool IsActionAvailable(States attemptedState)
        {
            if (playerTurn.Text == "Enemy turn")
            {
                UpdateInformationalLabel("You cannot do this during enemy turn");

                return false;
            }

            BattleUnitView battleUnit = GetCurrentBattleUnit();

            if (battleUnit != null && !battleUnit.State.IsAvailable(attemptedState))
            {
                UpdateInformationalLabel($"State {attemptedState} is not available for this unit now");

                return false;
            }

            return true;
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            if (!IsActionAvailable(States.Moved))
            {
                return;
            }

            currentAction = TurnActionTypesEnum.Move;
            map.usingSecondSelect = true;
            confirmActionButton.Show();
            cancelActionButton.Show();
        }

        private void attackButton_Click(object sender, EventArgs e)
        {
            if (!IsActionAvailable(States.UsedAction))
            {
                return;
            }

            currentAction = TurnActionTypesEnum.Attack;
            map.usingSecondSelect = true;
            confirmActionButton.Show();
            cancelActionButton.Show();
        }

        public async void createTurnAction(int X = -1, int Y = -1)
        {
            string url = "http://localhost:5000/api/turn_action/create";
            HttpClient client = new HttpClient();

            JObject createTurnActionObj = new JObject();

            createTurnActionObj["typeIndex"] = (int) currentAction;
            createTurnActionObj["turnId"] = CurrentTurn.Id;
            createTurnActionObj["gameId"] = Global.CurrentGame.Id;
            createTurnActionObj["playerId"] = Global.Profile.Id;
            createTurnActionObj["x1"] = map.selectedField.X;
            createTurnActionObj["y1"] = map.selectedField.Y;
            createTurnActionObj["x2"] = X != -1 ? X : map.selectedFieldAdditional.X;
            createTurnActionObj["y2"] = Y != -1 ? Y : map.selectedFieldAdditional.Y;

            var response = await client.PostAsync(url, new StringContent(createTurnActionObj.ToString(), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                map.resetSelectedFieldAdditional();
                map.resetSelectedField();
                currentAction = TurnActionTypesEnum.None;
                map.usingSecondSelect = false;
                confirmActionButton.Hide();
                cancelActionButton.Hide();
                updateMap();
            }
        }


        private void confirmActionButton_Click(object sender, EventArgs e)
        {
            if (currentAction == TurnActionTypesEnum.Attack)
            {
                BattleUnitView secondaryBattleUnit = GetCurrentSecondaryBattleUnit();

                if (secondaryBattleUnit != null && secondaryBattleUnit.PlayerId != Global.Profile.Id)
                {
                    BattleUnitView battleUnit = GetCurrentBattleUnit();

                    if (battleUnit != null)
                    {
                        battleUnit.State.TransitionTo(battleUnit, new UsedAction());
                    }

                    createTurnAction();
                }
            }
            else
            {
                BattleUnitView battleUnit = GetCurrentBattleUnit();
                
                if (battleUnit != null)
                {
                    battleUnit.State.TransitionTo(battleUnit, new Moved());

                    if (battleUnit.getType() == BattleUnitTypeEnum.Triangle)
                    {
                        TriangleView triangleUnit = battleUnit as TriangleView;
                        string res = triangleUnit.CreateMemento();

                        UpdateInformationalLabel(res);
                    }
                }

                createTurnAction();
            }
        }

        private void cancelActionButton_Click(object sender, EventArgs e)
        {
            currentAction = TurnActionTypesEnum.None;
            map.usingSecondSelect = false;
            map.resetSelectedFieldAdditional();
            confirmActionButton.Hide();
            cancelActionButton.Hide();
            updateMap();
        }

        private async void UpdateInformationalLabel(string text)
        {
            informationalLabel.Text = "Information: " + text;

            await Task.Delay(5000);

            informationalLabel.Text = "Information: ";
        }

        private BattleUnitView GetCurrentBattleUnit()
        {
            IMapUnitView mapUnit = map.getSelectedMapUnit();
            if (mapUnit != null && mapUnit.Type == MapUnitTypeEnum.BattleUnit)
            {
                return mapUnit as BattleUnitView;
            }

            return null;
        }

        private BattleUnitView GetCurrentSecondaryBattleUnit()
        {
            IMapUnitView mapUnit = map.getSelectedAdditionalMapUnit();
            if (mapUnit != null && mapUnit.Type == MapUnitTypeEnum.BattleUnit)
            {
                return mapUnit as BattleUnitView;
            }

            return null;
        }
    }
}
