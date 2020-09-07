using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.SignalR;
using GameServer.Patterns.Observer.Hubs;

namespace GameServer.Patterns.Observer
{
    public class GameRoomObserver
    {
        private IHubContext<GameRoomHub> _hubContext;

        public GameRoomObserver(IHubContext<GameRoomHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyGameRoomCreated(GameRoom gameRoom)
        {
            await _hubContext.Clients.Group("gameRoom").SendAsync("GameRoomCreated", gameRoom);
            Logger.getInstance.logMessage($"Sent GameRoomCreated {gameRoom.Id}");
        }

        public async Task NotifyGameRoomDeleted(GameRoom gameRoom)
        {
            await _hubContext.Clients.Group("gameRoom").SendAsync("GameRoomDeleted", gameRoom);
            Logger.getInstance.logMessage($"Sent GameRoomDeleted {gameRoom.Id}");
        }

        public async Task NotifyGameRoomJoined(GameRoom gameRoom, User user)
        {
            await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameRoomJoined", user);
            Logger.getInstance.logMessage($"Sent GameRoomJoined {gameRoom.Id} {user.Id}");
        }

        public async Task NotifyGameStarted(GameRoom gameRoom, Game game)
        {
            await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameStarted", game);
            Logger.getInstance.logMessage($"Sent GameStarted {gameRoom.Id} {game.Id}");
        }

        public async Task NotifyGameRoomLeft(GameRoom gameRoom, User user)
        {
            await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameRoomLeft", user);
            Logger.getInstance.logMessage($"Sent GameRoomLeft {gameRoom.Id} {user.Id}");
        }

        public async Task NotifyGameRoomHostLeft(GameRoom gameRoom, User user)
        {
            await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameRoomHostLeft", user);
            Logger.getInstance.logMessage($"Sent GameRoomHostLeft {gameRoom.Id} {user.Id}");
        }
    }
}
