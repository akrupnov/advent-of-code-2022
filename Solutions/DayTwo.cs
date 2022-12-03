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
        private Int32 CalculateScore(GameMove opponentMove, GameMove playerMove)
        {
            var baseResult = Convert.ToInt32(playerMove);
            var moveResult = 0;
            if(opponentMove == playerMove)
                moveResult = 3;

            if(opponentMove == GameMove.Rock)
            {
                if(playerMove == GameMove.Paper)
                {
                    moveResult = 6;
                }
                else if(playerMove == GameMove.Scissors)
                {
                    moveResult = 0;
                }
            } else if(opponentMove == GameMove.Paper)
            {
                if(playerMove == GameMove.Rock)
                {
                    moveResult = 0;
                }
                else if(playerMove == GameMove.Scissors)
                {
                    moveResult = 6;
                }

            }else if(opponentMove == GameMove.Scissors)
{
                if(playerMove == GameMove.Rock)
                {
                    moveResult = 6;
                }
                else if(playerMove == GameMove.Paper)
                {
                    moveResult = 0;
                }

            }
            return baseResult + moveResult;
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
    }
}