using tabuleiro;
namespace ProjetoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Chessboard chess = new Chessboard(8, 8);
            Screen.PrintChessboard(chess);
            Console.WriteLine();
        }
    }
}