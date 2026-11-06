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
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //down
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //southeast
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //southwest
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //northeast
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //northwest
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //left
            pos.SetValues(Position.Row, Position.Column - 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //right
            pos.SetValues(Position.Row, Position.Column + 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
        }
    }
}