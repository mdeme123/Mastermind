using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Mastermind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool cheat = false;  //turn to true if you want the answer printed at top!           

            Mastermind mastermind = new Mastermind();

            if (cheat == true)
            {
                Console.WriteLine("Correct Answer:" + string.Join("", mastermind.Answer));
                Console.WriteLine();
                Console.WriteLine();
            }
            
            mastermind.PlayGame();   
        }
    }
}

