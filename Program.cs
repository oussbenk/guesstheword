using System;

/// <summary>
/// @author Oussama Ben Khiroun
/// @version 1.0
/// </summary>
namespace GuessTheWord
{
    class Program
    {
        static void Main(string[] args)
        {

            // a styled game name with ASCII art (see : https://patorjk.com/software/taag/#p=display&f=Graffiti&t=Guess%20the%20word )
            string gameName = @" 
  ________                                __  .__                                     .___
 /  _____/ __ __   ____   ______ ______ _/  |_|  |__   ____   __  _  _____________  __| _/
/   \  ___|  |  \_/ __ \ /  ___//  ___/ \   __\  |  \_/ __ \  \ \/ \/ /  _ \_  __ \/ __ | 
\    \_\  \  |  /\  ___/ \___ \ \___ \   |  | |   Y  \  ___/   \     (  <_> )  | \/ /_/ | 
 \______  /____/  \___  >____  >____  >  |__| |___|  /\___  >   \/\_/ \____/|__|  \____ | 
        \/            \/     \/     \/             \/     \/                           \/                                                                     
";
            Console.WriteLine(gameName);

            /*  Read words list from text file.
                "words.txt" should be placed in same folder of generated ".exe" program.
                Each line should contain a single word.
            */
            string[] wordsDictionary = System.IO.File.ReadAllLines("words.txt");
            
            /*foreach(string w in wordsDictionary)
            {
                Console.WriteLine(w);
            }*/

            // Select a random index
            Random randomGenerator = new Random();

            // Next method have min and max attributes (min value is included, max is excluded;  [min, max[)
            int randomIndex = randomGenerator.Next(0, wordsDictionary.Length);

            string secretWord = wordsDictionary[randomIndex];

            // Console.WriteLine(secretWord);

            char[] guessMe = new char[secretWord.Length];   // Guessed word should have the same length of secretWord
            // Should use an array of chars because string type is immutable in C# (string cannot be changed after it has been created)

            // Hide all chars with stars
            for (int i = 0; i < guessMe.Length; i++)
            {
                guessMe[i] = '*';
            }

            int roundNumber = 1;

            while (!TestWinGame(guessMe, secretWord))
            {
                Console.WriteLine("\n== Round " + roundNumber + " ==");
                Console.WriteLine(guessMe); // Write the guessed word with hidden chars

                Console.Write("Type a letter :");
                char letter = char.Parse(Console.ReadLine().Substring(0,1));

                for (int i = 0; i < secretWord.Length; i++)
                {
                    if(letter == secretWord[i])
                    {
                        guessMe[i] = letter;    // Update revealed letters in guessed word
                    }
                }

                roundNumber++;
            }


            string congratulations = @"  
 ____________________________________
(__   __________________________   __)
   | |                          | |
   | |      Congratulations     | |
 __| |__________________________| |__
(____________________________________)
";
            Console.WriteLine(congratulations);
            Console.WriteLine("The secret word was " + secretWord);
            Console.WriteLine("Found in " + (roundNumber - 1) + " rounds");

            Console.ReadKey(); // Wait for any key before closing application
        }

        /// <summary>
        ///  This method compares a string with an array of chars
        /// </summary>
        /// <param name="charsArray"></param>
        /// <param name="secret"></param>
        /// <returns>true if string is equal to array chars, else false</returns>
        private static bool TestWinGame(char[] charsArray, string secret)
        {
            for (int i = 0; i < secret.Length; i++)
            {
                if(secret[i] != charsArray[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
