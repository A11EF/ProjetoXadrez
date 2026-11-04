using tabuleiro;
using Xadrez;
namespace ProjetoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            // try{
            //     Chessboard chess = new Chessboard(8, 8);
            //     chess.PlacePiece(new Rook(chess, Color.White), new Posicion(0, 0));
            //     chess.PlacePiece(new Rook(chess, Color.White), new Posicion(0, 7));
            //     chess.PlacePiece(new King(chess, Color.White), new Posicion(1, 3));
            //     chess.PlacePiece(new Rook(chess, Color.Black), new Posicion(2, 4));
            //     Screen.PrintChessboard(chess);
            // }
            // catch (ChessboardException e)
            // {
            //     Console.WriteLine(e.Message);
            // }

            ChessPosition pos = new ChessPosition('c', 7);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosition());
        }
    }
}