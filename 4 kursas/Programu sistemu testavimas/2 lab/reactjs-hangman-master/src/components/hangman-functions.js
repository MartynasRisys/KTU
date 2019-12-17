import { randomWord } from "./words";
export const resetButton = setState => () => {
  setState({
    mistake: 0,
    guessed: new Set(),
    answer: randomWord()
  });
};

export const guessedWord = state => {
  return state.answer
    .split("")
    .map(letter => (state.guessed.has(letter) ? letter : "_"));
};

export const getMistake = (state, letter) =>
  state.answer.includes(letter) ? 0 : 1;

export const handleGuess = (setState, state) => evt => {
  let letter = evt.target.value;
  setState({
    guessed: state.guessed.add(letter),
    mistake: state.mistake + getMistake(state, letter)
  });
};

export const getGameState = (isWinner, gameOver, gameStat) => {
  if (isWinner) {
    return "YOU WON";
  } if (gameOver) {
    return "YOU LOST";
  }
  return gameStat
};

export const getDisplayText = (gameOver, state) =>
  !gameOver ? guessedWord(state) : state.answer;
