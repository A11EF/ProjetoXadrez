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
        public HashSet<ChessPiece> CapturedPieces { get; private set; }
        public bool Xeque { get; private set; }
        public ChessPiece? VulnerableEnPassant { get; set; }

        public ChessGame()
        {
            this.Chessboard = new Chessboard(8, 8);
            this.Turn = 1;
            this.CurrentPlayer = Color.White;
            this.Finished = false;
            this.Pieces = new HashSet<ChessPiece>();
            this.CapturedPieces = new HashSet<ChessPiece>();
            this.Xeque = false;
            this.VulnerableEnPassant = null;
            PlacePieces();
        }
        public ChessPiece MovePiece(Posicion origin, Posicion destiny)
        {
            ChessPiece piece = Chessboard.RemovePiece(origin);
            piece.IncreaseMoveCount();
            ChessPiece capturedPiece = Chessboard.RemovePiece(destiny);
            Chessboard.PlacePiece(piece, destiny);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            //#jogadaespecial roque pequeno
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Posicion originR = new Posicion(origin.Row, origin.Column + 3);
                Posicion destinyR = new Posicion(origin.Row, origin.Column + 1);
                ChessPiece R = Chessboard.RemovePiece(originR);
                R.IncreaseMoveCount();
                Chessboard.PlacePiece(R, destinyR);
            }
            //#jogadaespecial roque grande
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Posicion originR = new Posicion(origin.Row, origin.Column - 4);
                Posicion destinyR = new Posicion(origin.Row, origin.Column - 1);
                ChessPiece R = Chessboard.RemovePiece(originR);
                R.IncreaseMoveCount();
                Chessboard.PlacePiece(R, destinyR);
            }
            //#jogadaespecial en passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Posicion pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Posicion(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        pawnPosition = new Posicion(destiny.Row - 1, destiny.Column);
                    }
                    capturedPiece = Chessboard.RemovePiece(pawnPosition);
                    CapturedPieces.Add(capturedPiece);
                }
            }
            return capturedPiece;
        }
        public void UndoMove(Posicion origin, Posicion destiny, ChessPiece capturedPiece)
        {
            ChessPiece piece = Chessboard.RemovePiece(destiny);
            piece.DecreaseMoveCount();
            if (capturedPiece != null)
            {
                Chessboard.PlacePiece(capturedPiece, destiny);
                CapturedPieces.Remove(capturedPiece);
            }
            Chessboard.PlacePiece(piece, origin);

            //#jogadaespecial roque pequeno
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Posicion originR = new Posicion(origin.Row, origin.Column + 3);
                Posicion destinyR = new Posicion(origin.Row, origin.Column + 1);
                ChessPiece R = Chessboard.RemovePiece(destinyR);
                R.DecreaseMoveCount();
                Chessboard.PlacePiece(R, originR);
            }
            //#jogadaespecial roque grande
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Posicion originR = new Posicion(origin.Row, origin.Column - 4);
                Posicion destinyR = new Posicion(origin.Row, origin.Column - 1);
                ChessPiece R = Chessboard.RemovePiece(destinyR);
                R.DecreaseMoveCount();
                Chessboard.PlacePiece(R, originR);
            }
            //#jogadaespecial en passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
                {
                    ChessPiece pawn = Chessboard.RemovePiece(destiny);
                    Posicion pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Posicion(3, destiny.Column);
                    }
                    else
                    {
                        pawnPosition = new Posicion(4, destiny.Column);
                    }
                    Chessboard.PlacePiece(pawn, pawnPosition);
                }
            }
        }
        public void MakePlay(Posicion origin, Posicion destiny)
        {
            ChessPiece capturedPiece = MovePiece(origin, destiny);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new ChessboardException("Você não pode se colocar em xeque!");
            }
            // #jogadaespecial promoção
            ChessPiece piece = Chessboard.GetPiece(destiny);
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && destiny.Row == 0) || (piece.Color == Color.Black && destiny.Row == 7))
                {
                    piece = Chessboard.RemovePiece(destiny);
                    Pieces.Remove(piece);
                    ChessPiece queen = new Queen(Chessboard, piece.Color);
                    Chessboard.PlacePiece(queen, destiny);
                    Pieces.Add(queen);
                }
            }
            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (IsCheckMate(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
            //#jogadaespecial en passant
            if (piece is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                VulnerableEnPassant = piece;
            }
            else
            {
                VulnerableEnPassant = null;
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
            PlaceNewPiece('a', 1, new Rook(Chessboard, Color.White));
            PlaceNewPiece('b', 1, new Knight(Chessboard, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Chessboard, Color.White));
            PlaceNewPiece('d', 1, new Queen(Chessboard, Color.White));
            PlaceNewPiece('e', 1, new King(Chessboard, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(Chessboard, Color.White));
            PlaceNewPiece('g', 1, new Knight(Chessboard, Color.White));
            PlaceNewPiece('h', 1, new Rook(Chessboard, Color.White));
            for (int i = 0; i < Chessboard.Columns; i++)
            {
                PlaceNewPiece((char)('a' + i), 2, new Pawn(Chessboard, Color.White,this));
            }
            PlaceNewPiece('a', 8, new Rook(Chessboard, Color.Black));
            PlaceNewPiece('b', 8, new Knight(Chessboard, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Chessboard, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Chessboard, Color.Black));
            PlaceNewPiece('e', 8, new King(Chessboard, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Chessboard, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Chessboard, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Chessboard, Color.Black));
            for (int i = 0; i < Chessboard.Columns; i++)
            {
                PlaceNewPiece((char)('a' + i), 7, new Pawn(Chessboard, Color.Black,this));
            }
        }
    }
}