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
 * * Реализовать изъятие данных из файла
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
            string image = null, text = null;
            image = FileProcessor.ImageOpen();
            text = FileProcessor.TextOpen();
            DataConverter.HideData(image, text);
        }
    }

    public class FileProcessor
    {
        public static Bitmap targetBitmap;
        //методы должны быть static!
        public static string ImageOpen(string filepath = "1.jpg")
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
                    jpeg += Convert.ToString(c.R, 2).PadLeft(8, '0');
                    jpeg += Convert.ToString(c.G, 2).PadLeft(8, '0');
                    jpeg += Convert.ToString(c.B, 2).PadLeft(8, '0');
                }
            }
            targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, sourceBitmap.PixelFormat);
            return jpeg;
        }
        public static string TextOpen(string filepath = "1.txt")
        {
            string output = null;
            try
            {
                StreamReader fstr_in = new StreamReader(filepath);
                while (!fstr_in.EndOfStream)
                {
                    int sym_code = fstr_in.Read();
                    output += Convert.ToString(sym_code, 2).PadLeft(8, '0');
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
        public static void HideData(string container, string maindata) //container - jpeg, main_data - текст
        {
            string s1 = null; byte r = 0, g = 0, b = 0;

            for(int i = 0, j = 0, cnt = 0, x = 0, y = 0, colour = 0; i < container.Length; i++)
            {
                s1 += container[i];
                cnt++;
                if (cnt % 6 == 0)
                {
                    try
                    {
                        s1 += maindata[j];
                        s1 += maindata[j + 1];
                        j += 2;
                    }
                    catch
                    {
                        s1 += container[i + 1];
                        s1 += container[i + 2];
                    }
                    i += 2;
                    cnt = 0;
                    switch (colour)
                    {
                        case 0:
                            r = Convert.ToByte(s1, 2);
                            s1 = null;
                            colour++;
                            break;
                        case 1:
                            g = Convert.ToByte(s1, 2);
                            s1 = null;
                            colour++;
                            break;
                        case 2:
                            b = Convert.ToByte(s1, 2);
                            FileProcessor.targetBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                            x++;
                            if (x >= FileProcessor.targetBitmap.Width)
                            {
                                x = 0;
                                y++;
                            }
                            colour = 0;
                            s1 = null;
                            break;
                    }
                }
            }
            FileProcessor.targetBitmap.Save("2.jpg");
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
