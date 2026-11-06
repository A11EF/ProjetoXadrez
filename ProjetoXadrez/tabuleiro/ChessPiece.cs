namespace tabuleiro
{
    abstract class ChessPiece
    {
        public Posicion? Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }
        public Chessboard Chessboard { get; protected set; }

        public ChessPiece(Chessboard chessboard, Color color)
        {
            this.Position = null;
            this.Chessboard = chessboard;
            this.Color = color;
            this.MoveCount = 0;
        }
        public void IncreaseMoveCount()
        {
            MoveCount++;
        }
        public bool HasPossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Chessboard.Rows; i++)
            {
                for (int j = 0; j < Chessboard.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool PossibleMovement(Posicion position)
        {
            return PossibleMovements()[position.Row, position.Column];
        }
        public abstract bool[,] PossibleMovements();
    }
}