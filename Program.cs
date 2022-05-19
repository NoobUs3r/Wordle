using System;
using System.Collections.Generic;

namespace Wordle
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string rapidApiHostUrl = "https://wordsapiv1.p.rapidapi.com/words/?";
            string rapidApiHostToken = ""; // Add RapidAPI WordsAPI token here
            string word = string.Empty;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            bool isApiPlay = rapidApiHostToken != string.Empty;

            if (isApiPlay)
            {
                Console.WriteLine("Searching for a word...");
                headers.Add("X-RapidAPI-Host", "wordsapiv1.p.rapidapi.com");
                headers.Add("X-RapidAPI-Key", rapidApiHostToken);
                parameters.Add("random", "true");
                parameters.Add("letterPattern", "^[A-Za-z]{5}$");
                parameters.Add("frequencymin", "8.03");

                dynamic parsedApiResponse = await ApiCall.ApiCallAsync(rapidApiHostUrl, headers, parameters);
                word = parsedApiResponse.word ?? string.Empty;

                if (word == string.Empty)
                {
                    Console.WriteLine("API call doesn't return any word.");
                    return;
                }

                Console.WriteLine("A word is found!\n");
            }
            else
            {
                word = NoApiPlay.GetRandom5LetterWord();
            }

            Console.WriteLine("Guess a 5 letter word!");
            Console.WriteLine("Type \"q\" to exit, \"show word\" to show word.");
            Console.WriteLine("Begin!\n");
            word = word.ToLower();

            while (true)
            {
                string userInput = Console.ReadLine().ToLower();
                string lettersCheck = word;

                if (userInput == "q")
                    return;
                else if (userInput == "show word")
                {
                    Console.WriteLine(word);
                    continue;
                }
                else if (userInput.Length != 5)
                {
                    Console.WriteLine("Input must be 5 chars long!");
                    continue;
                }

                if (isApiPlay)
                {
                    rapidApiHostUrl = "https://wordsapiv1.p.rapidapi.com/words/" + userInput;
                    dynamic parsedApiResponse = await ApiCall.ApiCallAsync(rapidApiHostUrl, headers, new Dictionary<string, string>());
                    bool isWordFound = parsedApiResponse.word == null ? false : true;

                    if (!isWordFound)
                    {
                        Console.WriteLine("Input word not found!");
                        continue;
                    }
                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);

                for (int i = 0; i < 5; i++)
                {
                    char userInputChar = userInput[i];
                    int foundCharIndex = lettersCheck.IndexOf(userInputChar);

                    if (foundCharIndex == i)
                    {
                        lettersCheck = MarkCharOnIndexAsUsed(lettersCheck, i);
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (foundCharIndex != -1)
                    {
                        if (!IsCharOnIndexGreen(userInput, word, foundCharIndex))
                        {
                            lettersCheck = MarkCharOnIndexAsUsed(lettersCheck, foundCharIndex);
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                    }

                    Console.Write(userInputChar);
                    Console.ResetColor();
                }

                Console.WriteLine();

                if (userInput == word)
                {
                    Console.WriteLine("You Won!");
                    return;
                }
            }
        }

        private static bool IsCharOnIndexGreen(string input, string word, int index)
        {
            return input[index] == word[index];
        }

        private static string MarkCharOnIndexAsUsed(string str, int index)
        {
            int strLength = str.Length;
            return str.Substring(0, index) + '$' + str.Substring(index + 1, strLength - 1 - index);
        }
    }
}
