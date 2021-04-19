namespace TicTacToe.Models
{
    public class Piece
    {
        public static bool ValidatePiece(char c)
        {
            return c == 'X' || c == 'O';
        }

        public static bool ValidateMove(char oldChar)
        {
            return oldChar == '-';
        }
    }

    public enum GameMoveStatus
    {
        ValidMove,
        InvalidMove,
        WinningMove,
        GameMove
    }
}