using System;
using System.IO;

namespace Lab03_System_IO
{
    public class Program
    {
        /// <summary>
        /// main method that allows user to play a word guess game or edit text files
        /// </summary>
        /// <param name="args">what the main method needs to run...</param>
        static void Main(string[] args)
        {
            bool runProg = true;
            string easyPath = @"../../../Text_Files/Easy_Text_List.txt";
            string hardPath = @"../../../Text_Files/Hard_Text_List.txt";
            CreateEasyFile(easyPath);
            CreateHardFile(hardPath);
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
                // main menu options
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
                        // admin options
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
                                    Console.WriteLine($"{wordToAdd} successfully added.");
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
            // exit messages
            Console.WriteLine("Thank you for playing! Press any button to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// creates a text file with words for the easy game mode
        /// </summary>
        /// <returns>string declaring whether text file was created</returns>
        public static string CreateEasyFile(string easyPath)
        {
            try
            {
                if (!File.Exists(easyPath) && easyPath.Contains(".txt"))
                {
                    // create file and write text
                    using (StreamWriter sw = File.CreateText(easyPath))
                    {
                        sw.WriteLine("test");
                        sw.WriteLine("file");
                        sw.WriteLine("easy");
                        sw.WriteLine("hello");
                        sw.WriteLine("world");
                        sw.WriteLine("what");
                    }
                    return "file created";
                }
                if (!easyPath.Contains(".txt"))
                    return "not a valid .txt file";

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "failed to create file";
            }
            return "file exists";
        }

        /// <summary>
        /// creates a text file with words for the hard game mode
        /// </summary>
        /// <returns>string declaring whether text file was created</returns>
        public static string CreateHardFile(string hardPath)
        {
            try
            {
                if (!File.Exists(hardPath) && hardPath.Contains(".txt"))
                {
                    // create file and write text
                    using (StreamWriter sw = File.CreateText(hardPath))
                    {
                        sw.WriteLine("pneumonoultramicroscopicsilicovolcanoconiosis");
                        sw.WriteLine("pseudopseudohypoparathyroidism");
                        sw.WriteLine("antidisestablishmentarianism");
                        sw.WriteLine("incomprehensibilities");
                        sw.WriteLine("spectrophotofluorometrically");
                        sw.WriteLine("hepaticocholangiogastrostomy");
                    }
                    return "file created";
                }
                if (!hardPath.Contains(".txt"))
                    return "not a valid .txt file";
            }

            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return "failed to create file";
            }

            return "file exists";
        }

        /// <summary>
        /// reads text files used for the word guess game
        /// </summary>
        /// <param name="FILE_NAME">the file path for the text file</param>
        /// <returns>string indicating whether the file was sucessfully read</returns>
        public static string ReadFile(string FILE_NAME)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(FILE_NAME))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
                return "file read succesfully";
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return "failed to read file";
            }
        }

        /// <summary>
        /// lets the user choose the word list for both playing the game and editing
        /// </summary>
        /// <returns>integer value to be used for other methods</returns>
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

        /// <summary>
        /// random picks a word to be used for the word guess game
        /// </summary>
        /// <param name="FILE_PATH">the text file to be used for choosing a random word</param>
        /// <returns>the random word chosen for the game</returns>
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

        /// <summary>
        /// launches the word guess game
        /// </summary>
        /// <param name="FILE_PATH">the text file used for the game</param>
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
                char[] correctGuesses = new char[30];
                char[] wrongGuesses = new char[5];
                int correctIndex = 0;
                int wrongIndex = 0;
                while (gameDone == false && lives > 0)
                {
                    bool uniqueCharGuessed = false;
                    Console.WriteLine($"\nLives remaining: {lives}.");
                    Console.WriteLine($"Here's your word: {coveredWord}");
                    Console.WriteLine($"Correct letters: {string.Join(' ', correctGuesses)}");
                    Console.WriteLine($"Incorrect letters: {string.Join(' ', wrongGuesses)}");

                    char guessedLetter = GuessLetter();
                    if (!string.Join("", correctGuesses).Contains(guessedLetter.ToString()) && !string.Join("", wrongGuesses).Contains(guessedLetter.ToString()))
                        uniqueCharGuessed = true;
                    while (uniqueCharGuessed == false)
                    {
                        Console.WriteLine("You already guessed that letter!");
                        guessedLetter = GuessLetter();
                                            if (!string.Join("", correctGuesses).Contains(guessedLetter.ToString()) && !string.Join("", wrongGuesses).Contains(guessedLetter.ToString()))
                        uniqueCharGuessed = true;
                    }
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
                        Console.WriteLine("\nCongratulations! You win!");
                        Console.WriteLine($"This was your word: {gameWord}.\n");
                        gameDone = true;
                    }

                    if (lives == 0)
                    {
                        Console.WriteLine("\nI'm sorry, but you lost. :(");
                        Console.WriteLine($"This was your word: {gameWord}.\n");
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

        /// <summary>
        /// takes user input and ensures it is the proper format
        /// </summary>
        /// <returns>the formatted letter to be used for the game</returns>
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

        /// <summary>
        /// displays the admin options
        /// </summary>
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

        /// <summary>
        /// allows user to add a new word to an existing text file
        /// </summary>
        /// <param name="wordToAdd">word to add</param>
        /// <param name="fileToEdit">file to add word to</param>
        /// <returns>string confirming whether the word was successfully added</returns>
        public static string AddWord(string wordToAdd, string fileToEdit)
        {
            string[] strArr = File.ReadAllText(fileToEdit).Trim().Split('\n');
            bool foundMatch = false;
            string properWordToAdd = wordToAdd.ToLower().Trim();
            foreach (var word in strArr)
            {
                if (word.Trim() == properWordToAdd)
                    foundMatch = true;
            }
            try
            {
                if (foundMatch == true)
                {
                    Console.WriteLine("Sorry, but that word already exists!");
                    return "word exists";
                }
                else
                {
                    using (StreamWriter outputFile = new StreamWriter(fileToEdit, true))
                    {
                        outputFile.WriteLine(properWordToAdd);
                        Console.WriteLine($"{properWordToAdd} succesfully added!\n");

                    }
                    return "added word";
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return "failed to add word";
            }
        }

        /// <summary>
        /// allows user to delete a word from an existing text file
        /// </summary>
        /// <param name="wordToDelete">word to delete</param>
        /// <param name="fileToEdit">file to delete text file from</param>
        /// <returns></returns>
        public static string DeleteWord(string wordToDelete, string fileToEdit)
        {
            string[] oldStrArr = File.ReadAllText(fileToEdit).Trim().Split('\n');
            string[] newStrArr = new string[oldStrArr.Length - 1];
            bool foundMatch = false;
            string properWordToDelete = wordToDelete.ToLower().Trim();
            for (int i = 0; i < newStrArr.Length; i++)
            {
                if (oldStrArr[i].Trim() == properWordToDelete)
                    foundMatch = true;
                else
                    newStrArr[i] = oldStrArr[i].Trim();
            }
            try
            {
                if (foundMatch == false)
                {
                    Console.WriteLine("Sorry, but we couldn't find that word in the list.");
                    return "word not found";
                }
                else
                {
                    using (StreamWriter outputFile = new StreamWriter(fileToEdit))
                    {
                        foreach (var word in newStrArr)
                            outputFile.WriteLine(word);

                        Console.WriteLine($"{properWordToDelete} succesfully deleted!");
                    }
                    return "deleted word";
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return "failed to read file";
            }
        }
    }
}