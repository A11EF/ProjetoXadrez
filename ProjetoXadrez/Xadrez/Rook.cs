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
        private bool CanMove(Posicion position)
        {
            ChessPiece? p = Chessboard.GetPiece(position);
            return p == null || p.Color != Color;
        }
         public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Chessboard.Rows, Chessboard.Columns];
            Posicion pos = new Posicion(0, 0);
            //up
            pos.SetValues(Position.Row - 1, Position.Column);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column);
            }
            //down
            pos.SetValues(Position.Row + 1, Position.Column);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column);
            }
            //right
            pos.SetValues(Position.Row, Position.Column + 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row, pos.Column + 1);
            }
            //left
            pos.SetValues(Position.Row, Position.Column - 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row, pos.Column - 1);
            }
            return mat;
        }
    }
}