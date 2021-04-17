namespace SnakeAndLadder.Models
{
    public class Player
    {
        private readonly string _name;

        private int _currentPosition;

        private bool _didComplete;

        public Player(string name)
        {
            _name = name;
            _currentPosition = 0;
            _didComplete = false;
        }
        
        public void SetWinningFlag()
        {
            _didComplete = true;
        }

        public bool DidPlayerCompletedGame()
        {
            return _didComplete;
        }

        public string GetPlayerName()
        {
            return _name;
        }

        public void SetPositionForPlayer(int pos)
        {
            _currentPosition = pos;
        }

        public int GetPlayersCurrentPosition()
        {
            return _currentPosition;
        }
    }
}