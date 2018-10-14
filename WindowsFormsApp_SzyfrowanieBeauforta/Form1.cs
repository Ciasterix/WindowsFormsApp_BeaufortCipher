using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp_SzyfrowanieBeauforta
{
    public partial class Form1 : Form
    {
        Dictionary<char, int> dictionaryLettersNumbers;
        Dictionary<int, char> dictionaryNumbersLetters;
        int numberOfChars;

        Visualizator cipherAlgorithmVisualizator;
        bool stepByStepActivated;
        int numberOfCharsDone;

        public Form1()
        {
            InitializeComponent();
            initializeDictionaries();

            cipherAlgorithmVisualizator = new Visualizator();
            stepByStepActivated = false;
            numberOfCharsDone = 0;
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
                                        '[', ']', '<', '>', '-', '_', ' ', '+', '=',
                                        '\\', '\'', '\r', '\t', '\n' };
            
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

            numberOfCharsDone = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = System.Drawing.Color.White;
            textBox2.BackColor = System.Drawing.Color.Aquamarine;
            textBox3.BackColor = System.Drawing.Color.White;

            numberOfCharsDone = 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // Przycisk Szyfruj
        private void button1_Click(object sender, EventArgs e)
        {
            cipherOrDecipherText(ref textBox3, textBox1.Text, textBox2.Text);
        }

        // Przycisk Deszyfruj
        private void button2_Click(object sender, EventArgs e)
        {
            cipherOrDecipherText(ref textBox1, textBox3.Text, textBox2.Text);
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

        private void cipherOrDecipherText(ref TextBox resultTextBox, string textMessage, string keyText)
        {
            string resultText = "";

            for(int i = 0; i < textMessage.Length; i++)
            {
                resultText += cipherOrDecipherOneLetter(textMessage[i], keyText[i % keyText.Length]);
            }

            resultTextBox.Text = resultText;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cipherAlgorithmVisualizator.loadLabelIntoForm(this);
            cipherAlgorithmVisualizator.setVisibilityForAllLabels(false);
        }

        // Przycisk Wykonywanie krokowe
        private void button3_Click(object sender, EventArgs e)
        {
            if (stepByStepActivated == false)
            {
                stepByStepActivated = true;
                button3.BackColor = System.Drawing.Color.DodgerBlue;
                cipherAlgorithmVisualizator.unmarkAll();
                cipherAlgorithmVisualizator.setVisibilityForAllLabels(true);
            }
            else
            {
                stepByStepActivated = false;
                button3.BackColor = System.Drawing.Color.LightGray;
                cipherAlgorithmVisualizator.setVisibilityForAllLabels(false);
            }

        }

        // Przycisk Kolejny krok
        private void button4_Click(object sender, EventArgs e)
        {
            if(stepByStepActivated == true)
            {
                cipherAlgorithmVisualizator.unmarkAll();
                int textLength = textBox1.Text.Length;
                int keyLength = textBox2.Text.Length;

                if (numberOfCharsDone < textLength)
                {
                    char textLetterToDo = textBox1.Text[numberOfCharsDone];
                    char keyLetterToDo = textBox2.Text[numberOfCharsDone % keyLength];

                    cipherOrDecipherStepByStep(textLetterToDo, keyLetterToDo);
                    numberOfCharsDone++;
                }
            }
        }

        private void cipherOrDecipherStepByStep(char textLetterToDo, char keyLetterToDo)
        {
            char resultLetter = cipherAlgorithmVisualizator.cipherOrDecipherOneLetter(textLetterToDo, keyLetterToDo);
            cipherAlgorithmVisualizator.markLetters(textLetterToDo, keyLetterToDo, resultLetter);
            textBox3.Text += resultLetter;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            stepByStepActivated = false;
            button3.BackColor = System.Drawing.Color.LightGray;

            numberOfCharsDone = 0;
            cipherAlgorithmVisualizator.unmarkAll();
        }

        private void readTextFromFile(ref TextBox resultTextBox, ref TextBox fileNameTextBox)
        {
            string fileName = null;
            string text = "";

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }

            if (fileName != null)
            {
                //Do something with the file, for example read text from it
                text = File.ReadAllText(fileName);
            }

            resultTextBox.Text = text;
            fileNameTextBox.Text = fileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            readTextFromFile(ref textBox1, ref textBox2);
        }
    }
}
