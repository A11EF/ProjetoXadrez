using tabuleiro;
namespace Xadrez
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row)
        {
            this.Column = column;
            this.Row = row;
        }
        public Posicion ToPosition()
        {
            return new Posicion(8 - Row, Column - 'a');
        }

        public override string ToString()
        {
            return "" + Column + Row;
        }
    }
}