using tabuleiro;
using Xadrez;
namespace ProjetoXadrez
{
    class Screen
    {
        public static void PrintGame(ChessGame game)
        {
            PrintChessboard(game.Chessboard);
            Console.WriteLine();
            PrintCapturedPieces(game);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.Turn);
            Console.WriteLine("Waiting player: " + game.CurrentPlayer);
            if (game.xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }
        public static void PrintCapturedPieces(ChessGame game)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            PrintSet(game.CapturedPieces.Where(x => x.Color == Color.White).ToHashSet());
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Black: ");
            PrintSet(game.CapturedPieces.Where(x => x.Color == Color.Black).ToHashSet());
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void PrintSet(HashSet<ChessPiece> set)
        {
            Console.Write("[");
            foreach (ChessPiece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
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