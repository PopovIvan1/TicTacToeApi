namespace TicTacToeApi.Models
{
    public class Move : BaseModel
    {
        public char Val { get; private set; }
        public int Position { get; private set; }
        public Player Player { get; private set; }
        public Game Game { get; private set; }

        public Move(char val, int column, int row, Player player, Game game)
        {
            Val = val;
            convertPosition(column, row);
            Player = player;
            Game = game;
        }

        private void convertPosition(int column, int row)
        {
            if (column > 3 || row > 3 || column < 1 || row < 1) Position = -1;
            else Position = (row - 1) * 3 + column - 1;
        }
    }
}
