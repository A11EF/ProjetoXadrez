using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using tabuleiro;
using Xadrez;
using ChessColor = tabuleiro.Color;
using MediaColor = System.Windows.Media.Color;

namespace ProjetoXadrez
{
    public partial class MainWindow : Window
    {
        private ChessGame game = null!;
        private Posicion? selectedPosition;
        private bool[,] possibleMovements = new bool[8, 8];

        private readonly SolidColorBrush lightSquare = new(MediaColor.FromRgb(238, 238, 210));
        private readonly SolidColorBrush darkSquare = new(MediaColor.FromRgb(118, 150, 86));
        private readonly SolidColorBrush selectedSquare = new(MediaColor.FromRgb(246, 246, 105));
        private readonly SolidColorBrush possibleSquare = new(MediaColor.FromRgb(125, 211, 252));

        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            game = new ChessGame();
            selectedPosition = null;
            possibleMovements = new bool[8, 8];
            RenderBoard();
        }

        private void RenderBoard()
        {
            ChessBoardGrid.Children.Clear();

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    var position = new Posicion(row, column);
                    var square = new Button
                    {
                        BorderThickness = new Thickness(0),
                        Padding = new Thickness(0),
                        FontFamily = new FontFamily("Segoe UI Symbol"),
                        FontSize = 46,
                        Tag = position,
                        Cursor = System.Windows.Input.Cursors.Hand
                    };

                    if (possibleMovements[row, column])
                    {
                        square.Background = possibleSquare;
                    }
                    else if (selectedPosition != null && selectedPosition.Row == row && selectedPosition.Column == column)
                    {
                        square.Background = selectedSquare;
                    }
                    else
                    {
                        square.Background = (row + column) % 2 == 0 ? lightSquare : darkSquare;
                    }

                    ChessPiece? piece = game.Chessboard.GetPiece(row, column);
                    if (piece != null)
                    {
                        square.Content = GetPieceSymbol(piece);
                        square.Foreground = piece.Color == ChessColor.White ? Brushes.White : Brushes.Black;
                    }

                    square.Click += Square_Click;
                    ChessBoardGrid.Children.Add(square);
                }
            }

            UpdateStatus();
        }

        private static string GetPieceSymbol(ChessPiece piece)
        {
            bool white = piece.Color == ChessColor.White;

            if (piece is King) return white ? "\u2654" : "\u265A";
            if (piece is Queen) return white ? "\u2655" : "\u265B";
            if (piece is Rook) return white ? "\u2656" : "\u265C";
            if (piece is Bishop) return white ? "\u2657" : "\u265D";
            if (piece is Knight) return white ? "\u2658" : "\u265E";
            return white ? "\u2659" : "\u265F";
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            if (game.Finished)
            {
                return;
            }

            var square = (Button)sender;
            var position = (Posicion)square.Tag;

            try
            {
                if (selectedPosition == null)
                {
                    game.ValidateOriginPosition(position);
                    selectedPosition = position;
                    possibleMovements = game.Chessboard.GetPiece(position).PossibleMovements();
                }
                else
                {
                    game.ValidateDestinyPosition(selectedPosition, position);
                    game.MakePlay(selectedPosition, position);
                    selectedPosition = null;
                    possibleMovements = new bool[8, 8];
                }
            }
            catch (ChessboardException exception)
            {
                StatusText.Text = exception.Message;
                selectedPosition = null;
                possibleMovements = new bool[8, 8];
            }

            RenderBoard();
        }

        private void UpdateStatus()
        {
            TurnText.Text = game.Turn.ToString();
            PlayerText.Text = game.CurrentPlayer == ChessColor.White ? "Brancas" : "Pretas";

            if (game.Finished)
            {
                StatusText.Text = "Xeque-mate!\nVencedor: " + (game.CurrentPlayer == ChessColor.White ? "Brancas" : "Pretas");
            }
            else if (game.Xeque)
            {
                StatusText.Text = "XEQUE!";
            }
            else
            {
                StatusText.Text = "Em jogo";
            }

            var captured = new List<string>();
            foreach (ChessPiece piece in game.CapturedPieces)
            {
                captured.Add(GetPieceSymbol(piece));
            }

            CapturedText.Text = captured.Count == 0 ? "Nenhuma" : string.Join(" ", captured);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}
