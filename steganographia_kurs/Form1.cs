using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

/*
 * TODO:
 * * Реализовать проверку целостности данных
 * * Переработать систему расчета лимита
 */

namespace steganographia_kurs
{

    public class FileData
    {
        public static string containerPath { get; set; }
        public static string hideFilePath { get; set; }
        public static string outputName { get; set; }

        public static string hidingText { get; set; }
        public static Bitmap container { get; set; }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            FileData.containerPath = containerPath.Text;
            FileData.hideFilePath = hideFilePath.Text;
            FileData.outputName = outputName.Text;

            if (FileData.containerPath == "" || FileData.hideFilePath == "" || FileData.outputName == "")
            {
                DialogResult error = MessageBox.Show("Не указан путь к файлам", "Укажите путь", MessageBoxButtons.OK);
                return;
            }
            //if (!FileProcessor.Limit(containerPath.Text, hideFilePath.Text))
            //{
            //    DialogResult error = MessageBox.Show("К сожалению, программа не в состоянии справится с таким объемом данных", "Ошибка", MessageBoxButtons.OK);
            //    return;
            //}
            
            pBHide.Visible = true;
            FileProcessor.Init(new FileProcessor());
            pBHide.Value = 20;

            //if (4 * text.Length > image.Length)
            //{
            //    DialogResult error = MessageBox.Show("Объем скрываемого файла превышает емкость контейнера", "Недостаточно места", MessageBoxButtons.OK);
            //    return;
            //}
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
        public static void Init(FileProcessor ob)
        {
            FileData.container = ob.ImageOpen(FileData.containerPath);
            FileData.hidingText = ob.TextOpen(FileData.hideFilePath);
        }

        Bitmap ImageOpen(string filepath)
        {
            try
            {
                Bitmap sourceBitmap = new Bitmap(filepath);
                return sourceBitmap;
            }
            catch(Exception exp)
            {
                DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при открытии", MessageBoxButtons.OK);
                return null;
            }
        }

        string TextOpen(string filepath)
        {
            string output = null;
            try
            {
                StreamReader fstr_in = new StreamReader(filepath);
                while (!fstr_in.EndOfStream)
                {
                    int sym_code = fstr_in.Read();
                    output += Utils.ToBin(sym_code);
                }
                return output;
            }
            catch (Exception exp)
            {
                DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при открытии", MessageBoxButtons.OK);
                return null;
            }
        }
    }

    //public class DataConverter
    //{
    //   public static void HideData(string container, string maindata, string outputName) //container - jpeg, main_data - текст
    //    {
    //        string s1 = null; byte r = 0, g = 0, b = 0;
    //        string preamble = "<!preamble size="+maindata.Length.ToString().PadLeft(16, '0')+"!>";
    //        preamble = Utils.ToBin(preamble);
    //        maindata = preamble + maindata;
    //        for (int i = 0, j = 0, cnt = 0, x = 0, y = 0, colour = 0; i < container.Length; i++)
    //        {
    //            s1 += container[i];
    //            cnt++;
    //            if (cnt % 6 == 0)
    //            {
    //                try
    //                {
    //                    s1 += maindata[j];
    //                    s1 += maindata[j + 1];
    //                    j += 2;
    //                }
    //                catch
    //                {
    //                    s1 += container[i + 1];
    //                    s1 += container[i + 2];
    //                }
    //                i += 2;
    //                cnt = 0;
    //                switch (colour)
    //                {
    //                    case 0:
    //                        r = Convert.ToByte(s1, 2);
    //                        s1 = null;
    //                        colour++;
    //                        break;
    //                    case 1:
    //                        g = Convert.ToByte(s1, 2);
    //                        s1 = null;
    //                        colour++;
    //                        break;
    //                    case 2:
    //                        b = Convert.ToByte(s1, 2);
    //                        FileProcessor.targetBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
    //                        x++;
    //                        if (x >= FileProcessor.targetBitmap.Width)
    //                        {
    //                            x = 0;
    //                            y++;
    //                        }
    //                        colour = 0;
    //                        s1 = null;
    //                        break;
    //                }
    //            }
    //        }
    //        FileProcessor.targetBitmap.Save(outputName + ".jpg");
    //    }

    //    public static string ExtractData(string container = "2.jpg")
    //    {
    //        string output = null; int size = 0;
    //        output = Extractor(container);
    //        if (string.Compare(Utils.ToText(output), 0, "<!preamble size=", 0, 16) != 0) 
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            size = int.Parse(Utils.ToText(output).Substring(16, 16))*4;
    //        }
    //        output = null;
    //        output = Extractor(container, 2176, size);
    //        output = Utils.ToText(output);
    //        return output;
    //    }

        

    //    static string Extractor(string container, int position = 0, int size = 2176)
    //    {
    //        string output = null;
    //        for (int i = position, cnt = 0; i < position + size; i++)
    //        {
    //            cnt++;
    //            if (cnt % 8 == 0)
    //            {
    //                try
    //                {
    //                    output += container[i - 1]; //брал на символ больше, проверить
    //                    output += container[i];
    //                    cnt = 0;
    //                }
    //                catch
    //                {
    //                    return output;
    //                }
    //            }
    //        }
    //        return output;
    //    }
    //    //public static string RedundantData(string input)
    //    //{
    //    //    return "";
    //    //}
    //}


    class HideData
    {
        Thread Thrd;
        public static void Init(HideData ob)
        {
            ob.Thrd = new Thread(ob.Run);
            ob.Thrd.Start();
        }
        
        //public HideData()
        //{
        //    Thrd = new Thread(this.Run);
        //    Thrd.Start();
        //}

        void Run()
        {
            string preamble = "<!preamble size="+FileData.hidingText.Length.ToString().PadLeft(16, '0')+"!>";
            FileData.hidingText = Utils.ToBin(preamble + FileData.hidingText);
            for (int y = 0, i = 0; y < FileData.container.Height; ++y)
            { // создать событие для изменения процентажа
                for (int x = 0; x < FileData.container.Width; ++x)
                {
                    Color c = FileData.container.GetPixel(x, y); //вывести в отдельную функцию
                    byte[] colorsArr = { c.R, c.G, c.B };
                    foreach(byte color in colorsArr)
                    {
                        int r = color + DeltaCalculation(color, i);
                        i += 2;
                    }
                    
                }
            }
        }

        int DeltaCalculation(byte dig, int i)
        {
            while (dig > 3)
            {
                dig -= 4;
            }
            int hidingNum = Utils.ToDec(String.Concat(FileData.hidingText[i], FileData.hidingText[i + 1]));
            byte delta = (byte)(hidingNum - dig);
            return delta;
        }
    }


    public static class Utils
    {
        public static string ToBin(int sym_code)
        {
            return Convert.ToString(sym_code, 2).PadLeft(16, '0');
        }

        public static string ToBin(string text)
        {
            string temp = null;
            foreach (char letter in text.ToCharArray())
            {
                temp += Convert.ToString(letter, 2).PadLeft(16, '0');
            }
            return temp;
        }

        public static byte ToDec(string binText)
        {
            return 0;
        }

        public static string ToText(string binaryString)
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
    }
}