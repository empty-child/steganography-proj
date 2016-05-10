using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * TODO:
 * * Высчитывать вместимость контейнера и предупреждать о переполнении
 * * Реализовать сокрытие и изъятие данных из файла
 * * Реализовать логику преамбулы файла и предупреждении об отсутствии оной при извлечении данных
 * * Реализовать проверку целостности данных
 */

namespace steganographia_kurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }

    public class FileProcessor
    {
        //методы должны быть static!
        public static string ImageOpen(string filepath)
        {
            Bitmap sourceBitmap = null;
            try
            {
                sourceBitmap = new Bitmap(filepath);
            }
            catch(Exception exp)
            {
                DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при открытии", MessageBoxButtons.OK);
                return null;
            }
            string jpeg = null;
            for (int y = 0; y < sourceBitmap.Height; ++y)
            {
                for (int x = 0; x < sourceBitmap.Width; ++x)
                {
                    Color c = sourceBitmap.GetPixel(x, y);
                    jpeg += Convert.ToString(c.R, 2);
                    jpeg += Convert.ToString(c.G, 2);
                    jpeg += Convert.ToString(c.B, 2);
                }
            }
            return jpeg;
        }
        public static string TextOpen(string filepath)
        {
            string output = null;
            try
            {
                StreamReader fstr_in = new StreamReader(filepath);
                while (!fstr_in.EndOfStream)
                {
                    int sym_code = fstr_in.Read();
                    output += Convert.ToString(sym_code, 2);
                }
                return output;
            }
            catch(FileNotFoundException)
            {
                DialogResult error = MessageBox.Show("Файл не найден", "Ошибка при открытии", MessageBoxButtons.OK);
                return null;
            }
            catch (Exception exp)
            {
                DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при открытии", MessageBoxButtons.OK);
                return null;
            }
        }
    }

    public class DataConverter
    {
        //методы должны быть static!
        public static void HideData(string container, string main_data) //container - jpeg, main_data - текст
        {
            string temp = null; int j = 0;
            for (int i = 0; i <= main_data.Length; i++)
            {
                temp += main_data[i];
                if (i % 15 == 0)//временно вырезать redundantdata, оставить на "сладкое", если будет время
                {
                    temp = RedundantData(temp); //тут творится форменное безумие
                    string output = null;
                    for (int x = 0, y = 0; j <= container.Length; j++, x++)
                    {
                        output += container[j];
                        if (x % 5 == 0)
                        {
                            output += temp[y] + temp[y+1];
                            y += 2;
                            x = 0;
                            output = null;
                        }

                    }
                    temp = null;
                }
            }
        }
        public static string RedundantData(string input)
        {

            return "";
        }
    }
}

//string a = "abc";
//foreach (char letter in a.ToCharArray())
//{
//  Console.WriteLine(Convert.ToString(letter, 2));
//}
