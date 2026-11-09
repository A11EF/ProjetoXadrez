using tabuleiro;

namespace Xadrez
{

    class Queen : ChessPiece
    {

        public Queen(Chessboard chessboard, Color cor) : base(chessboard, cor)
        {
        }

        public override string ToString()
        {
            return "Q";
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

            // esquerda
            pos.SetValues(Position.Row, Position.Column - 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row, pos.Column - 1);
            }

            // direita
            pos.SetValues(Position.Row, Position.Column + 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row, pos.Column + 1);
            }

            // acima
            pos.SetValues(Position.Row - 1, Position.Column);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column);
            }

            // abaixo
            pos.SetValues(Position.Row + 1, Position.Column);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column);
            }

            // NO
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column - 1);
            }

            // NE
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column + 1);
            }

            // SE
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column + 1);
            }

            // SO
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            while (Chessboard.PositionExists(pos) && CanMove(pos)) {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.GetPiece(pos) != null && Chessboard.GetPiece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}