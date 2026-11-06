namespace tabuleiro
{
     class Posicion
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Posicion(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public void SetValues(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public override string ToString()
        {
            return Row + ", " + Column;
        }
    }
}