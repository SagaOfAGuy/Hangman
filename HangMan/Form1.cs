using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Windows;
using System.Resources; 

namespace HangMan
{
    public partial class Form1 : Form
    {
        String[] words = { "matrix", "founder", "restart", "column", "integer", "vernacular", "wendigo", "incite", "metaphysical", "eidetic", "magniloquent", "avaricious", "loquacious", "redaction", "alternative", "corporation" };
        
        static Random randomseed = new Random();
        SoundPlayer error = new SoundPlayer(Properties.Resources.error);
        SoundPlayer clap = new SoundPlayer(Properties.Resources.clapping);
        SoundPlayer ding = new SoundPlayer(Properties.Resources.ding);
        Image[] hangImages = new Image[7] { Properties.Resources.hang1, 
            Properties.Resources.hang2,
            Properties.Resources.hang3,
            Properties.Resources.hang4,
            Properties.Resources.hang5,
            Properties.Resources.hang6,
            Properties.Resources.hang7,
             }; 
        int randomNumber;
        int counter = 0; 
        String asteriskText = "";
        int secondCounter = 0; 
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void start()
        {
            randomNumber = randomseed.Next(words.Length);
            for (int i = 0; i < words[randomNumber].Length; i++)
            {
                asteriskText += "*";
            }
            label1.Text = asteriskText;
            pictureBox1.Image = hangImages[0];
            counter = 0;
            textBox1.Text = "";
            label3.Text = "Used Letters:";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int wordLength = words[randomNumber].Length;
            Boolean letterFound = false;
            int num = 0;
            label3.Text += $" {textBox1.Text}";
            
          
            
            for (int v = 0; v < wordLength; v++)
                {
                    if (textBox1.Text.Equals(words[randomNumber].Substring(v, 1).ToLower()))
                    {
                        letterFound = true;
                        num = v;
                        //string match1 = words[randomNumber].Substring(num, 1);
                        string changedText = asteriskText.Remove(num, 1).Insert(num, textBox1.Text);
                        asteriskText = changedText;
                    }
                }
            if (letterFound)
            {
                label4.Text = "Yes! " + textBox1.Text + " is in the word!";
                label1.Text = asteriskText;
                ding.Play();
                if (label1.Text.Equals(words[randomNumber]))
                {
                    clap.Play();
                    MessageBox.Show("You Win!");
                    label1.Text = "";
                    asteriskText = "";
                    start();
                }
            }
            else
            {
                counter += 1;
                if (counter < 7)
                {
                    label4.Text = "No! " + textBox1.Text + " is not in the word! Please try again!";
                    pictureBox1.Image = hangImages[counter];
                    error.Play();
                }
                else
                {
                    MessageBox.Show($"You Lose! The correct word was {words[randomNumber]}");

                    label1.Text = "";
                    asteriskText = "";
                    start();
                }
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            start();
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clap.Stop();
            label1.Text = "";
            asteriskText = "";
            start();
            


        }
    }
}
