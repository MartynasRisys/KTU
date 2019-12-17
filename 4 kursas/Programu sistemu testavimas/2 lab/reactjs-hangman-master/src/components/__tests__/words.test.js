import { PROGRAMING_LANG, randomWord } from "../words";

test("Output one of the words", () => {
  expect(PROGRAMING_LANG).toContain(randomWord());
});
