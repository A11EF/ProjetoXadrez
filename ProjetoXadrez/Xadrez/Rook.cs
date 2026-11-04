using tabuleiro;
namespace Xadrez
{
    class Rook : ChessPiece
    {
        public Rook(Chessboard chessboard, Color color) : base(chessboard, color)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}