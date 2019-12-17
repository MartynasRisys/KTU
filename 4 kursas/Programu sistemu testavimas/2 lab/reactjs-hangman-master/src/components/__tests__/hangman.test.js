import React from "react";
import ReactDom from "react-dom";
import { Hangman } from "../Hangman";
import {
  resetButton,
  guessedWord,
  handleGuess,
  getMistake,
  getGameState,
  getDisplayText
} from "../hangman-functions";
import { PROGRAMING_LANG } from "../words";

let mockState = { mistake: 100, guessed: new Set(), answer: "python" };
const setState = state => {
  mockState = state;
};

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDom.render(<Hangman />, div);
});

test("Reset state", () => {
  resetButton(setState)();
  expect(mockState.mistake).toBe(0);
  expect(PROGRAMING_LANG).toContain(mockState.answer);
});

test("Get current guessed word", () => {
  mockState.answer = "python";
  mockState.guessed = new Set("p");
  expect(guessedWord(mockState)).toEqual(["p", "_", "_", "_", "_", "_"]);

  mockState.answer = "python";
  mockState.guessed = new Set("r");
  expect(guessedWord(mockState)).toEqual(["_", "_", "_", "_", "_", "_"]);
});

test("Get mistake", () => {
  mockState.answer = "python";
  expect(getMistake(mockState, "p")).toBe(0);
});

test("Handle guess", () => {
  let fakeEvt = { target: { value: "r" } };
  mockState.guessed = new Set();
  mockState.answer = "python";
  mockState.mistake = 0;
  handleGuess(setState, mockState)(fakeEvt);
  expect(mockState.mistake).toEqual(1);

  fakeEvt = { target: { value: "p" } };
  mockState.guessed = new Set();
  mockState.answer = "python";
  mockState.mistake = 0;
  handleGuess(setState, mockState)(fakeEvt);
  expect(mockState.mistake).toEqual(0);
});

test("Get game state", () => {
  expect(getGameState(true, false, "state")).toBe("YOU WON");
  expect(getGameState(false, true, "state")).toBe("YOU LOST");
  expect(getGameState(false, false, "state")).toBe("state");
});

test("Get display text", () => {
  mockState.answer = "python"
  mockState.guessed = new Set("p");

  expect(getDisplayText(true, mockState)).toBe("python");
  expect(getDisplayText(false, mockState)).toEqual(["p", "_", "_", "_", "_", "_"]);
});
