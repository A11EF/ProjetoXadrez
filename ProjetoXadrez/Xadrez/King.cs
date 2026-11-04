using tabuleiro;
namespace Xadrez
{
    class King : ChessPiece
    {
        public King(Chessboard chessboard, Color color) : base(chessboard, color)
        {
        }
        public override string ToString()
        {
            return "K";
        }
    }
}