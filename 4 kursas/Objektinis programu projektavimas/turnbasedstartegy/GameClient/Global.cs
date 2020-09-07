using System.Windows.Forms;
using Models;

namespace GameClient
{
    static class Global
    {
        private static User _profile = null;
        private static GameRoom _currentGameRoom = null;
        private static Game _currentGame = null;
        private static int _formsCount = 0;

        public static User Profile
        {
            get { return _profile; }
            set { _profile = value; }
        }

        public static GameRoom CurrentGameRoom
        {
            get { return _currentGameRoom; }
            set { _currentGameRoom = value; }
        }

        public static Game CurrentGame
        {
            get { return _currentGame; }
            set { _currentGame = value; }
        }

        public static bool createGameRoom(GameRoom createdGameRoom)
        {
            if (createdGameRoom.Name != null && CurrentGameRoom == null)
            {
                CurrentGameRoom = createdGameRoom;
                return true;
            }
            else return false;
        }

        public static bool joinGameRoom(GameRoom joinedGameRoom)
        {
            if (joinedGameRoom.Name != null && CurrentGameRoom == null)
            {
                CurrentGameRoom = joinedGameRoom;
                return true;
            }
            else return false;
        }

        public static bool startGame(Game startedGame)
        {
            CurrentGame = startedGame;
            return true;
        }

        public static bool exitGameRoom()
        {
            if (CurrentGameRoom != null)
            {
                CurrentGameRoom = null;
                return true;
            }
            else return false;
        }


        public static bool login(User profile)
        {
            if (profile.Username != null)
            {
                Profile = profile;
                return true;
            }
            else return false;
        }

        public static bool logout()
        {
            Profile = null;
            return true;
        }

        public static bool isLoggedIn()
        {
            return Profile != null;
        }

        public static int formsCount
        {
            get { return _formsCount; }
        }

        public static void addForm()
        {
            _formsCount += 1;
        }
        public static void removeForm()
        {
            _formsCount -= 1;
            if (_formsCount <= 0) Application.Exit();
        }
    }
}
