using System;
using System.Collections.Generic;
using System.Linq;

namespace HangCatGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // מאגר המילים
            string[] wordBank = { "ALGORITHM", "COMPILER", "VARIABLE", "DATABASE", "INTERFACE", "SYNTAX", "COMPUTER" };
            Random random = new Random();
            
            bool keepPlaying = true;

            while (keepPlaying)
            {
                PlayGame(wordBank[random.Next(wordBank.Length)]);
                
                Console.WriteLine("\nWould you like to play again? (Y/N)");
                string response = Console.ReadLine()?.ToUpper();
                keepPlaying = (response == "Y");
            }
        }

        static void PlayGame(string secretWord)
        {
            HashSet<char> guessedLetters = new HashSet<char>();
            int maxWrongGuesses = 7;
            int currentWrongGuesses = 0;
            bool isWon = false;

            while (currentWrongGuesses < maxWrongGuesses && !isWon)
            {
                // ניקוי המסך וציור מחדש
                Console.Clear();
                DisplayHeader();
                
                // כאן התיקון: מציירים את החתול בגודל קבוע
                DrawCatFixed(currentWrongGuesses);

                DisplayWord(secretWord, guessedLetters);
                
                Console.WriteLine($"\n\nGuessed: {string.Join(" ", guessedLetters)}");
                Console.Write("Guess a letter: ");

                string input = Console.ReadLine()?.ToUpper();

                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    continue;
                }

                char guess = input[0];

                if (guessedLetters.Contains(guess))
                {
                    continue; // כבר ניחשו, פשוט ממשיכים
                }

                guessedLetters.Add(guess);

                if (!secretWord.Contains(guess))
                {
                    currentWrongGuesses++;
                }

                if (secretWord.All(c => guessedLetters.Contains(c)))
                {
                    isWon = true;
                }
            }

            // סיום המשחק
            Console.Clear();
            DisplayHeader();
            DrawCatFixed(currentWrongGuesses); // ציור אחרון
            DisplayWord(secretWord, guessedLetters);
            
            Console.WriteLine("\n\n----------------------------------");
            if (isWon)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("VICTORY! You saved the cat!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"GAME OVER. The word was: {secretWord}");
            }
            Console.ResetColor();
            Console.WriteLine("----------------------------------");
        }

        static void DisplayHeader()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("        H A N G   C A T           ");
            Console.WriteLine("==================================");
        }

        static void DisplayWord(string word, HashSet<char> guessed)
        {
            Console.Write("\nWord: ");
            foreach (char c in word)
            {
                if (guessed.Contains(c))
                    Console.Write(c + " ");
                else
                    Console.Write("_ ");
            }
        }

        // פונקציה חדשה שמציירת את העמוד בגובה קבוע
        static void DrawCatFixed(int wrongs)
        {
            Console.WriteLine(); // רווח עליון

            // שורה 1: גג הגרדום
            Console.WriteLine("  _______");

            // שורה 2: הפינה והחבל
            Console.WriteLine("  |/    |");

            // שורה 3: החבל היורד
            if (wrongs > 0) Console.WriteLine("  |     |");
            else            Console.WriteLine("  |      ");

            // שורה 4: הראש
            if (wrongs > 1) Console.WriteLine("  |   (=^.^=)");
            else            Console.WriteLine("  |      ");

            // שורה 5: גוף עליון / ידיים
            if (wrongs > 2) Console.WriteLine("  |     /|\\  ");
            else if (wrongs > 3) Console.WriteLine("  |      |   ");
            else            Console.WriteLine("  |      ");

            // שורה 6: רגליים
            if (wrongs > 4) Console.WriteLine("  |     / \\  ");
            else            Console.WriteLine("  |      ");

            // שורה 7: זנב
            if (wrongs > 5) Console.WriteLine("  |    Tail~ ");
            else            Console.WriteLine("  |      ");

            // שורה 8: בסיס
            Console.WriteLine("__|__");
            
            // שורת מידע על שגיאות
            Console.WriteLine($"Errors: {wrongs} / 7");
        }
    }
}