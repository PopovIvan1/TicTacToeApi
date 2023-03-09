using Microsoft.AspNetCore.Mvc;
using TicTacToeApi.DataBases;
using TicTacToeApi.Models;
using TicTacToeApi.Repositories;

namespace TicTacToeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private IRepository<Game> games { get; set; }
        private IRepository<Move> moves { get; set; }
        private IRepository<Player> players { get; set; }

        public GameController(ApplicationContext context)
        {
            games = new Repository<Game>(context);
            moves = new Repository<Move>(context);
            players = new Repository<Player>(context);
        }

        [HttpGet]
        public JsonResult GetAllGames()
        {
            return new JsonResult(games.GetAll());
        }

        [HttpPost]
        public JsonResult GetGame(int id)
        {
            var game = games.Get(id);
            if (game == null) return new JsonResult("The game doesn't exist.");
            return new JsonResult(game);
        }

        [HttpPost]
        public JsonResult NewGame(string name1, string name2)
        {
            var player1 = players.GetAll().FirstOrDefault(p => p.Name == name1);
            var player2 = players.GetAll().FirstOrDefault(p => p.Name == name2);
            if (name1 == name2 || player1 == null || player2 == null) return new JsonResult("Invalid parameters.");
            var game = new Game(player1, player2);
            games.Create(game);
            return new JsonResult(game);
        }

        [HttpPost]
        public JsonResult GetGameBoard(int id)
        {
            var game = games.Get(id);
            if (game == null) return new JsonResult("The game doesn't exist.");
            return new JsonResult(game.Board);
        }

        [HttpPost]
        public JsonResult NewPalyer(string name)
        {
            if (players.GetAll().FirstOrDefault(p => p.Name == name) != null)
                return new JsonResult("This username is already taken.");
            var player = new Player(name);
            players.Create(player);
            return new JsonResult(player);
        }

        [HttpPost]
        public JsonResult GetPlayer(int id)
        {
            var player = players.Get(id);
            if (player == null) return new JsonResult("The player doesn't exist.");
            return new JsonResult(player);
        }

        [HttpPost]
        public JsonResult DoMove(string playerName, char moveType, int gameId, int column, int row)
        {
            var game = games.Get(gameId);
            if (game == null) return new JsonResult("The game doesn't exist.");
            var player = players.GetAll().FirstOrDefault(p => p.Name == playerName);
            if (player == null) return new JsonResult("The player doesn't exist.");
            Move move = new Move(moveType, column, row, player, game);
            if (!checkTurn(move)) return new JsonResult("Wrong turn.");
            moves.Create(move);
            game.DoMove(move);
            return new JsonResult(game.Status);
        }

        private bool checkTurn(Move move)
        {
            if (move.Position < 0 || move.Val != move.Game.Turn) return false;
            if (!checkPlayer(move)) return false;
            if (move.Game.Board[move.Position] != '.') return false;
            return true;
        }

        private bool checkPlayer(Move move)
        {
            int playerNumber = 0;
            if (move.Player == move.Game.Player1) playerNumber = 1;
            else if (move.Player == move.Game.Player2) playerNumber = 2;
            if (playerNumber == 0) return false;
            if (playerNumber == 1 && move.Val != 'X' || playerNumber == 2 && move.Val != 'O') return false;
            return true;
        }
    }
}
