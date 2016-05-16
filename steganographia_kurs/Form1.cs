using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/*
 * TODO:
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
            if (containerPath.Text == "" || hideFilePath.Text == "" || outputName.Text == "")
            {
                DialogResult error = MessageBox.Show("Не указан путь к файлам", "Укажите путь", MessageBoxButtons.OK);
                return;
            }
            if (!FileProcessor.Limit(containerPath.Text, hideFilePath.Text))
            {
                DialogResult error = MessageBox.Show("К сожалению, программа не в состоянии справится с таким объемом данных", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            pBHide.Visible = true;
            string image = FileProcessor.ImageOpen(containerPath.Text);
            pBHide.Value = 10;
            string text = FileProcessor.TextOpen(hideFilePath.Text);
            pBHide.Value = 20;
            if (4 * text.Length > image.Length)
            {
                DialogResult error = MessageBox.Show("Объем скрываемого файла превышает емкость контейнера", "Недостаточно места", MessageBoxButtons.OK);
                return;
            }
            DataConverter.HideData(image, text, outputName.Text);
            pBHide.Value = 100;
            DialogResult result = MessageBox.Show("Сокрытие прошло успешно", "Успех", MessageBoxButtons.OK);
            pBHide.Visible = false;
            containerPath.Text = "";
            hideFilePath.Text = "";
            outputName.Text = "";
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            pBExtract.Visible = true;
            string image = FileProcessor.ImageOpen(containerExtract.Text);
            pBExtract.Value = 20;
            string extractedData = DataConverter.ExtractData(image);
            pBExtract.Value = 100;
            if(extractedData == null)
            {
                DialogResult error = MessageBox.Show("Данный файл не является контейнером", "Ошибка чтения данных", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult result = MessageBox.Show(extractedData, "Результат", MessageBoxButtons.OK);
            }
            pBExtract.Visible = false;
            containerExtract.Text = "";
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
                    jpeg += Convert.ToString(c.R, 2).PadLeft(8, '0');
                    jpeg += Convert.ToString(c.G, 2).PadLeft(8, '0');
                    jpeg += Convert.ToString(c.B, 2).PadLeft(8, '0');
                }
            }
            targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, sourceBitmap.PixelFormat);
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
                    output += Convert.ToString(sym_code, 2).PadLeft(16, '0');
                }
                return output;
            }
            catch (Exception exp)
            {
                DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при открытии", MessageBoxButtons.OK);
                return null;
            }
        }
        public static bool Limit(string container, string maindata)
        {
            FileInfo containerWeight = new FileInfo(container);
            FileInfo maindataWeight = new FileInfo(maindata);
            return maindataWeight.Length > 10000 || containerWeight.Length > 100000 ? false : true;
        }
    }

    public class DataConverter
    {
        //методы должны быть static!
        public static void HideData(string container, string maindata, string outputName) //container - jpeg, main_data - текст
        {
            string s1 = null, temp = null; byte r = 0, g = 0, b = 0;
            string preamble = "<!preamble size="+maindata.Length.ToString().PadLeft(16, '0')+"!>";
            foreach (char letter in preamble.ToCharArray())
            {
              temp += Convert.ToString(letter, 2).PadLeft(16, '0');
            }
            maindata = temp + maindata;
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
            FileProcessor.targetBitmap.Save(outputName + ".jpg");
        }

        public static string ExtractData(string container = "2.jpg")
        {
            string output = null; int size = 0;
            output = Extractor(container);
            if (string.Compare(ToText(output), 0, "<!preamble size=", 0, 16) != 0) 
            {
                return null;
            }
            else
            {
                size = int.Parse(ToText(output).Substring(16, 16))*4;
            }
            output = null;
            output = Extractor(container, 2176, size);
            output = ToText(output);
            return output;
        }

        static string ToText(string binaryString)
        {
            string text = null; int charNum = 0;
            for (int i = 0; i < binaryString.Length; i += 16)
            {
                try
                {
                    charNum = Convert.ToInt32(binaryString.Substring(i, 16), 2);
                    text += ((char)charNum).ToString();
                }
                catch
                {
                    return text;
                }
            }
            return text;
        }

        static string Extractor(string container, int position = 0, int size = 2176)
        {
            string output = null;
            for (int i = position, cnt = 0; i < position + size; i++)
            {
                cnt++;
                if (cnt % 8 == 0)
                {
                    try
                    {
                        output += container[i - 1]; //брал на символ больше, проверить
                        output += container[i];
                        cnt = 0;
                    }
                    catch
                    {
                        return output;
                    }
                }
            }
            return output;
        }
        //public static string RedundantData(string input)
        //{
        //    return "";
        //}
    }
}