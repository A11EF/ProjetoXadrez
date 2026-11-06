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

        public ChessPiece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }
        public ChessPiece GetPiece(Posicion position)
        {
            return Pieces[position.Row, position.Column];
        }
        public bool HasPiece(Posicion position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null;
        }
        public void PlacePiece(ChessPiece piece, Posicion position)
        {
            if (HasPiece(position))
            {
                throw new ChessboardException("Já existe uma peça nessa posição!");
            }
            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public ChessPiece RemovePiece(Posicion pos) {
            if (GetPiece(pos) == null) {
                return null;
            }
            ChessPiece aux = GetPiece(pos);
            aux.Position = null;
            Pieces[pos.Row, pos.Column] = null;
            return aux;
        }

        public bool PositionExists(Posicion position)
        {
            return position.Row >= 0 && position.Row < Rows && position.Column >= 0 && position.Column < Columns;
        }

        public void ValidatePosition(Posicion position)
        {
            if (!PositionExists(position))
            {
                throw new ChessboardException("Posição inválida!");
            }
        }
    }
}