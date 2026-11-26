const boardElement = document.querySelector("#board");
const turnLabel = document.querySelector("#turn-label");
const playerLabel = document.querySelector("#player");
const statusLabel = document.querySelector("#status");
const capturedLabel = document.querySelector("#captured");
const newGameButton = document.querySelector("#new-game");

let gameId;
let gameState;
let selectedSquare;
let possibleMoves;
let message;

async function request(path, options = {}) {
  const response = await fetch(path, {
    headers: { "Content-Type": "application/json" },
    ...options
  });
  const data = await response.json();

  if (!response.ok) {
    throw new Error(data.error || "Não foi possível concluir a jogada.");
  }

  return data;
}

async function startNewGame() {
  newGameButton.disabled = true;
  try {
    const result = await request("/api/games", { method: "POST" });
    gameId = result.id;
    gameState = result.state;
    selectedSquare = null;
    possibleMoves = null;
    message = null;
    render();
  } catch (error) {
    statusLabel.textContent = error.message;
  } finally {
    newGameButton.disabled = false;
  }
}

async function handleSquareClick(row, column) {
  if (!gameState || gameState.finished) return;

  if (selectedSquare === null) {
    try {
      const selection = await request(`/api/games/${gameId}/select`, {
        method: "POST",
        body: JSON.stringify({ row, column })
      });
      selectedSquare = { row, column };
      possibleMoves = selection.possibleMoves;
      message = null;
      render();
    } catch (error) {
      statusLabel.textContent = error.message;
    }
    return;
  }

  if (selectedSquare.row === row && selectedSquare.column === column) {
    selectedSquare = null;
    possibleMoves = null;
    message = null;
    render();
    return;
  }

  const destinationPiece = gameState.board[row][column];
  if (destinationPiece && isCurrentPlayersPiece(destinationPiece)) {
    try {
      const selection = await request(`/api/games/${gameId}/select`, {
        method: "POST",
        body: JSON.stringify({ row, column })
      });
      selectedSquare = { row, column };
      possibleMoves = selection.possibleMoves;
      message = null;
      render();
    } catch (error) {
      message = error.message;
      render();
    }
    return;
  }

  try {
    gameState = await request(`/api/games/${gameId}/move`, {
      method: "POST",
      body: JSON.stringify({
        origin: selectedSquare,
        destiny: { row, column }
      })
    });
    selectedSquare = null;
    possibleMoves = null;
    message = null;
    render();
  } catch (error) {
    message = error.message;
    render();
  }
}

function isCurrentPlayersPiece(symbol) {
  const whitePieces = new Set(["♔", "♕", "♖", "♗", "♘", "♙"]);
  return gameState.currentPlayer === "Brancas"
    ? whitePieces.has(symbol)
    : !whitePieces.has(symbol);
}

function render() {
  if (!gameState) return;

  boardElement.replaceChildren();
  for (let row = 0; row < 8; row++) {
    for (let column = 0; column < 8; column++) {
      const button = document.createElement("button");
      const piece = gameState.board[row][column];
      button.type = "button";
      button.className = `square ${(row + column) % 2 === 0 ? "light" : "dark"}`;
      button.setAttribute("role", "gridcell");
      button.setAttribute("aria-label", `${String.fromCharCode(97 + column)}${8 - row}${piece ? `, ${piece}` : ""}`);

      if (selectedSquare?.row === row && selectedSquare?.column === column) {
        button.classList.add("selected");
      } else if (possibleMoves?.[row]?.[column]) {
        button.classList.add("possible");
      }

      if (piece) {
        button.textContent = piece;
        button.classList.add(["♔", "♕", "♖", "♗", "♘", "♙"].includes(piece) ? "piece-white" : "piece-black");
      }

      button.addEventListener("click", () => handleSquareClick(row, column));
      boardElement.append(button);
    }
  }

  turnLabel.textContent = `JOGADA ${gameState.turn}`;
  playerLabel.textContent = gameState.currentPlayer;
  statusLabel.textContent = message || gameState.status;
  capturedLabel.textContent = gameState.capturedPieces.length
    ? gameState.capturedPieces.join(" ")
    : "Nenhuma";
}

newGameButton.addEventListener("click", startNewGame);
startNewGame();
