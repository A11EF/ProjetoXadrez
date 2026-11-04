namespace tabuleiro
{
    class ChessPiece
    {
        public Posicion Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }
        public Chessboard Chessboard { get; protected set; }

        public ChessPiece(Posicion position, Chessboard chessboard, Color color)
        {
            this.Position = position;
            this.Chessboard = chessboard;
            this.Color = color;
            this.MoveCount = 0;
        }
    }
}