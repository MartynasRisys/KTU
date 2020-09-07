using System;
namespace Models
{
    public class Turn
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public long GameId { get; set; }
        public bool PlayerHostEnded { get; set; }
        public bool PlayerJoinerEnded { get; set; }


        public Turn()
        {
            PlayerHostEnded = false;
            PlayerJoinerEnded = false;
        }

        public Turn(int number, long gameId)
        {
            PlayerHostEnded = false;
            PlayerJoinerEnded = false;
            GameId = gameId;
            Number = number;
        }
    }
}
