using tabuleiro;

namespace Xadrez
{

    class Pawn : ChessPiece
    {

        public Pawn(Chessboard chessboard, Color cor) : base(chessboard, cor)
        {
        }

        public override string ToString()
        {
            return "P";
        }
        private bool HaveEnemy(Posicion position)
        {
            ChessPiece? p = Chessboard.GetPiece(position);
            return p != null && p.Color != Color;
        }
        private bool Free(Posicion position)
        {
            return Chessboard.GetPiece(position) == null;
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

            if (Color == Color.White)
            {
                pos.SetValues(Position.Row - 1, Position.Column);
                if (Chessboard.PositionExists(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row - 2, Position.Column);
                Posicion p2 = new Posicion(Position.Row - 1, Position.Column);
                if (Chessboard.PositionExists(p2) && Free(p2) && Chessboard.PositionExists(pos) && Free(pos) && MoveCount == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row - 1, Position.Column - 1);
                if (Chessboard.PositionExists(pos) && HaveEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row - 1, Position.Column + 1);
                if (Chessboard.PositionExists(pos) && HaveEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }
            else
            {
                pos.SetValues(Position.Row + 1, Position.Column);
                if (Chessboard.PositionExists(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 2, Position.Column);
                Posicion p3 = new Posicion(Position.Row + 1, Position.Column);
                if (Chessboard.PositionExists(p3) && Free(p3) && Chessboard.PositionExists(pos) && Free(pos) && MoveCount == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 1, Position.Column - 1);
                if (Chessboard.PositionExists(pos) && HaveEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 1, Position.Column + 1);
                if (Chessboard.PositionExists(pos) && HaveEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }

            return mat;
        }
    }
}
