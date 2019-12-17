import React, { Component } from "react";
import { randomWord } from "./words";
import {
  resetButton,
  guessedWord,
  handleGuess,
  getGameState,
  getDisplayText
} from "./hangman-functions";

import step0 from "./images/0.jpg";
import step1 from "./images/1.jpg";
import step2 from "./images/2.jpg";
import step3 from "./images/3.jpg";
import step4 from "./images/4.jpg";
import step5 from "./images/5.jpg";
import step6 from "./images/6.jpg";

export class Hangman extends Component {
  static defaultProps = {
    maxWrong: 6,
    images: [step0, step1, step2, step3, step4, step5, step6]
  };

  constructor(props) {
    super(props);
    this.state = { mistake: 0, guessed: new Set(), answer: randomWord() };
    this.setState = this.setState.bind(this)
  }

  generateButtons() {
    return "abcdefghijklmnopqrstuvwxyz".split("").map(letter => (
      <button
        key={letter}
        value={letter}
        onClick={handleGuess(this.setState, this.state)}
        disabled={this.state.guessed.has(letter)}
      >
        {letter}
      </button>
    ));
  }

  render() {
    console.log(this.state)
    const gameOver = this.state.mistake >= this.props.maxWrong;
    const altText = `${this.state.mistake}/${this.props.maxWrong} wrong guesses`;
    const isWinner = guessedWord(this.state).join("") === this.state.answer;
    let gameStat = this.generateButtons();
    gameStat = getGameState(isWinner, gameOver, gameStat);

    return (
      <div className="Hangman">
        <nav className="navbar navbar-expand-lg">
          <a className="navbar-brand text-light" href="/">
            Hangman. <small>Do (or) Die</small>
          </a>
          <span className="d-xl-none d-lg-none text-primary">
            Guessed wrong: {this.state.mistake}
          </span>
          <button
            className="navbar-toggler sr-only"
            type="button"
            data-toggle="collapse"
            data-target="#navbarText"
            aria-controls="navbarText"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarText">
            <ul className="navbar-nav mr-auto">
              <li className="nav-item "></li>
              <li className="nav-item"></li>
              <li className="nav-item"></li>
            </ul>
            <span className="navbar-text text-primary">
              Guessed wrong: {this.state.mistake}
            </span>
          </div>
        </nav>
        <p className="text-center">
          <img src={this.props.images[this.state.mistake]} alt={altText} />
        </p>
        <p className="text-center text-light">
          Guess the Programming Language ?
        </p>
        <p className="Hangman-word text-center">
          {getDisplayText(gameOver, this.state)}{" "}
        </p>

        <p className="text-center text-warning mt-4">{gameStat}</p>

        <div>
          <p className="text-center">
            <button
              className="Hangman-reset"
              onClick={resetButton(this.setState)}
            >
              Reset
            </button>
          </p>
        </div>
      </div>
    );
  }
}

export default Hangman;
