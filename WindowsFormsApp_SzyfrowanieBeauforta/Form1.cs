using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_SzyfrowanieBeauforta
{
    public partial class Form1 : Form
    {
        Dictionary<char, int> dictionaryLettersNumbers;
        Dictionary<int, char> dictionaryNumbersLetters;
        int numberOfChars;

        Visualizator cipherAlgorithmVisualizator;

        public Form1()
        {
            InitializeComponent();
            initializeDictionaries();
            cipherAlgorithmVisualizator = new Visualizator();
        }

        private void initializeDictionaries()
        {
            dictionaryLettersNumbers = new Dictionary<char, int>();
            dictionaryNumbersLetters = new Dictionary<int, char>();

            char[] allowedCharacters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                                        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                                        'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
                                        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
                                        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                                        'Y', 'Z', 'ą', 'ć', 'ę', 'ł', 'ń', 'ó', 'ś', 'ź',
                                        'ż', 'Ą', 'Ć', 'Ę', 'Ł', 'Ń', 'Ó', 'Ś', 'Ź', 'Ż',
                                        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                        ',', '.', ':', ';', '"', '?', '!', '*', '@', '#',
                                        '$', '%', '^', '&', '/', '|', '(', ')', '{', '}',
                                        '[', ']', '<', '>', ' ', '\\', '\r', '\t', '\n' };
            
            numberOfChars = allowedCharacters.Length;

            for (int i = 0; i < numberOfChars; i++)
            {
                dictionaryLettersNumbers.Add(allowedCharacters[i], i);
                dictionaryNumbersLetters.Add(i, allowedCharacters[i]);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = System.Drawing.Color.Aquamarine;
            textBox2.BackColor = System.Drawing.Color.White;
            textBox3.BackColor = System.Drawing.Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = System.Drawing.Color.White;
            textBox2.BackColor = System.Drawing.Color.Aquamarine;
            textBox3.BackColor = System.Drawing.Color.White;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cipherOrDecipherText(textBox1.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private char cipherOrDecipherOneLetter(char textLetter, char keyLetter)
        {
            // Algorytm szyfrujacy i deszyfrujacy sa dokladnie takie same, C = E(M) = (K - M) mod 26
            // Pierwszy krok: (K - M)
            int resultLetterNumber = dictionaryLettersNumbers[keyLetter] - dictionaryLettersNumbers[textLetter];
            // drugi krok: mod 26
            resultLetterNumber %= numberOfChars;

            // Korekcja modulo (jeśli wynik jest ujemny)
            if (resultLetterNumber < 0)
                resultLetterNumber += numberOfChars;

            return dictionaryNumbersLetters[resultLetterNumber];
        }

        private void cipherOrDecipherText(string textMessage, string keyText)
        {
            string resultText = "";

            for(int i = 0; i < textMessage.Length; i++)
            {
                resultText += cipherOrDecipherOneLetter(textMessage[i], keyText[i % keyText.Length]);
            }

            textBox3.Text = resultText;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cipherAlgorithmVisualizator.loadLabelIntoForm(this);
        }


        // Ponizej ToDel

        /*
        private void colourColumnA(System.Drawing.Color colour)
        {
            label26.BackColor = colour;
            label27.BackColor = colour;
            label28.BackColor = colour;
            label31.BackColor = colour;
            label32.BackColor = colour;
            label33.BackColor = colour;
            label34.BackColor = colour;
            label35.BackColor = colour;
            label36.BackColor = colour;
            label37.BackColor = colour;
            label38.BackColor = colour;
            label39.BackColor = colour;
            label40.BackColor = colour;
            label41.BackColor = colour;
            label42.BackColor = colour;
            label43.BackColor = colour;
            label44.BackColor = colour;
            label45.BackColor = colour;
            label46.BackColor = colour;
            label47.BackColor = colour;
            label48.BackColor = colour;
            label49.BackColor = colour;
            label50.BackColor = colour;
            label51.BackColor = colour;
            label52.BackColor = colour;
            label53.BackColor = colour;
            label54.BackColor = colour;
        }

        private void colourColumnB(System.Drawing.Color colour)
        {
            label55.BackColor = colour;
            label56.BackColor = colour;
            label57.BackColor = colour;
            label58.BackColor = colour;
            label59.BackColor = colour;
            label60.BackColor = colour;
            label61.BackColor = colour;
            label62.BackColor = colour;
            label63.BackColor = colour;
            label64.BackColor = colour;
            label65.BackColor = colour;
            label66.BackColor = colour;
            label67.BackColor = colour;
            label68.BackColor = colour;
            label69.BackColor = colour;
            label70.BackColor = colour;
            label71.BackColor = colour;
            label72.BackColor = colour;
            label73.BackColor = colour;
            label74.BackColor = colour;
            label75.BackColor = colour;
            label76.BackColor = colour;
            label77.BackColor = colour;
            label78.BackColor = colour;
            label79.BackColor = colour;
            label80.BackColor = colour;
            label81.BackColor = colour;
            label54.BackColor = colour;
        }

        private void colourColumnC(System.Drawing.Color colour)
        {
            label82.BackColor = colour;
            label83.BackColor = colour;
            label84.BackColor = colour;
            label85.BackColor = colour;
            label86.BackColor = colour;
            label87.BackColor = colour;
            label88.BackColor = colour;
            label89.BackColor = colour;
            label90.BackColor = colour;
            label91.BackColor = colour;
            label92.BackColor = colour;
            label93.BackColor = colour;
            label94.BackColor = colour;
            label95.BackColor = colour;
            label96.BackColor = colour;
            label97.BackColor = colour;
            label98.BackColor = colour;
            label99.BackColor = colour;
            label100.BackColor = colour;
            label101.BackColor = colour;
            label102.BackColor = colour;
            label103.BackColor = colour;
            label104.BackColor = colour;
            label105.BackColor = colour;
            label106.BackColor = colour;
            label107.BackColor = colour;
            label108.BackColor = colour;
        }

        private void colourColumnD(System.Drawing.Color colour)
        {
            label109.BackColor = colour;
            label110.BackColor = colour;
            label111.BackColor = colour;
            label112.BackColor = colour;
            label113.BackColor = colour;
            label114.BackColor = colour;
            label115.BackColor = colour;
            label116.BackColor = colour;
            label117.BackColor = colour;
            label118.BackColor = colour;
            label119.BackColor = colour;
            label120.BackColor = colour;
            label121.BackColor = colour;
            label122.BackColor = colour;
            label123.BackColor = colour;
            label124.BackColor = colour;
            label125.BackColor = colour;
            label126.BackColor = colour;
            label127.BackColor = colour;
            label128.BackColor = colour;
            label129.BackColor = colour;
            label130.BackColor = colour;
            label131.BackColor = colour;
            label132.BackColor = colour;
            label133.BackColor = colour;
            label134.BackColor = colour;
            label135.BackColor = colour;
        }
        */
    }
}
