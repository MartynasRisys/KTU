export var PROGRAMING_LANG = [
	"python",
	"javascript",
	"mongodb",
	"json",
	"java",
	"html",
	"css",
	"c",
	"csharp",
	"golang",
	"kotlin",
	"php",
	"sql",
	"ruby"
];

export function randomWord() {
	return PROGRAMING_LANG[Math.floor(Math.random() * PROGRAMING_LANG.length)];
}
