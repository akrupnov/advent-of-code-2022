using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
    public enum GameMove
    {
        Rock = 1,
        Paper = 2, 
        Scissors = 3
    }
    public enum GameResult
    {
        Lose = 0,
        Draw = 3,
        Win = 6
    }
    public class DayTwo
    {        
        public String Input { get; set; }
        public DayTwo(String input)
        {
            this.Input = input;
            
        }

        public String SolvePartOne()
        {
            var moves = Input.Split(Environment.NewLine).Select(p => new Tuple<GameMove, GameMove>(ParseMove(p.Split(" ")[0]), ParseMove(p.Split(" ")[1])));
            return moves.Sum(gm => CalculateScore(gm.Item1, gm.Item2)).ToString();
        }

        public String SolvePartTwo()
        {
            var moves = Input.Split(Environment.NewLine).Select(p => new Tuple<GameMove, GameResult>(ParseMove(p.Split(" ")[0]), ParseResult(p.Split(" ")[1])));
            return moves.Sum(gm => CalculateScore(gm.Item1, CalculateMove(gm.Item1, gm.Item2))).ToString();
        }

        private GameMove CalculateMove(GameMove opponentMove, GameResult expectedResult)
        {
            if(expectedResult == GameResult.Draw)
                return opponentMove;

            if(opponentMove == GameMove.Rock)
            {
                if(expectedResult == GameResult.Win)
                {
                    return GameMove.Paper;
                }
                return GameMove.Scissors;
            } 
            else if(opponentMove == GameMove.Paper)
            {
                if(expectedResult == GameResult.Win)
                {
                    return GameMove.Scissors;
                }
                return GameMove.Rock;
            }
            else if(opponentMove == GameMove.Scissors)
            {
                if(expectedResult == GameResult.Win)
                {
                    return GameMove.Rock;
                }
                return GameMove.Paper;
            }
            throw new Exception("Impossible to reach here, please call tech support");

        }

        private Int32 CalculateScore(GameMove opponentMove, GameMove playerMove)
        {
            var baseResult = Convert.ToInt32(playerMove);
            
            var moveResult = GameResult.Lose;
            if(opponentMove == playerMove)
                moveResult = GameResult.Draw;

            if(opponentMove == GameMove.Rock && playerMove == GameMove.Paper)
            {
                    moveResult = GameResult.Win;
            } else if(opponentMove == GameMove.Paper && playerMove == GameMove.Scissors)
            {
                moveResult = GameResult.Win;
            }else if(opponentMove == GameMove.Scissors && playerMove == GameMove.Rock)
            {
                    moveResult = GameResult.Win;

            }
            return baseResult + Convert.ToInt32(moveResult);
        }

        private GameMove ParseMove(String move)
        {
            if (move == "A" || move == "X")
                return GameMove.Rock;
            if(move == "B" || move == "Y")
                return GameMove.Paper;
            if(move == "C" || move == "Z")
                return GameMove.Scissors;

            throw new Exception($"Unknown move {move}");
        }
        private GameResult ParseResult(string value)
        {
            if(value == "X")
                return GameResult.Lose;
            if(value == "Y")
                return GameResult.Draw;
            if(value == "Z")
                return GameResult.Win;
                
            throw new Exception($"Unknown result {value}");

        }
    }
}