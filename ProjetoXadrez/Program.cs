using tabuleiro;
using Xadrez;
namespace ProjetoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessGame game = new ChessGame();
                while (!game.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintGame(game);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Posicion origin = Screen.ReadChessPosition().ToPosition();
                        game.ValidateOriginPosition(origin);

                        bool[,] possibleMovements = game.Chessboard.GetPiece(origin).PossibleMovements();
                        Console.Clear();
                        Screen.PrintChessboard(game.Chessboard, possibleMovements);


                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Posicion destiny = Screen.ReadChessPosition().ToPosition();
                        game.ValidateDestinyPosition(origin, destiny);

                        game.MakePlay(origin, destiny);
                    }
                    catch (ChessboardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintGame(game);
            }
            catch (ChessboardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}