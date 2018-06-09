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
            string hardPath = CreateHardFile();
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
                        string chosenPath = ChooseDifficulty();
                        if (chosenPath == "1")
                            PlayGame(easyPath);
                        else if (chosenPath == "2")
                            PlayGame(hardPath);
                        else
                            Console.Clear();
                        break;
                    case "2":
                        AdminView();
                        string adminChoice = Console.ReadLine();

                        switch (adminChoice)
                        {
                            case "1":
                                Console.Clear();
                                string fileNum = ChooseDifficulty();
                                string fileToEdit = "";
                                if (fileNum == "1")
                                    fileToEdit = easyPath;
                                if (fileNum == "2")
                                    fileToEdit = hardPath;

                                if (fileNum == "1" || fileNum == "2")
                                {
                                    Console.WriteLine("List to edit:");
                                    ReadFile(fileToEdit);
                                    Console.WriteLine("Which word would you like to add?");
                                    string wordToAdd = Console.ReadLine().ToLower();
                                    AddWord(wordToAdd, fileToEdit);
                                    Console.WriteLine("Your word list now looks like this:");
                                    ReadFile(fileToEdit);
                                }
                                else
                                    Console.Clear();
                                break;
                            case "2":
                                Console.Clear();
                                fileNum = ChooseDifficulty();
                                fileToEdit = "";
                                if (fileNum == "1")
                                    fileToEdit = easyPath;
                                if (fileNum == "2")
                                    fileToEdit = hardPath;

                                if (fileNum == "1" || fileNum == "2")
                                {
                                    Console.WriteLine("List to edit:");
                                    ReadFile(fileToEdit);
                                    Console.WriteLine("Which word would you like to delete?");
                                    string wordToDelete = Console.ReadLine().ToLower();

                                    DeleteWord(wordToDelete, fileToEdit);
                                }
                                else
                                    Console.Clear();
                                break;
                            case "3":
                                Console.Clear();
                                fileNum = ChooseDifficulty();
                                fileToEdit = "";
                                if (fileNum == "1")
                                {
                                    fileToEdit = easyPath;
                                    Console.WriteLine("\nEasy Mode List");
                                }
                                if (fileNum == "2")
                                {
                                    fileToEdit = hardPath;
                                    Console.WriteLine("\nHard Mode List");
                                }

                                ReadFile(fileToEdit);
                                break;
                            case "4":
                                break;
                            default:
                                break;
                        }
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

        public static void ReadFile(string FILE_NAME)
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
        public static string CreateHardFile()
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
            return hardPath;
        }
        
        public static string ChooseDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Choose word list:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Hard");
            Console.WriteLine("3. Neither (return to main menu)");
            string chosenPath = Console.ReadLine();
            while (chosenPath != "1" && chosenPath != "2" && chosenPath != "3")
            {
                Console.WriteLine("That wasn't one of the options! Please try again.");
                chosenPath = Console.ReadLine();
            }
            if (chosenPath == "2")
                return "2";
            else if (chosenPath == "3")
                return "3";
            else
                return "1";
        }
        public static string PickWord(string FILE_PATH)
        {
            string gameWord;
            try
            {  
                string[] strArr = File.ReadAllText(FILE_PATH).Trim().Split('\n');
                Random rnd1 = new Random();
                int r = rnd1.Next(strArr.Length);
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
            Console.WriteLine("Main Menu");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. play a word guessing game");
            Console.WriteLine("2. admin access");
            Console.WriteLine("3. exit");
        }

        public static void PlayGame(string FILE_PATH)
        {
            Console.Clear();
            Console.WriteLine("Playing GAEMZ!");
            try
            {
                string gameWord = PickWord(FILE_PATH).Trim();
                char[] wordArr = new char[gameWord.Length];
                for (int i = 0; i < wordArr.Length; i++)
                    wordArr[i] = '*';
                string coveredWord = string.Join("", wordArr);
                bool gameDone = false;
                int lives = 5;
                char[] correctGuesses = new char[5];
                char[] wrongGuesses = new char[5];
                int correctIndex = 0;
                int wrongIndex = 0;
                while (gameDone == false && lives > 0)
                {
                    Console.WriteLine($"\nLives remaining: {lives}.");
                    Console.WriteLine($"Here's your word: {coveredWord}");
                    Console.WriteLine($"Correct letters: {string.Join(' ', correctGuesses)}");
                    Console.WriteLine($"Incorrect letters: {string.Join(' ', wrongGuesses)}");

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
                        correctGuesses[correctIndex] = guessedLetter;
                        correctIndex++;
                    }

                    else
                    {
                        Console.WriteLine("That is incorrect!");
                        wrongGuesses[wrongIndex] = guessedLetter;
                        wrongIndex++;
                        lives--;
                    }

                    if (coveredWord == gameWord)
                    {
                        Console.WriteLine("\nCongratulations! You win!\n");
                        Console.WriteLine($"The word was {gameWord}\n");
                        gameDone = true;
                    }

                    if (lives == 0)
                    {
                        Console.WriteLine("\nI'm sorry, but you lost. :(");
                        Console.WriteLine($"The word was {gameWord}\n");
                        gameDone = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        public static char GuessLetter()
        {
            Console.WriteLine("Guess a letter.");
            string letterGuess = Console.ReadLine();
            if (letterGuess.Length > 1)
                Console.WriteLine("One letter only please!");
            
            if (letterGuess.Length == 0)
                Console.WriteLine("You didn't enter anything! Please try again.");

            while (letterGuess.Length > 1 || letterGuess.Length == 0)
                letterGuess = Console.ReadLine();

            return Convert.ToChar(letterGuess.ToLower());
        }

        public static void AdminView()
        {
            Console.Clear();
            Console.WriteLine("Admin Options");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add a word to a file.");
            Console.WriteLine("2. Remove words from a text file.");
            Console.WriteLine("3. View text file.");
            Console.WriteLine("4. Return to main menu.");

        }

        public static void AddWord(string wordToAdd, string filetoEdit)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(filetoEdit, true))
                {
                    outputFile.WriteLine(wordToAdd);
                    Console.WriteLine($"{wordToAdd} succesfully added!\n");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void DeleteWord(string wordToDelete, string fileToEdit)
        {
            string[] oldStrArr = File.ReadAllText(fileToEdit).Trim().Split('\n');
            string[] newStrArr = new string[oldStrArr.Length-1];
            bool foundMatch = false;
            for (int i = 0; i < newStrArr.Length; i++)
            {
                if (oldStrArr[i] != wordToDelete)
                    newStrArr[i] = oldStrArr[i];
                else
                    foundMatch = true;
            }
            try
            {
                if (foundMatch == false)
                    Console.WriteLine("Sorry, but we couldn't find that word in the list.");
                else
                {
                    using (StreamWriter outputFile = new StreamWriter(fileToEdit))
                    {
                        foreach (var word in newStrArr)
                            outputFile.WriteLine(word);

                        Console.WriteLine($"{wordToDelete} succesfully deleted!");
                        Console.WriteLine("New word list:");
                        ReadFile(fileToEdit);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
