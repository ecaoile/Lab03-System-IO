using System;
using System.IO;

namespace Lab03_System_IO
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runProg = true;
            string easyPath = CreateEasyFile();
            CreateHardFile();
            Console.WriteLine("Welcome to Lab 03!\n");
            while (runProg == true)
            {
                HomeView();
                string userChoice = Console.ReadLine();

                while (userChoice != "1" && userChoice != "2" && userChoice != "3" && userChoice != "4")
                {
                    Console.WriteLine("You did not choose a valid option. Please choose a number between 1 and 4");
                    userChoice = Console.ReadLine();
                }
                switch (userChoice)
                {
                    case "1":
                        //ReadEasyFile(easyPath);
                        PlayGame(easyPath);
                        break;
                    case "2":
                        AdminView();
                        break;
                    case "3":
                        runProg = false;
                        break;
                    default:
                        break;
                }
            }
            
            Console.WriteLine("Thank you for playing! Press any button to exit.");
            Console.ReadLine();
        }


        public static string CreateEasyFile()
        {
            string easyPath = @"../../../Text_Files/Easy_Text_List.txt";
            if (!File.Exists(easyPath))
            {
                // create file and write text
                using (StreamWriter sw = File.CreateText(easyPath))
                {
                    sw.WriteLine("test");
                    sw.WriteLine("file");
                    sw.WriteLine("easy");
                    sw.WriteLine("hello");
                    sw.WriteLine("world");
                }
            }
            Console.WriteLine("Easy mode file created!");
            return easyPath;
        }

        public static void ReadEasyFile(string FILE_NAME)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(FILE_NAME))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public static void CreateHardFile()
        {
            string hardPath = @"../../../Text_Files/Hard_Text_List.txt";
            if (!File.Exists(hardPath))
            {
                // create file and write text
                using (StreamWriter sw = File.CreateText(hardPath))
                {
                    sw.WriteLine("pneumonoultramicroscopicsilicovolcanoconiosis");
                    sw.WriteLine("pseudopseudohypoparathyroidism");
                    sw.WriteLine("antidisestablishmentarianism");
                    sw.WriteLine("incomprehensibilities");
                }
            }
            Console.WriteLine("Hard mode file created!");
        }
        
        public static string PickWord(string FILE_PATH)
        {
            string gameWord;
            try
            {  
                string[] strArr = File.ReadAllText(FILE_PATH).Trim().Split('\n');
                //foreach (var word in strArr)
                //{
                //    Console.WriteLine(word);
                //}
                Random rnd1 = new Random();
                int r = rnd1.Next(strArr.Length);
                  
                Console.WriteLine($"{strArr.Length} is your array length.");
                Console.WriteLine($"{strArr[r]} is your random word.");
                gameWord = strArr[r];
                return gameWord;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return "fail";
        }
        public static void HomeView()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. play a word guessing game");
            Console.WriteLine("2. admin access");
            Console.WriteLine("3. exit");
        }

        public static void PlayGame(string FILE_PATH)
        {
            Console.WriteLine("playing GAEMZ!");
            string gameWord = PickWord(FILE_PATH).Trim();
            char[] wordArr = new char[gameWord.Length];
            for (int i = 0; i < wordArr.Length; i++)
                wordArr[i] = '*';
            string coveredWord = string.Join("", wordArr);
            bool gameDone = false;
            int lives = 3;
            Console.WriteLine($"Using the word {gameWord}, but don't tell anyone. Sssh!");
            
            while (gameDone == false || lives > 0)
            {
                Console.WriteLine($"Here's your word: {coveredWord}");
                char guessedLetter = GuessLetter();
                if (gameWord.Contains(guessedLetter.ToString()))
                {
                    Console.WriteLine("That is correct!");
                    for (int i = 0; i < gameWord.Length; i++)
                    {
                        if (gameWord[i] == guessedLetter)
                            wordArr[i] = guessedLetter;
                    }
                    coveredWord = string.Join("", wordArr);
                }
                else
                {
                    Console.WriteLine("That is incorrect!");
                    lives--;
                }
                
                if (coveredWord == gameWord)
                {
                    Console.WriteLine("\nCongratulations! You win!\n");
                    gameDone = true;
                }

                if (lives == 0)
                {
                    Console.WriteLine("\nI'm sorry, but you lost. :(\n");
                    gameDone = true;
                }
            }
        }

        public static char GuessLetter()
        {
            Console.WriteLine("Guess a letter.");
            string letterGuess = Console.ReadLine();
            while (letterGuess.Length > 1 || letterGuess.Length < 1)
            {
                Console.WriteLine("One letter only please!");
                letterGuess = Console.ReadLine();
            }
            return Convert.ToChar(letterGuess);
        }

        public static void AdminView()
        {
            Console.WriteLine("\nlooking at dem ADMIN VIEWZ!");
        }
    }
}
