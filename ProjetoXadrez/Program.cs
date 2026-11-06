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
                    Console.Clear();
                    Screen.PrintChessboard(game.Chessboard);
                    Console.WriteLine();

                    Console.Write("Origin: ");
                    Posicion origin = Screen.ReadChessPosition().ToPosition();
                    Console.WriteLine();

                    Console.Clear();
                    bool[,] possibleMovements = game.Chessboard.GetPiece(origin).PossibleMovements();
                    Screen.PrintChessboard(game.Chessboard, possibleMovements);


                    Console.WriteLine();
                    Console.Write("Destiny: ");
                    Posicion destiny = Screen.ReadChessPosition().ToPosition();

                    game.MovePiece(origin, destiny);
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