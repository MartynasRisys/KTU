using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace GameServer.Patterns.Observer.Hubs
{
    public class GameHub : Hub
    {

        public async Task SubscribeToGame(Game game)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"game#{game.Id}");
            Logger.getInstance.logMessage($"Received subscribe to game {game.Id} events");
        }

        public async Task UnsubscribeFromGame(Game game)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"game#{game.Id}");
            Logger.getInstance.logMessage($"Received unsubscribe to game {game.Id} events");
        }
    }
}
