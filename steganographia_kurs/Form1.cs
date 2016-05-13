using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/*
 * TODO:
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

        private void hideButton_Click(object sender, EventArgs e)
        {
            if (containerPath.Text == "" || hideFilePath.Text == "")
            {
                DialogResult error = MessageBox.Show("Не указан путь к файлам", "Укажите путь", MessageBoxButtons.OK);
                return;
            }
            if (!FileProcessor.Compare(containerPath.Text, hideFilePath.Text))
            {
                DialogResult error = MessageBox.Show("Объем скрываемого файла превышает емкость контейнера", "Недостаточно места", MessageBoxButtons.OK);
                return;
            }
            pBHide.Visible = true;
            string image = FileProcessor.ImageOpen(containerPath.Text); //сначала проводить проверку на наличие данных
            pBHide.Value = 10;
            string text = FileProcessor.TextOpen(hideFilePath.Text);
            pBHide.Value = 20;
            DataConverter.HideData(image, text);
            pBHide.Value = 100;
            DialogResult result = MessageBox.Show("Сокрытие прошло успешно", "Успех", MessageBoxButtons.OK);
            pBHide.Visible = false;
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            pBExtract.Visible = true;
            string image = FileProcessor.ImageOpen(containerExtract.Text);
            pBExtract.Value = 20;
            string a = DataConverter.ExtractData(image);
            pBExtract.Value = 100;
            DialogResult error = MessageBox.Show(a, "sadasd", MessageBoxButtons.OK);
            pBExtract.Visible = false;
        }

        private void chooseHideFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы txt|*.txt";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                hideFilePath.Text = OPF.FileName;
            }
        }

        private void chooseContainer_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы jpg|*.jpg;*,jpeg";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                containerPath.Text = OPF.FileName;
            }
        }

        private void containerExChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы jpg|*.jpg;*,jpeg";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                containerExtract.Text = OPF.FileName;
            }
        }
    }

    public class FileProcessor
    {
        public static Bitmap targetBitmap;
        //методы должны быть static!
        public static string ImageOpen(string filepath = "2.jpg")
        {
            Bitmap sourceBitmap = null;
            try
            {
                sourceBitmap = new Bitmap(filepath);
            }
            catch(Exception exp)
            {
                //DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при открытии", MessageBoxButtons.OK);
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
                    output += Convert.ToString(sym_code, 2).PadLeft(16, '0');
                }
                return output;
            }
            //catch(FileNotFoundException)
            //{
            //    DialogResult error = MessageBox.Show("Файл не найден", "Ошибка при открытии", MessageBoxButtons.OK);
            //    return null;
            //}
            catch (Exception exp)
            {
                //DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при открытии", MessageBoxButtons.OK);
                return null;
            }
        }
        public static bool Compare(string container, string maindata)
        {
            FileInfo containerWeight = new FileInfo(container);
            FileInfo maindataWeight = new FileInfo(maindata);
            return maindataWeight.Length > containerWeight.Length ? false : true;
        }
    }

    public class DataConverter
    {
        //методы должны быть static!
        public static void HideData(string container, string maindata) //container - jpeg, main_data - текст
        {
            string s1 = null; byte r = 0, g = 0, b = 0;
            Application.DoEvents();
            for (int i = 0, j = 0, cnt = 0, x = 0, y = 0, colour = 0; i < container.Length; i++)
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
        public static string ExtractData(string container = "2.jpg")
        {
            string output = null, temp = null;
            int a = 0;
            for (int i = 0, cnt = 0; i < 384; i++)
            {
                cnt++;
                if (cnt % 8 == 0)
                {
                    output += container[i - 1]; //брал на символ больше, проверить
                    output += container[i];
                    cnt = 0;
                }
            }
            for(int i = 0; i < output.Length; i += 16)
            {
                try
                {
                    a = Convert.ToInt32(output.Substring(i, 16), 2);
                    temp += ((char)a).ToString();
                }
                catch
                {
                    return temp;
                }
            }
            return temp;
        }
        //public static string RedundantData(string input)
        //{
        //    return "";
        //}
    }
}