using tabuleiro;
using Xadrez;
namespace ProjetoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Chessboard chess = new Chessboard(8, 8);
            chess.PlacePiece(new Rook(chess, Color.White), new Posicion(0, 0));
            chess.PlacePiece(new King(chess, Color.White), new Posicion(1, 3));
            chess.PlacePiece(new Rook(chess, Color.Black), new Posicion(2, 4));
            Screen.PrintChessboard(chess);
        }
    }
}