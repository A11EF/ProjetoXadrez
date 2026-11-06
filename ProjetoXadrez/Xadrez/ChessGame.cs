using tabuleiro;
using System.Collections.Generic;
namespace Xadrez
{
    class ChessGame
    {
        public Chessboard Chessboard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<ChessPiece> Pieces;
        public HashSet<ChessPiece> CapturedPieces{ get; private set; }
        public bool xeque { get; private set; }

        public ChessGame()
        {
            this.Chessboard = new Chessboard(8, 8);
            this.Turn = 1;
            this.CurrentPlayer = Color.White;
            this.Finished = false;
            this.Pieces = new HashSet<ChessPiece>();
            this.CapturedPieces = new HashSet<ChessPiece>();
            this.xeque = false;
            PlacePieces();
        }
        public ChessPiece MovePiece(Posicion origin, Posicion destiny)
        {
            ChessPiece piece = Chessboard.RemovePiece(origin);
            piece.IncreaseMoveCount();
            ChessPiece capturedPiece = Chessboard.RemovePiece(destiny);
            Chessboard.PlacePiece(piece, destiny);
            if(capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }
        public void UndoMove(Posicion origin, Posicion destiny, ChessPiece capturedPiece)
        {
            ChessPiece piece = Chessboard.RemovePiece(destiny);
            piece.DecreaseMoveCount();
            if(capturedPiece != null)
            {
                Chessboard.PlacePiece(capturedPiece, destiny);
                CapturedPieces.Remove(capturedPiece);
            }
            Chessboard.PlacePiece(piece, origin);
        }
        public void MakePlay(Posicion origin, Posicion destiny)
        {
            ChessPiece capturedPiece = MovePiece(origin, destiny);
            if(IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new ChessboardException("Você não pode se colocar em xeque!");
            }
            if(IsInCheck(Opponent(CurrentPlayer)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if(IsCheckMate(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
        }
        public void ValidateOriginPosition(Posicion position)
        {
            if (Chessboard.GetPiece(position) == null)
            {
                throw new ChessboardException("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != Chessboard.GetPiece(position).Color)
            {
                throw new ChessboardException("A peça de origem escolhida não é sua!");
            }
            if (!Chessboard.GetPiece(position).HasPossibleMovements())
            {
                throw new ChessboardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidateDestinyPosition(Posicion origin, Posicion destiny)
        {
            if (!Chessboard.GetPiece(origin).PossibleMovement(destiny))
            {
                throw new ChessboardException("Posição de destino inválida!");
            }
        }
        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }
        public HashSet<ChessPiece> CapturedPiecesByColor(Color color)
        {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            foreach (ChessPiece x in CapturedPieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<ChessPiece> PiecesInGameByColor(Color color)
        {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            foreach (ChessPiece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPiecesByColor(color));
            return aux;
        }
        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        private ChessPiece King(Color color)
        {
            foreach (ChessPiece x in PiecesInGameByColor(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }
        public bool IsInCheck(Color color)
        {
            ChessPiece R = King(color);
            if (R == null)
            {
                throw new ChessboardException("Não existe rei da cor " + color + " no tabuleiro!");
            }
            foreach (ChessPiece x in PiecesInGameByColor(Opponent(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[R.Position.Row, R.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (ChessPiece x in PiecesInGameByColor(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < Chessboard.Rows; i++)
                {
                    for (int j = 0; j < Chessboard.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicion origin = x.Position;
                            Posicion destiny = new Posicion(i, j);
                            ChessPiece capturedPiece = MovePiece(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void PlaceNewPiece(char column, int row, ChessPiece piece)
        {
            Chessboard.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }
        private void PlacePieces()
        {
            PlaceNewPiece('c', 1, new Rook(Chessboard, Color.White));
            PlaceNewPiece('h', 6, new Rook(Chessboard, Color.White));
            PlaceNewPiece('d', 1, new King(Chessboard, Color.White));
            PlaceNewPiece('e', 2, new Rook(Chessboard, Color.Black));
            PlaceNewPiece('f', 5, new Rook(Chessboard, Color.Black));
            PlaceNewPiece('d', 8, new King(Chessboard, Color.Black));
        }
    }
}