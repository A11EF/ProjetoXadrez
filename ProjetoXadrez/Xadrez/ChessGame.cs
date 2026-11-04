using tabuleiro;
namespace Xadrez
{
    class ChessGame
    {
        public Chessboard Chessboard { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
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