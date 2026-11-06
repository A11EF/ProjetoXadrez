using tabuleiro;
using Xadrez;
namespace ProjetoXadrez
{
    class Screen
    {
        public static void PrintChessboard(Chessboard chessboard)
        {
            for (int i=0; i<chessboard.Rows; i++) {
                Console.Write(8 - i + " ");
                for (int j=0; j<chessboard.Columns; j++) {
                   PrintPiece(chessboard.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintChessboard(Chessboard chessboard, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;
            for (int i=0; i<chessboard.Rows; i++) {
                Console.Write(8 - i + " ");
                for (int j=0; j<chessboard.Columns; j++) {
                    if (possiblePositions[i, j]) {
                        Console.BackgroundColor = changedBackground;
                    }
                    else {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(chessboard.GetPiece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine()!;
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }
        public static void PrintPiece(ChessPiece chessPiece) {

            if(chessPiece == null) {
                Console.Write("- ");
            }
            else {
                if (chessPiece.Color == Color.White) {
                    Console.Write(chessPiece);
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(chessPiece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}