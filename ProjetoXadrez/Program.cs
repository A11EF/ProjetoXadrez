using tabuleiro;
namespace ProjetoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Chessboard cbd = new Chessboard(8, 8);
            Console.WriteLine(cbd);
        }
    }
}