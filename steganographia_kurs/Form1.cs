using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

//TODO: реализовать проверку целостности данных

namespace steganographia_kurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////

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
            
            pBHide.Visible = true;
            FileProcessor.Init(new FileProcessor());

            if (!Utils.Limit(FileData.container, FileData.hidingText))
            {
                DialogResult error = MessageBox.Show("Количество скрываемых данных превышает объем контейнера", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            pBHide.Value = 20;

            HideData.Init(new HideData()); //TODO: проверить работу алгоритма

            pBHide.Value = 100;

            DialogResult result = MessageBox.Show("Сокрытие прошло успешно", "Успех", MessageBoxButtons.OK);
            pBHide.Visible = false;

            ResetForms();
        }

        private void extractButton_Click(object sender, EventArgs e) //TODO: адаптировать функцию под новые реалии
        {
            pBExtract.Visible = true;
            FileData.containerPath = containerExtract.Text;
            //string image = FileProcessor.ImageOpen(containerExtract.Text);
            pBExtract.Value = 20;
            FileProcessor.Init(new FileProcessor());
            ExtractData.Init(new ExtractData());

            //string extractedData = DataConverter.ExtractData(FileData.containerPath);
            //pBExtract.Value = 100;
            //if (extractedData == null)
            //{
            //    DialogResult error = MessageBox.Show("Данный файл не является контейнером", "Ошибка чтения данных", MessageBoxButtons.OK);
            //}
            //else
            //{
            //    DialogResult result = MessageBox.Show(extractedData, "Результат", MessageBoxButtons.OK);
            //}
            pBExtract.Visible = false;
            ResetForms();
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

        void ResetForms()
        {
            containerPath.Text = "";
            hideFilePath.Text = "";
            outputName.Text = "";
            containerExtract.Text = "";

            FileData.containerPath = containerPath.Text;
            FileData.hideFilePath = hideFilePath.Text;
            FileData.outputName = outputName.Text;
        }

        //public delegate void Procentage();

        //public event Procentage ProcentEvent;

        //public void OnProcentage()
        //{
        //    ProcentEvent?.Invoke();
        //}

        //void ProcentIncrease(object sender, EventArgs e)
        //{
        //    pBHide.Increment(5);

        //}
    }
//TODO: разобраться с событиями и их работой (можно посмотреть в лабе с музыкой в вк)
//public delegate void Procentage(); 

//class EventTest : Form1
//{
//    public delegate void Procentage();

//    public event Procentage ProcentEvent;

//    public void OnProcentage()
//    {
//        ProcentEvent?.Invoke();
//    }

//    void ProcentIncrease(object sender, EventArgs e)
//    {
//        pBHide.Increment(5);

//    }
//}

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
                output = File.ReadAllText(filepath);
                return Utils.ToBin(output);
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
        public static string ExtractData(string container = "2.jpg")
        {
            string output = null; int size = 0;
            output = Extractor(container);
            if (string.Compare(Utils.ToText(output), 0, "<!preamble size=", 0, 16) != 0)
            {
                return null;
            }
            else
            {
                size = int.Parse(Utils.ToText(output).Substring(16, 16)) * 4;
            }
            output = null;
            output = Extractor(container, 2176, size);
            output = Utils.ToText(output);
            return output;
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
}


    public class HideData
    {
        Thread Thrd;
        public static void Init(HideData ob)
        {
            ob.Thrd = new Thread(ob.Run);
            ob.Thrd.Start();
        }

        void Run()
        {
            string preamble = "<!preamble size="+FileData.hidingText.Length.ToString().PadLeft(16, '0')+"!>";
            FileData.hidingText = Utils.ToBin(preamble + FileData.hidingText);
            byte i = 0;
            for (int y = 0; y < FileData.container.Height; ++y)
            {   // TODO: создать событие для изменения процентажа
                for (int x = 0; x < FileData.container.Width; ++x)
                {
                    Color c = FileData.container.GetPixel(x, y);
                    byte[] colorsArr = { c.R, c.G, c.B };
                    byte[] outputColors = new byte[3];
                    for(int it = 0; it < 3; it++)
                    {
                        outputColors[it] = (byte)(colorsArr[it] + DeltaCalculation(colorsArr[it], i));
                        i += 2;
                    }
                    FileData.container.SetPixel(x, y, Color.FromArgb(outputColors[0], outputColors[1], outputColors[2]));
                }
            }
            FileData.container.Save(FileData.outputName + ".jpg");
        }

        int DeltaCalculation(byte dig, byte i)
        {
            dig %= 10;
            while (dig > 3)
            {
                dig -= 4;
            }
            byte hidingNum = Utils.ToDec(String.Concat(FileData.hidingText[i], FileData.hidingText[i + 1])); //вызывает сомнения
            int delta = hidingNum - dig;
            return delta;
        }
    }

    public class ExtractData
    {
        Thread Thrd;
        public static void Init(ExtractData ob)
        {
            ob.Thrd = new Thread(ob.Run);
            ob.Thrd.Start();
        }

        void Run()
        {//34
            string output = null;
            for (int y = 0, i = 0; y < FileData.container.Height; ++y)
            {
                for (int x = 0; x < FileData.container.Width; ++x)
                {
                    Color c = FileData.container.GetPixel(x, y);
                    byte[] colorsArr = { c.R, c.G, c.B };
                    for (int it = 0; it < 3; it++)
                    {
                        output += Extracting(colorsArr[it]);
                        i++;
                    }
                    if (i == 33)
                    {

                    }
                }
            }
        }

        string Extracting(byte color)
        {
            color %= 10;
            while (color > 3)
            {
                color -= 4;
            }
            return Utils.ToBin(color).ToString();
        }
    }


    public static class Utils
    {
        public static string ToBin(int sym_code)
        {
            return Convert.ToString(sym_code, 2).PadLeft(2, '0');
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
            return Convert.ToByte(binText, 2);
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

        public static bool Limit(Bitmap image, string text)
        {
            return text.Length <= image.Height*image.Width*3 ? true : false;
        }
    }

    public class FileData
    {
        public static string containerPath { get; set; }
        public static string hideFilePath { get; set; }
        public static string outputName { get; set; }

        public static string hidingText { get; set; }
        public static Bitmap container { get; set; }
    }
}

