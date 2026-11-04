using tabuleiro;
namespace ProjetoXadrez
{
    class Screen
    {
        public static void PrintChessboard(Chessboard chessboard)
        {
            for (int i = 0; i < chessboard.Rows; i++)
            {
                for (int j = 0; j < chessboard.Columns; j++)
                {
                    if (chessboard.GetPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(chessboard.GetPiece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}