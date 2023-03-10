﻿namespace TicTacToeApi.Models
{
    public class Game : BaseModel
    {
        public string Board { get; private set; } = ".........";
        public string Status { get; private set; } = "Game in process. First player's turn.";
        public char Turn { get; private set; } = 'X';
        public Player? Player1 { get; set; }
        public Player? Player2 { get; set; }

        public string DoMove(Move move)
        {
            if (!Status.Contains("in process.")) return Status;
            updateBoard(move);
            updateTurn();
            updateStatus();
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
            Status = Turn == 'X' ? "Game in process. First player's turn." : "Game in process. Second player's turn.";
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
