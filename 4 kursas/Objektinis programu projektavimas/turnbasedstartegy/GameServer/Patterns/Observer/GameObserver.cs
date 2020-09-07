using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.SignalR;
using GameServer.Patterns.Observer.Hubs;

namespace GameServer.Patterns.Observer
{
    public class GameObserver
    {
        private IHubContext<GameHub> _hubContext;

        public GameObserver(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyTurnUpdated(Turn turn)
        {
            await _hubContext.Clients.Group($"game#{turn.GameId}").SendAsync("GameTurnUpdate", turn);
        }

        public async Task NotifyTurnActionCreated(TurnAction turnAction)
        {
            await _hubContext.Clients.Group($"game#{turnAction.GameId}").SendAsync("TurnActionCreated", turnAction);
        }

        //public async Task NotifyGameRoomJoined(GameRoom gameRoom, User user)
        //{
        //    await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameRoomJoined", user);
        //}

        //public async Task NotifyGameStarted(GameRoom gameRoom, Game game)
        //{
        //    await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameStarted", game);
        //}

        //public async Task NotifyGameRoomLeft(GameRoom gameRoom, User user)
        //{
        //    await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameRoomLeft", user);
        //}

        //public async Task NotifyGameRoomHostLeft(GameRoom gameRoom, User user)
        //{
        //    await _hubContext.Clients.Group($"{gameRoom.Id}{gameRoom.Name}").SendAsync("GameRoomHostLeft", user);
        //}
    }
}
