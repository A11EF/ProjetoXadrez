using tabuleiro;

namespace Xadrez
{

    class Pawn : ChessPiece
    {
        private ChessGame Game;

        public Pawn(Chessboard chessboard, Color cor, ChessGame game) : base(chessboard, cor)
        {
            this.Game = game;
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
                // #SpecialMove En Passant
                if (Position.Row == 3)
                {
                    Posicion left = new Posicion(Position.Row, Position.Column - 1);
                    if (Chessboard.PositionExists(left) && HaveEnemy(left) && Chessboard.GetPiece(left) == Game.VulnerableEnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }
                    Posicion right = new Posicion(Position.Row, Position.Column + 1);
                    if (Chessboard.PositionExists(right) && HaveEnemy(right) && Chessboard.GetPiece(right) == Game.VulnerableEnPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
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
                // #SpecialMove En Passant
                if (Position.Row == 4)
                {
                    Posicion left = new Posicion(Position.Row, Position.Column - 1);
                    if (Chessboard.PositionExists(left) && HaveEnemy(left) && Chessboard.GetPiece(left) == Game.VulnerableEnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }
                    Posicion right = new Posicion(Position.Row, Position.Column + 1);
                    if (Chessboard.PositionExists(right) && HaveEnemy(right) && Chessboard.GetPiece(right) == Game.VulnerableEnPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
