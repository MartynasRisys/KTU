using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Models;

namespace GameClient.Patterns.Observer
{
    static class Subscriber
    {
        private static HubConnection _connection = null;

        public static HubConnection PrepareConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/observer/gameroom")
                .Build();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };

            return _connection;
        }

        public static async Task InitConnection()
        {
            if (_connection.State != HubConnectionState.Connected)
            {
                await _connection.StartAsync();
            }
        }

        public static async Task SubscribeToGameRoomCreationAndDeletion()
        {
            await InitConnection();
            await _connection.InvokeAsync("SubscribeToGameRoomCreationAndDeletion");
        }

        public static async Task UnsubscribeFromGameRoomCreationAndDeletion()
        {
            await InitConnection();
            await _connection.InvokeAsync("UnsubscribeFromGameRoomCreationAndDeletion");
        }

        public static async Task SubscribeToGameRoom(GameRoom gameRoom)
        {
            await InitConnection();
            await _connection.InvokeAsync("SubscribeToGameRoom", gameRoom);
        }

        public static async Task UnsubscribeFromGameRoom(GameRoom gameRoom)
        {
            await InitConnection();
            await _connection.InvokeAsync("UnsubscribeFromGameRoom", gameRoom);
        }

    }
}
