namespace tabuleiro
{
    class Chessboard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        private ChessPiece[,] Pieces { get; set; }

        public Chessboard(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Pieces = new ChessPiece[rows, columns];
        }
    }
}