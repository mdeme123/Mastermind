using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public class Mastermind
    {
        private const int NUMDIGITS = 4;   //Amount of digits in an answer
        private const int STARTINGGUESSES = 10;  //Amount of guesses player starts with
        public int[] Answer { get; private set; }

        public int GuessesLeft { get; private set; }
        
        //Make variable so we can easily change the digits.
        private char _correctDigitAndPlace = '+';
        private char _correctDigitOnly = '-';
        private char _WrongDigit = ' ';
        //Basic Constructior
        public Mastermind()
        {
            Answer = GenerateAnswer(NUMDIGITS);
            GuessesLeft = STARTINGGUESSES;
        }

        /// <summary>
        /// Generate a number, taking in the # of digits. 
        /// </summary>
        /// <param name="numDigits">number of digits our answer should be</param>
        /// <returns></returns>
        private int[] GenerateAnswer(int numDigits)
        {
            Random rand = new Random();
            int[] answer = new int[numDigits];

            for (int i = 0; i < numDigits; i++)
            {
                answer[i] = rand.Next(1, 7);   //Generates number between 1-6;
            }

            return answer;
        }

        //Check to see if the string are only digits between the range of 1-6
        private bool IsValidDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '1' || c > '6')
                    return false;
            }

            return true;
        }

        public void PrintRules()
        {
            Console.WriteLine("Welcome To Mastermind!");
            Console.WriteLine("You have " + STARTINGGUESSES + " attempts to guess a " + NUMDIGITS + " digit code, with numbers between 1-6.");
            Console.WriteLine(_correctDigitAndPlace+": means digit in correct spot.");
            Console.WriteLine(_correctDigitOnly+": means it is in the wrong spot.");
            Console.WriteLine(_WrongDigit+": means incorrect guess");
            Console.WriteLine();
            Console.WriteLine("A "+_correctDigitOnly+" will only be printed once per remaining digit (ex: you guess 1111 and answer is 4321, only '"+_correctDigitOnly+"   ' will print");
            Console.WriteLine("Good luck!!!");

            Console.WriteLine();
        }

        public void PlayGame()
        {

            PrintRules();
            bool isCorrect = false;  //Did the player guess the answer?           

            while (GuessesLeft > 0 && isCorrect == false)
            {
                Console.WriteLine($"\nAttempt {GuessesLeft}:");

                bool guessValid = false;
                var guess = "";

                //Make sure we have valid input
                while (guessValid == false)
                {
                    guess = Console.ReadLine();
                    if (guess.Length != NUMDIGITS || !IsValidDigitsOnly(guess))
                    {
                        Console.WriteLine($"Please enter a {NUMDIGITS} digit number with each digit between 1 and 6.");
                        guessValid = false;
                    }

                    else
                    {
                        guessValid = true;
                    }
                }

                //if they got the answer right, break loop
                if (guess == string.Join("", Answer))
                {
                    isCorrect = true;
                    
                    for(int x=0; x<NUMDIGITS;x++)
                    {
                        Console.Write(_correctDigitAndPlace);
                    }
                    Console.WriteLine();
                    break;
                }
                
                //convert guess into array
                int[] guessArray = new int[NUMDIGITS];
                for (int x = 0; x < NUMDIGITS; x++)
                {
                    guessArray[x] = (int)char.GetNumericValue(guess[x]);
                }
                
                GuessesLeft--; //reduce the number of guesses left if we have valid input
                
                char[] outputArray = new char[NUMDIGITS];  //our array to give results to the user 

                List<int> remainingNumbers = new List<int>();  //list of numbers that were not in the correct spot

                //check correct digits first
                for (int x = 0; x < NUMDIGITS; x++)
                {
                    if (guessArray[x] == Answer[x]) //if they match, then add a + to the output array
                    {
                        outputArray[x] = _correctDigitAndPlace;
                    }

                    else  //if they don't match, add number to our list of remaining numbers
                    {
                        remainingNumbers.Add(Answer[x]);
                    }
                }

                //Now check to see if there are any matches but in the wrong place.
                if (remainingNumbers.Count > 0)
                {
                    for (int x = 0; x < NUMDIGITS; x++)
                    {
                        //Make sure we don't already have a match (0 is default value for empty char)
                        if (outputArray[x] == 0)
                        {
                            for (int num = 0; num < remainingNumbers.Count; num++)

                                if (guessArray[x] == remainingNumbers[num])
                                {
                                    outputArray[x] = _correctDigitOnly;
                                    remainingNumbers[num] = 0;  //set value to 0 so we don't check against that number again.
                                    break;
                                }
                        }
                    }
                }

                //replace defaults with our default wrong answer
                for (int x = 0; x < outputArray.Length; x++)
                {
                    if (outputArray[x] == 0)
                    {
                        outputArray[x] = _WrongDigit;

                    }
                }

                //print our value
                foreach (char c in outputArray)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }  //End While Loop

            string answerString = string.Join("", Answer);

            if (isCorrect)
            {
                Console.WriteLine("Congratulations!  You guessed the correct answer: " + answerString);
            }

            else
            {
                Console.WriteLine("You ran out of guesses!  Correct answer: " + answerString);
            }

        }
    }
}
