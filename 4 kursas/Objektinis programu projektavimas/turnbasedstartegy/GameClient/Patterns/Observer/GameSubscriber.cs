using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Models;

namespace GameClient.Patterns.Observer
{
    static class GameSubscriber
    {
        private static HubConnection _connection = null;

        public static HubConnection PrepareConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/observer/game")
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

        public static async Task SubscribeToGame(Game game)
        {
            await InitConnection();
            await _connection.InvokeAsync("SubscribeToGame", game);
        }

        public static async Task UnsubscribeFromGame(Game game)
        {
            await InitConnection();
            await _connection.InvokeAsync("UnsubscribeFromGame", game);
        }
    }
}
