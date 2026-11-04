namespace tabuleiro
{
     class Posicion
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicion(int linha, int coluna)
        {
            this.Linha = linha;
            this.Coluna = coluna;
        }
        public override string ToString()
        {
            return Linha + ", " + Coluna;
        }
    }
}