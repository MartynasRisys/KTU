using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace L4_ignjas_IFF66
{
    class Program
    {
        const string learningSpamPath = @"../../../learning/spam";
        const string learningNotSpamPath = @"../../../learning/ne_spam";

        const string testingNotSpamPath = @"../../../testing/ne_spam";
        const string testingSpamPath = @"../../../testing/spam";

        const int leksemuSkaicius = 10;
        const double newWordSpamProbability = 0.4;
        const double neutralProbability = 0.65;

        static void Main(string[] args)
        {
            Dictionary<string, LearningCounter> learningTable = new Dictionary<string, LearningCounter>();
            int learningSpamFilesCount = 0;
            int learningSpamWordsCount = 0;
            int learningNotSpamWordsCount = 0;
            int learningNotSpamFilesCount = 0;
            
            //LEARNING PHASE SPAM
            foreach (string file in Directory.EnumerateFiles(learningSpamPath, "*.txt"))
            {
                string contents = File.ReadAllText(file);
                string word = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    char character = contents[i];
                    //find words
                    if (character >= 'a' && character <= 'z' || character >= 'A' && character <= 'Z'
                        || character >= '0' && character <= '9' || character == '\'' || character == '$' || character == '"')
                    {
                        word += character;
                    }
                    else if (word != "")
                    {
                        learningSpamWordsCount++;
                        if (!learningTable.ContainsKey(word))
                        {
                            learningTable.Add(word, new LearningCounter());
                            learningTable[word].increaseSpam();
                        }
                        else
                        {
                            learningTable[word].increaseSpam();
                        }
                        word = "";
                    }
                }
                learningSpamFilesCount++;
            }

            //LEARNING PHASE NOT SPAM
            foreach (string file in Directory.EnumerateFiles(learningNotSpamPath, "*.txt"))
            {
                string contents = File.ReadAllText(file);
                string word = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    char character = contents[i];
                    //find words
                    if (character >= 'a' && character <= 'z' || character >= 'A' && character <= 'Z'
                        || character >= '0' && character <= '9' || character == '\'' || character == '$' || character == '"')
                    {
                        word += character;
                    }
                    else if (word != "")
                    {
                        learningNotSpamWordsCount++;
                        if (!learningTable.ContainsKey(word))
                        {
                            learningTable.Add(word, new LearningCounter());
                            learningTable[word].increaseNotSpam();
                        }
                        else
                        {
                            learningTable[word].increaseNotSpam();
                        }
                        word = "";
                    }
                }
                learningNotSpamFilesCount++;
            }
            //CALCULATE ALL SPAM PROBABILITIES
            foreach (KeyValuePair<string, LearningCounter> entry in learningTable)
            {
                // do something with entry.Value or entry.Key
                entry.Value.calculateSpamProbability(learningSpamWordsCount, learningNotSpamWordsCount);
            }

            //TESTAVIMAS
            double truePositiveSpam = 0;
            double truePositiveSpamCount = 0;
            double falsePositiveSpam = 0;
            double falsePositiveSpamCount = 0;
            double programAccuracy = 0;
            double totalSpamAnalyzes = 0;
            foreach (string file in Directory.EnumerateFiles(testingSpamPath, "*.txt"))
            {
                List<AnalyzeHelper> analyzeList = new List<AnalyzeHelper>();
                string contents = File.ReadAllText(file);
                string word = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    char character = contents[i];
                    //find words
                    if (character >= 'a' && character <= 'z' || character >= 'A' && character <= 'Z'
                        || character >= '0' && character <= '9' || character == '\'' || character == '$' || character == '"')
                    {
                        word += character;
                    }
                    else if (word != "")
                    {
                        if (!learningTable.ContainsKey(word))
                        {
                            AnalyzeHelper newWord = new AnalyzeHelper(word, newWordSpamProbability, neutralProbability);
                            if (!analyzeList.Any(x => x.word == word)) analyzeList.Add(newWord);
                        }
                        else
                        {
                            LearningCounter wordInfo = learningTable[word];
                            AnalyzeHelper newWord = new AnalyzeHelper(word, wordInfo.spamProbability, neutralProbability);
                            if (!analyzeList.Any(x => x.word == word)) analyzeList.Add(newWord);
                        }
                        word = "";
                    }
                }
                //Sort by difference
                analyzeList = analyzeList.OrderByDescending(x => x.probabilityDifference).ToList();
                int wordsPicked = 0;
                double finalSpamProbability = 0;
                double upperFormula = 1;
                double lowerFirstFormula = 1;
                double lowerSecondFormula = 1;
                foreach (AnalyzeHelper analyzedWord in analyzeList)
                {
                    if (wordsPicked < leksemuSkaicius)
                    {
                        upperFormula *= analyzedWord.spamProbability;
                        lowerFirstFormula *= analyzedWord.spamProbability;
                        lowerSecondFormula *= (1 - analyzedWord.spamProbability);
                    }
                    else
                    {
                        finalSpamProbability = (upperFormula / (lowerFirstFormula + lowerSecondFormula));
                        break;
                    }
                    wordsPicked++;
                }
                if (finalSpamProbability >= neutralProbability)
                {
                    truePositiveSpam += finalSpamProbability;
                    truePositiveSpamCount++;
                }
                else
                {
                    falsePositiveSpam += finalSpamProbability;
                    falsePositiveSpamCount++;
                }
                totalSpamAnalyzes++;
                if (finalSpamProbability >= neutralProbability) Console.WriteLine(file + " probability : " + (int)(finalSpamProbability * 100) + " %, Spamas");
                else Console.WriteLine(file + " probability : " + (int)(finalSpamProbability * 100) + " %, Ne spamas");

            }


            //NOT SPAM TESTING
            double truePositiveNotSpam = 0;
            double truePositiveNotSpamCount = 0;
            double falsePositiveNotSpam = 0;
            double falsePositiveNotSpamCount = 0;
            int totalNotSpamAnalyzes = 0;
            foreach (string file in Directory.EnumerateFiles(testingNotSpamPath, "*.txt"))
            {
                List<AnalyzeHelper> analyzeList = new List<AnalyzeHelper>();
                string contents = File.ReadAllText(file);
                string word = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    char character = contents[i];
                    //find words
                    if (character >= 'a' && character <= 'z' || character >= 'A' && character <= 'Z'
                        || character >= '0' && character <= '9' || character == '\'' || character == '$' || character == '"')
                    {
                        word += character;
                    }
                    else if (word != "")
                    {
                        if (!learningTable.ContainsKey(word))
                        {
                            AnalyzeHelper newWord = new AnalyzeHelper(word, newWordSpamProbability, neutralProbability);
                            if (!analyzeList.Any(x => x.word == word)) analyzeList.Add(newWord);
                        }
                        else
                        {
                            LearningCounter wordInfo = learningTable[word];
                            AnalyzeHelper newWord = new AnalyzeHelper(word, wordInfo.spamProbability, neutralProbability);
                            if (!analyzeList.Any(x => x.word == word)) analyzeList.Add(newWord);
                        }
                        word = "";
                    }
                }
                //Sort by difference
                analyzeList = analyzeList.OrderByDescending(x => x.probabilityDifference).ToList();
                int wordsPicked = 0;
                double finalSpamProbability = 0;
                double upperFormula = 1;
                double lowerFirstFormula = 1;
                double lowerSecondFormula = 1;
                foreach (AnalyzeHelper analyzedWord in analyzeList)
                {
                    if (wordsPicked < leksemuSkaicius)
                    {
                        upperFormula *= analyzedWord.spamProbability;
                        lowerFirstFormula *= analyzedWord.spamProbability;
                        lowerSecondFormula *= (1 - analyzedWord.spamProbability);
                    }
                    else
                    {
                        finalSpamProbability = (upperFormula / (lowerFirstFormula + lowerSecondFormula));
                        break;
                    }
                    wordsPicked++;
                }
                if (finalSpamProbability >= neutralProbability)
                {
                    falsePositiveNotSpam += finalSpamProbability;
                    falsePositiveNotSpamCount++;
                }
                else
                {
                    truePositiveNotSpam += finalSpamProbability;
                    truePositiveNotSpamCount++;
                }
                totalNotSpamAnalyzes++;
                if (finalSpamProbability >= neutralProbability) Console.WriteLine(file + " probability : " + (finalSpamProbability * 100) + " %, Spamas");
                else Console.WriteLine(file + " probability : " + string.Format("{0:00.00}", finalSpamProbability * 100) + " %, Ne spamas");

            }
            if (falsePositiveNotSpamCount == 0) falsePositiveNotSpamCount = 1;
            if (falsePositiveSpamCount == 0) falsePositiveSpamCount = 1;
            if (truePositiveNotSpamCount == 0) truePositiveNotSpamCount = 1;
            if (truePositiveSpamCount == 0) truePositiveSpamCount = 1;
            double truePositive = (truePositiveSpamCount / totalSpamAnalyzes + truePositiveNotSpamCount / totalNotSpamAnalyzes) / 2;
            double falsePositive = (falsePositiveSpamCount / totalSpamAnalyzes + falsePositiveNotSpamCount / totalNotSpamAnalyzes) / 2;

            programAccuracy = (truePositive + falsePositive) / 2;
            Console.WriteLine("True positive: " + truePositive);
            Console.WriteLine("False positive: " + falsePositive);
            Console.WriteLine("Tikslumas: " + truePositive * 100 + " %");


            Console.ReadKey();
        }
    }

    public class LearningCounter
    {
        public int spam { get; set; }
        public int notSpam { get; set; }
        public double spamProbability { get; set; }

        public LearningCounter()
        {
            spam = 0;
            notSpam = 0;
            spamProbability = 0;
        }

        public void increaseSpam()
        {
            spam++;
        }

        public void increaseNotSpam()
        {
            notSpam++;
        }

        public void calculateSpamProbability(int totalSpamWords, int totalNotSpamWords)
        {
            double PSW;
            double PWS = spam / (double)totalSpamWords;
            double PWH = notSpam / (double)totalNotSpamWords;
            if (PWH == 0) PSW = 0.99;
            else if (PWS == 0) PSW = 0.01;
            else PSW = PWS / (PWS + PWH);
            spamProbability = PSW;
        }
    }

    public class AnalyzeHelper
    {
        public string word { get; set; }
        public double spamProbability { get; set; }
        public double probabilityDifference { get; set; }

        public AnalyzeHelper(string word_input, double spamProbability_input, double neutralProbability_input)
        {
            word = word_input;
            spamProbability = spamProbability_input;
            probabilityDifference = spamProbability - neutralProbability_input;
            if (probabilityDifference < 0) probabilityDifference = probabilityDifference * -1;
        }
    }

}
