using System.Collections.Concurrent;
using tabuleiro;
using Xadrez;

var games = new ConcurrentDictionary<Guid, ChessWebGame>();
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/games", () =>
{
    var id = Guid.NewGuid();
    var game = new ChessWebGame();
    games[id] = game;
    return Results.Ok(new { id, state = game.GetState() });
});

app.MapGet("/api/games/{id:guid}", (Guid id) =>
    games.TryGetValue(id, out var game)
        ? Results.Ok(game.GetState())
        : Results.NotFound(new { error = "Jogo não encontrado." }));

app.MapPost("/api/games/{id:guid}/select", (Guid id, ChessCoordinate coordinate) =>
{
    if (!games.TryGetValue(id, out var game))
    {
        return Results.NotFound(new { error = "Jogo não encontrado." });
    }

    try
    {
        return Results.Ok(game.Select(coordinate));
    }
    catch (ChessboardException exception)
    {
        return Results.BadRequest(new { error = exception.Message });
    }
});

app.MapPost("/api/games/{id:guid}/move", (Guid id, ChessMove move) =>
{
    if (!games.TryGetValue(id, out var game))
    {
        return Results.NotFound(new { error = "Jogo não encontrado." });
    }

    try
    {
        return Results.Ok(game.Move(move));
    }
    catch (ChessboardException exception)
    {
        return Results.BadRequest(new { error = exception.Message });
    }
});

app.Run();

public sealed record ChessCoordinate(int Row, int Column);
public sealed record ChessMove(ChessCoordinate Origin, ChessCoordinate Destiny);
public sealed record ChessSelection(bool[][] PossibleMoves);
public sealed record ChessGameState(
    string[][] Board,
    int Turn,
    string CurrentPlayer,
    bool Finished,
    bool InCheck,
    string Status,
    string[] CapturedPieces);

public sealed class ChessWebGame
{
    private readonly ChessGame game = new();
    private readonly object sync = new();

    public ChessGameState GetState()
    {
        lock (sync)
        {
            var board = new string[8][];
            for (int row = 0; row < 8; row++)
            {
                board[row] = new string[8];
                for (int column = 0; column < 8; column++)
                {
                    ChessPiece? piece = game.Chessboard.GetPiece(row, column);
                    board[row][column] = piece == null ? "" : GetPieceSymbol(piece);
                }
            }

            string currentPlayer = game.CurrentPlayer == Color.White ? "Brancas" : "Pretas";
            string status = game.Finished
                ? $"Xeque-mate! Vencedor: {currentPlayer}"
                : game.Xeque ? "Xeque!" : "Em jogo";

            return new ChessGameState(
                board,
                game.Turn,
                currentPlayer,
                game.Finished,
                game.Xeque,
                status,
                game.CapturedPieces.Select(GetPieceSymbol).ToArray());
        }
    }

    public ChessSelection Select(ChessCoordinate coordinate)
    {
        lock (sync)
        {
            var position = ToPosition(coordinate);
            game.ValidateOriginPosition(position);
            bool[,] legalMoves = game.Chessboard.GetPiece(position).PossibleMovements();
            var result = new bool[8][];

            for (int row = 0; row < 8; row++)
            {
                result[row] = new bool[8];
                for (int column = 0; column < 8; column++)
                {
                    result[row][column] = legalMoves[row, column];
                }
            }

            return new ChessSelection(result);
        }
    }

    public ChessGameState Move(ChessMove move)
    {
        lock (sync)
        {
            var origin = ToPosition(move.Origin);
            var destiny = ToPosition(move.Destiny);
            game.ValidateOriginPosition(origin);
            game.ValidateDestinyPosition(origin, destiny);
            game.MakePlay(origin, destiny);
            return GetState();
        }
    }

    private static Posicion ToPosition(ChessCoordinate coordinate)
    {
        var position = new Posicion(coordinate.Row, coordinate.Column);
        if (coordinate.Row < 0 || coordinate.Row >= 8 || coordinate.Column < 0 || coordinate.Column >= 8)
        {
            throw new ChessboardException("Posição inválida!");
        }

        return position;
    }

    private static string GetPieceSymbol(ChessPiece piece)
    {
        bool white = piece.Color == Color.White;

        if (piece is King) return white ? "\u2654" : "\u265A";
        if (piece is Queen) return white ? "\u2655" : "\u265B";
        if (piece is Rook) return white ? "\u2656" : "\u265C";
        if (piece is Bishop) return white ? "\u2657" : "\u265D";
        if (piece is Knight) return white ? "\u2658" : "\u265E";
        return white ? "\u2659" : "\u265F";
    }
}
