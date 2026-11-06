using tabuleiro;
namespace Xadrez
{
    class ChessGame
    {
        public Chessboard Chessboard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessGame()
        {
            this.Chessboard = new Chessboard(8, 8);
            this.Turn = 1;
            this.CurrentPlayer = Color.White;
            this.Finished = false;
            PlacePieces();
        }
        public void MovePiece(Posicion origin, Posicion destiny)
        {
            ChessPiece piece = Chessboard.RemovePiece(origin);
            piece.IncreaseMoveCount();
            ChessPiece capturedPiece = Chessboard.RemovePiece(destiny);
            Chessboard.PlacePiece(piece, destiny);
        }
        public void MakePlay(Posicion origin, Posicion destiny)
        {
            MovePiece(origin, destiny);
            Turn++;
            ChangePlayer();
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
        private void PlacePieces()
        {
            Chessboard.PlacePiece(new Rook(Chessboard, Color.White), new ChessPosition('c', 1).ToPosition());
            Chessboard.PlacePiece(new Rook(Chessboard, Color.White), new ChessPosition('h', 6).ToPosition());
            Chessboard.PlacePiece(new King(Chessboard, Color.White), new ChessPosition('d', 1).ToPosition());
            Chessboard.PlacePiece(new Rook(Chessboard, Color.Black), new ChessPosition('e', 2).ToPosition());
            Chessboard.PlacePiece(new Rook(Chessboard, Color.Black), new ChessPosition('f', 5).ToPosition());
            Chessboard.PlacePiece(new King(Chessboard, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}