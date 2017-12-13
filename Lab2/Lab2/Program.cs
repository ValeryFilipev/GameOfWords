using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Lab2
{
    class Vocabulary
    {
        private int length;      //amount word of vocabulary
        public string[] Vocabular;
        public Vocabulary(string filename)
        {
            StreamReader reader = new StreamReader(filename, Encoding.Default);
            Vocabular = System.IO.File.ReadAllLines(filename);
            length = Vocabular.Length;
            reader.Close();
        }
        public string GetRandomWord()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, length);
            return Vocabular[index];//random word 
        }
        public bool IsInVocabulary(string word)
        {
            Array.Sort(Vocabular);
            if (Array.BinarySearch(Vocabular, word) > 0) return true;//return position
            else return false;
        }
        public bool IsCheckingg(string UsersWord, string currentWord)  
        {  
            if (UsersWord.Equals(currentWord)) return false;  
            else return true;  
        }  
    }
    class Game
    {
        static int score;
        Vocabulary vocab;
        public string CurrentWord;
        List<string> lastwords;
        public int Score
        {
            get { return score; }
        }

        public Game(string filename)
        {
            vocab = new Vocabulary(filename);
            CurrentWord = vocab.GetRandomWord();
            score = 0;
            lastwords = new List<string>();
        }
        public bool isChecking(string strUsers, string beginningWord)
        {
            char[] arrayFirstWord = beginningWord.ToCharArray();
            int sum = 0;
            for (int i = 0; i < strUsers.Length; i++)
            {
                for (int j = 0; j < arrayFirstWord.Length; j++)
                {
                    if (strUsers[i].Equals(arrayFirstWord[j]))
                    {
                        arrayFirstWord[j] = '\0';
                        sum++;
                        break;
                    }
                }
            }
            if (sum.Equals(strUsers.Length)) return true;
            return false;
        }
        public bool GamePlay(string word)  
        {
            if (vocab.IsInVocabulary(word) && isChecking(word, CurrentWord) && vocab.IsCheckingg(word, CurrentWord) && !lastwords.Contains(word))
            {  
                SetScore(word);
                lastwords.Add(word);
                return true;  
            }  
            else return false;   
        }
        private void SetScore(string word)
        {
            score += word.Length;
        }
    }
    static class Program
    {
        /* static void Main(string[] args)
         {
             Vocabulary vocabulary = new Vocabulary("word_rus.txt");
             Game game = new Game("word_rus.txt");
             Console.WriteLine("Your word--- " + vocabulary.GetRandomWord());
             bool flag = true;
             while (flag)
             {
                 Console.Write("Enter the word : ");
                 string UsersWord = Console.ReadLine();
                 if (game.GamePlay(UsersWord).Equals(true)) Console.WriteLine("There is!");
                 else Console.WriteLine("No!");
                 Console.WriteLine("It's all? If yes - y, if no - n");
                 char answer = char.Parse(Console.ReadLine());
                 if (answer == 'y') flag = false;
             }
             Console.WriteLine("Your scores is :" + game.Score);
             Console.ReadKey(true);
         }*/
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameOfWords());
        }
    }
}
