using tabuleiro;
namespace Xadrez
{
    class King : ChessPiece
    {
        private ChessGame game;
        public King(Chessboard chessboard, Color color, ChessGame game) : base(chessboard, color)
        {
            this.game = game;
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
        private bool TestRookCastling(Posicion position)
        {
            ChessPiece? p = Chessboard.GetPiece(position);
            return p != null && p is Rook && p.Color == Color && p.MoveCount == 0;
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

            // #jogadaespecial roque
            if (MoveCount == 0 && !game.Xeque)
            {
                // #jogadaespecial roque pequeno
                Posicion posT1 = new Posicion(Position.Row, Position.Column + 3);
                if (TestRookCastling(posT1))
                {
                    Posicion p1 = new Posicion(Position.Row, Position.Column + 1);
                    Posicion p2 = new Posicion(Position.Row, Position.Column + 2);
                    if (Chessboard.GetPiece(p1) == null && Chessboard.GetPiece(p2) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicion posT2 = new Posicion(Position.Row, Position.Column - 4);
                if (TestRookCastling(posT2))
                {
                    Posicion p1 = new Posicion(Position.Row, Position.Column - 1);
                    Posicion p2 = new Posicion(Position.Row, Position.Column - 2);
                    Posicion p3 = new Posicion(Position.Row, Position.Column - 3);
                    if (Chessboard.GetPiece(p1) == null && Chessboard.GetPiece(p2) == null && Chessboard.GetPiece(p3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}