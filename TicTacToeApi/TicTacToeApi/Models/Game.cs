namespace TicTacToeApi.Models
{
    public class Game : BaseModel
    {
        public string Board { get; private set; } = ".........";
        public string Status { get; private set; } = "In process.";
        public char Turn { get; private set; } = 'X';
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public string DoMove(Move move)
        {
            if (Status != "In process.") return Status;
            updateBoard(move);
            updateStatus();
            updateTurn();
            return Status;
        }

        private void updateBoard(Move move)
        {
            char[] boardToArray = Board.ToCharArray();
            boardToArray[move.Position] = move.Val;
            Board = string.Concat(boardToArray);
        }

        private void updateStatus()
        {
            Status = checkWinner('X') ? "First player wins."
                : checkWinner('O') ? "Second player wins."
                : checkDraw() ? "Draw." : Status;
        }

        private void updateTurn()
        {
            Turn = Turn == 'X' ? 'O' : 'X';
        }

        private bool checkWinner(char ch)
        {
            return Enumerable.Range(0, 2).Any(i =>
            Board[i] == ch && Board[i + 3] == ch && Board[i + 6] == ch ||
            Board[i * 3] == ch && Board[i * 3 + 1] == ch && Board[i * 3 + 2] == ch)
                || Board[0] == ch && Board[4] == ch && Board[8] == ch
                || Board[2] == ch && Board[4] == ch && Board[6] == ch;
        }
        
        private bool checkDraw()
        {
            return Board.Contains('.');
        }
    }
}
