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

        private bool checkText(string text)
        {
            foreach (char l in text)
            {
                if (!dictionaryLettersNumbers.ContainsKey(l))
                {
                    MessageBox.Show("Wykryto niedozwolony znak: " + l, "Niedozwolony znak", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }

        private bool checkIfNotEmpty(string text)
        {
            if (text.Length == 0)
            {
                MessageBox.Show("Jedno z wymaganych pól tekstowych jest puste!", "Puste pole", MessageBoxButtons.OK);
                return false;
            }
            return true;
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
            if (!checkIfNotEmpty(textBox1.Text) || !checkIfNotEmpty(textBox2.Text))
                return;
            else
                cipherOrDecipherText(ref textBox3, textBox1.Text, textBox2.Text);
        }

        // Przycisk Deszyfruj
        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkIfNotEmpty(textBox3.Text) || !checkIfNotEmpty(textBox2.Text))
                return;
            else
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
            //Jezeli ciag zawiera niedozwolone znaki, to wyjdz z funkcji
            if (checkText(textMessage) == false)
                return;

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
                if (!checkIfNotEmpty(textBox1.Text) || !checkIfNotEmpty(textBox2.Text))
                    return;

                cipherAlgorithmVisualizator.unmarkAll();
                int textLength = textBox1.Text.Length;
                int keyLength = textBox2.Text.Length;

                if (numberOfCharsDone < textLength)
                {
                    char textLetterToDo = Char.ToUpper(textBox1.Text[numberOfCharsDone]);
                    char keyLetterToDo = Char.ToUpper(textBox2.Text[numberOfCharsDone % keyLength]);


                    if (!cipherAlgorithmVisualizator.checkText(Char.ToString(textLetterToDo)) || !cipherAlgorithmVisualizator.checkText(Char.ToString(keyLetterToDo)))
                        return;
                    cipherOrDecipherStepByStep(textLetterToDo, keyLetterToDo);
                    numberOfCharsDone++;
                }
            }
        }

        private void cipherOrDecipherStepByStep(char textLetterToDo, char keyLetterToDo)
        {
            char textLetterToDoUpper = Char.ToUpper(textLetterToDo);
            char keyLetterToDoupper = Char.ToUpper(keyLetterToDo);

            if (!cipherAlgorithmVisualizator.checkText(Char.ToString(textLetterToDoUpper)) || !cipherAlgorithmVisualizator.checkText(Char.ToString(keyLetterToDoupper)))
                return;

            char resultLetter = cipherAlgorithmVisualizator.cipherOrDecipherOneLetter(textLetterToDoUpper, keyLetterToDoupper);
            if (textLetterToDo == ' ' || textLetterToDo == '.' || textLetterToDo == ',' || textLetterToDo == '\r' || textLetterToDo == '\t' || textLetterToDo == '\n')
                textBox3.Text += resultLetter;
            else
            {
                cipherAlgorithmVisualizator.markLetters(textLetterToDoUpper, keyLetterToDoupper, resultLetter);
                textBox3.Text += resultLetter;
            }
            
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
            readTextFromFile(ref textBox1, ref textBox5);
        }

        private void showInfoPopUp()
        {
            String infoText = "Program Szyfrujący Szyfrem Beauforta\n" +
                "Autor: Cezary Waligóra\n\n" +
                "Program pozwala zaszyfrować i odszyfrować wybrany tekst za pomocą podanego klucza.\n" +
                "Instrukcja:\n" +
                "1. Tekst, który ma zostac zaszyfrowany należy wpisać do pierwszego pola tekstowego\n" +
                "2. Klucz należy wpisać do drugiego pola tekstowego\n" +
                "3. W trzecim polu powinien pojawić się zaszyfrowany tekst\n" +
                "4. Aby wczytać tekst z pliku należy kliknąć przycisk Otwórz plik. Zostanie on wczytany do pierwszego pola\n" +
                "5. Aby odszyfrować wiadomość należy ją wpisać do trzeciego pola tekstowego. Po uzupełnieniu klucza można nacisnąć przysk Deszyfruj\n" +
                "6. Algorytm szyfrujący i deszyfrujący są takie same, dlatego można także odszyfrować plik wpisując go w pierwsze pole tekstowe i klikając Szyfruj\n" +
                "7. Aby wyczyścić wszystkie pola należy nacisnąć przycisk Wyczyść wszystko\n" +
                "8. Aby zapisac wynik szyfrowania lub deszyfrowania do pliku tekstowego nalezy kliknąć przycisk Zapisz do pliku a wybrać lokalizację i nazwę dla nowego pliku\n\n" +
                "Dzialanie algorytmu:\n" +
                "Załóżmy, że kolejnym literom alfabetu łacińskiego nadajemy kolejne numery od 1 do 26, tak że A ma numer 1, B ma numer 2 itd." +
                "Dla danego znaku tekstu M i danego znaku klucza K znak zaszyfrowany C możemy wyznaczyć ze wzoru:\n" +
                "C = (K - M) mod 26\n" +
                "gdzie litery M, K i C to liczby reprezentujące odpowiadające im liczby.\n\n" +
                "Wizualizacja:\n" +
                "Aby przeprowadzić wizualizację, należy wpisać wybrany tekst (jawny lub zaszyfrowany) do pierwszego pola tekstowego oraz klucz do drugiego pola tekstowego.\n" +
                "Następnie należy kliknąć przycisk Wykonywanie krokowe. Powinien on oznaczyć się na niebiesko, co oznacza, że uruchomiono tryb wykonywania krokowego. Spowoduje to takze ukazanie się macierzy liter.\n" +
                "Po wcisnieciu przycisku następny krok w górnym rzędzie oznaczona zostanie odpowiednia litera oryginalnego tekstu. Następnie w tej kolumnie, w której znajduje się dana litera tekstu znajdowana jest litera klucza. Litera, która zostanie podswietlona po lewej stronie jest wynikiem szyfrowania.\n" +
                "Jezeli klucz jest krótszy niż szyfrowany tekst to klucz zostanie powielony tak, aby jego dłygość była równa długości szyfrowanego tesktu\n" +
                "Przykład: jeżeli klucz ma 5 liter, a tekst 14, to 6 litera zostanie zakodowana według pierwszej litery klucza.\n\n" +
                "UWAGA! Znaki dozwolone przy wizualizacji i znaki dozwolone poza trybem wizualizacji różnią się!\n" +
                "Znaki dostępne poza trybem wizualizacji, to wielkie litery alfabetu łacińskiego. Można także wpisywać małe litery, jednak zostaną one ostatecznie zamienione na wielkie.\n" +
                "Zbiór znaków dostepnych poza trybem wizualizacji obejmuje małe i wielkie znaki alfabetu łacińskiego oraz polskiego, cyfry, oraz znaki:\n" +
                "',' '.' ':' ';' '\"' ' ? ' '!' ' * @ # $', '%', '^', '&', '/', '|', '(', ')', '{', '}', '[', ']', '<', '>', '-', '_', ' ', '+', '=', '\\', '\'' oraz tabulację, enter i nową linię"
                ;

            MessageBox.Show(infoText, "Informacje o programie", MessageBoxButtons.OK);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            showInfoPopUp();
        }

        private void SaveToFile(string text)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter write = new StreamWriter(File.Create(save.FileName)))
                    write.Write(text);
                textBox4.Text = save.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(checkIfNotEmpty(textBox3.Text))
                SaveToFile(textBox3.Text);
        }
    }
}
