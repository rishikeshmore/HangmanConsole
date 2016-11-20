using HangmanConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace HangmanConsole
{
    class Hangman
    {

        // string readText[];
        private string currentword = "";
        private string currentword1 = "";
        private string copycurrentword = "";
        private string[] words;
        private string[] Hints;
        private string hint;
        private int guessIndex;
        List<int> GuessNumbers = new List<int>();
        private int score = 0;
        static private int Highscore = 0;
        static int counter;
        //static int stop = 0;
        static bool muteFlag = true;
        public static int Gradecheck = 0;
        private int wordCount = 0;
        public static int Categorycheck = 0;
        public static string category;
        System.Media.SoundPlayer volume = new System.Media.SoundPlayer();
        string[] readText;


        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Hangman game = new Hangman();
            

            Console.WriteLine("Hello User, Welcome to Hangman");




            //game.SelectCategory();
            while (true)
            {
                category = game.choosecategory();
                
                
                if (category.ToUpper() == "EXIT")
                {
                    Console.WriteLine("GoodBye\n");
                    Environment.Exit(0);
                }
                game.SelectCategory();

                game.game();
            }         





            //Console.WriteLine("Type Exit to Quit game:");
            //if (Console.ReadLine().ToUpper() == "EXIT")
            //{
                
            //}

        }

        private void displaycopycurrentword()
        {
            string labelshowword = "";
            for (int index = 0; index < copycurrentword.Length; index++)
            {
                labelshowword += copycurrentword.Substring(index, 1);
                labelshowword += " ";
            }

            Console.WriteLine(labelshowword);
        }

        public void game()
        {
            bool done = false;
            string guess;
            selectWord();
            int wrongguess = 0;

            while (!done)
            {
                try
                {
                    displaycopycurrentword();
                    Console.WriteLine("Please Type A letter or Hint for HINT:");
                    guess = Console.ReadLine();
                    guess = guess.ToUpper();

                    if (guess == "HINT")
                    {

                        Console.WriteLine(Hints[guessIndex]);
                    }



                    // GuessClick(guess);
                    string currentWordWithoutFirst = currentword.Substring(1);

                    if (currentWordWithoutFirst.Contains(guess))
                    {
                        char[] temp = copycurrentword.ToCharArray();
                        char[] find = currentword.ToCharArray();
                        char guesschar = guess.ElementAt(0);
                        for (int index = 0; index < find.Length; index++)
                        {
                            if (find[index] == guesschar)
                            {
                                temp[index] = guesschar;
                            }
                        }
                        copycurrentword = new string(temp);
                        //                  displaycopycurrentword();
                    }
                    else
                    {
                        //                    displaycopycurrentword();
                        wrongguess++;
                    }

                    if (wrongguess < 6)
                    {
                        switch (wrongguess)
                        {
                            case 0:
                                printStage0();
                                break;
                            case 1:
                                printStage1();
                                break;
                            case 2:
                                printStage2();
                                break;
                            case 3:
                                printStage3();
                                break;
                            case 4:
                                printStage4();
                                break;
                            case 5:
                                printStage5();
                                break;
                        }

                    }
                    else
                    {
                        printStage5();
                        //if (muteFlag == true)
                        //{
                        //    volume.Stream = Hangman.Resource.Game_end_theme;
                        //    volume.Play();
                        //}
                        score = 0;
                        Console.WriteLine("Game Over!! \n The Word was: " + currentword, "Well Done!");
                        done = true;
                        // choosecategory();
                    }
                    if (copycurrentword.Equals(currentword))
                    {
                        score += 100;
                        Console.WriteLine("You Won!! \n Score: " + score + "\n The word was: " + currentword, "Well Done!");
                        UpdateScore();
                        done = true;
                        // choosecategory();
                    }
                }
                catch
                {
                    Console.WriteLine("Sorry didn't get that please type it again");
                    continue;
                }
            }
        }



        private void GuessClick(String guess)
        {
                    }

        public void UpdateScore()
        {
            if (score >= Highscore)
            {
                Highscore = score;
            }
            Console.WriteLine("Score: " + Convert.ToString(score));
            // label_highscore.Text = "Highscore: " + Highscore;
        }


        public string choosecategory()
        {
            
            Console.WriteLine("Let's Play!!!");
            Console.WriteLine("Type a difficulty level:\n1) Easy\n2) Medium\n3) Hard\n or type EXIT to Quit.");
            return Console.ReadLine();
        }

        public void SelectCategory()
        {
            string read = Resource1.HangmanWords; 
            string[] readText = read.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            words = new string[readText.Length / 3];
            Hints = new string[readText.Length / 3];

            // words = new string[readText.Length];
            // Hints = new string[readText.Length];
            int flag = 0;
            
            foreach (string s in readText)
            {
                string[] line = s.Split(',');
                if (category.ToUpper() == "EASY" && line[2] == "1")
                {
                    // wordCount++;
                    words[flag] = line[0];
                    Hints[flag++] = line[3];
                }
                if (category.ToUpper() == "MEDIUM" && line[2] == "2")
                {
                    // wordCount++;
                    words[flag] = line[0];
                    Hints[flag++] = line[3];
                }
                if (category.ToUpper() == "HARD" && line[2] == "2")
                {
                    // wordCount++;
                    words[flag] = line[0];
                    Hints[flag++] = line[3];
                }
                
            }
        }

        private int guessNext()
        {
            return new Random().Next(words.Length);
        }

        public void selectWord()
        {
            //if (GuessNumbers.Count == wordCount)
            //{
            //    throw new Exception();
            //}

            guessIndex = guessNext();

            //while (GuessNumbers.Contains(guessIndex))
            //{
            //    guessIndex = guessNext();
            //}
            //GuessNumbers.Add(guessIndex);

            //wrongguess = 0;
            //Imagebox1.Image = hang_images[wrongguess];

            hint = Convert.ToString(Hints[guessIndex]);
            currentword1 = Convert.ToString(words[guessIndex]);
            currentword = currentword1.ToUpper();
            currentword =  currentword.Replace(" ", "");
            copycurrentword = currentword.ToCharArray()[0].ToString();
            
            for (int index = 1; index < currentword.Length; index++)
            {
                copycurrentword += "_";
            }

            //displaycopycurrentword();
            wordCount = 0;
            // GuessNumbers = new List<int>();

        }

        public void printStage0()
        {
            Console.WriteLine("  ____   ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |       ");
            Console.WriteLine(" |       ");
            Console.WriteLine(" |       ");
            Console.WriteLine("_|___    \n");
        }

        public void printStage1()
        {
            Console.WriteLine("  ____   ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |    O  ");
            Console.WriteLine(" |       ");
            Console.WriteLine(" |       ");
            Console.WriteLine("_|___    \n");

        }

        public void printStage2()
        {
            Console.WriteLine("  ____   ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |   _O_ ");
            Console.WriteLine(" |       ");
            Console.WriteLine(" |       ");
            Console.WriteLine("_|___    \n");
        }



        public void printStage3()
        {
            Console.WriteLine("  ____   ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |   _O_ ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |       ");
            Console.WriteLine("_|___    \n");
        }

        public void printStage4()
        {
            Console.WriteLine("  ____   ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |   _O_ ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |   /   ");
            Console.WriteLine("_|___    \n");
        }

        public void printStage5()
        {
            Console.WriteLine("  ____   ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |   _O_ ");
            Console.WriteLine(" |    |  ");
            Console.WriteLine(" |   / \\ ");
            Console.WriteLine("_|___    \n");
        }

    }





}