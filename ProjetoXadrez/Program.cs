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
                        Screen.PrintChessboard(game.Chessboard);
                        Console.WriteLine();
                        Console.WriteLine("Turn: " + game.Turn);
                        Console.WriteLine("Waiting player: " + game.CurrentPlayer);

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

                        game.MovePiece(origin, destiny);
                    }
                    catch (ChessboardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (ChessboardException e)
            {
                Console.WriteLine(e.Message);
            }

            // ChessPosition pos = new ChessPosition('c', 7);
            // Console.WriteLine(pos);
            // Console.WriteLine(pos.ToPosition());
        }
    }
}