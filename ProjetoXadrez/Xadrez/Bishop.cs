using tabuleiro;

namespace Xadrez
{

    class Bishop : ChessPiece
    {

        public Bishop(Chessboard chessboard, Color cor) : base(chessboard, cor)
        {
        }

        public override string ToString()
        {
            return "B";
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

            // NO
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column - 1);
            }

            // NE
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column + 1);
            }

            // SE
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column + 1);
            }

            // SO
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}