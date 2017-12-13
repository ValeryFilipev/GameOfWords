using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class GameOfWords : Form
    {
        private List<string> noRepeats = new List<string>();
        Game game = new Game("word_rus.txt");
        Button button;
        private List<object> send = new List<object>();
        private string s;

        public GameOfWords()
        {
            InitializeComponent();
            for (int i = 0; i < game.CurrentWord.Length; i++)
            {
                button = new Button();
                button.Text = game.CurrentWord[i].ToString();
                button.Size = new Size(30, 40);
                button.Location = new Point(104 + i * 43, 70);
                this.Controls.Add(button);
                button.Click += new EventHandler(button_Click);
            }
            s = "";

        }

       
        public void button_Click(object sender, EventArgs e)
        {
            this.textBox1.Text += (sender as Button).Text;
            (sender as Button).Hide();
            send.Add(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your score is " + game.Score.ToString());
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            if (textBox1.Text != "" && textBox1.Text != null)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                (send[send.Count - 1] as Button).Show();
                send.RemoveAt(send.Count - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String UserWord = textBox1.Text.ToString();
            bool flag = game.GamePlay(UserWord);
            if (flag == true)
            {
                listBox1.Items.Add(UserWord);
                listBox1.Items.Add("+" + UserWord.Length);
            }
            else
            {
                listBox1.Items.Add(UserWord);
                listBox1.Items.Add("Sorry, there isn't such word");
            }
            for (int i = 0; i < send.Count;)
            {
                (send[i] as Button).Show();
                send.RemoveAt(i);
            }
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (s.Length > textBox1.TextLength)
            {
                Button button = new Button();
                button.Text = s.Last().ToString();
                send.Add(button);
                s = textBox1.Text;
            }
            s = textBox1.Text;
        }
    }
}
