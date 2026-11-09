using tabuleiro;

namespace Xadrez
{

    class Knight : ChessPiece
    {

        public Knight(Chessboard chessboard, Color cor) : base(chessboard, cor)
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

            pos.SetValues(Position.Row - 1, Position.Column - 2);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.SetValues(Position.Row - 2, Position.Column - 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.SetValues(Position.Row - 2, Position.Column + 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.SetValues(Position.Row - 1, Position.Column + 2);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.SetValues(Position.Row + 2, Position.Column + 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.SetValues(Position.Row + 2, Position.Column - 1);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.SetValues(Position.Row + 1, Position.Column - 2);
            if (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }
    }
}