using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace GameServer.Patterns.Observer.Hubs
{
    public class GameRoomHub : Hub
    {
        public async Task SubscribeToGameRoomCreationAndDeletion()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "gameRoom");
            Logger.getInstance.logMessage("Received subscribe to gameRoom create/delete events");
        }

        public async Task UnsubscribeFromGameRoomCreationAndDeletion()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "gameRoom");
            Logger.getInstance.logMessage("Received unsubscribe to gameRoom create/delete events");
        }

        public async Task SubscribeToGameRoom(GameRoom gameRoom)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{gameRoom.Id}{gameRoom.Name}");
            Logger.getInstance.logMessage($"Received subscribe to gameRoom {gameRoom.Id}{gameRoom.Name} join/leave events");
        }

        public async Task UnsubscribeFromGameRoom(GameRoom gameRoom)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{gameRoom.Id}{gameRoom.Name}");
            Logger.getInstance.logMessage($"Received unsubscribe to gameRoom {gameRoom.Id}{gameRoom.Name} join/leave events");
        }
    }
}
