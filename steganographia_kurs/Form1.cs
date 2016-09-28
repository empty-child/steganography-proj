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

            if (FileData.containerPath == "" || FileData.hideFilePath == "")
            {
                DialogResult error = MessageBox.Show("Не указан путь к файлам", "Укажите путь", MessageBoxButtons.OK);
                return;
            }

            FileData.outputName = "[h]" + FileData.containerPath.Remove(0, FileData.containerPath.LastIndexOf('\\') + 1);

            //pBHide.Visible = true;
            FileProcessor.Init(new FileProcessor(), true);

            if (!Utils.Limit(FileData.container, FileData.hidingText))
            {
                DialogResult error = MessageBox.Show("Количество скрываемых данных превышает объем контейнера", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            //pBHide.Value = 20;

            HideData.Init(new HideData());

            //pBHide.Value = 100;

            //pBHide.Visible = false;

            ResetForms();
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            //pBExtract.Visible = true;
            FileData.containerPath = containerExtract.Text;

            if (FileData.containerPath == "")
            {
                DialogResult error = MessageBox.Show("Не указан путь к файлам", "Укажите путь", MessageBoxButtons.OK);
                return;
            }

            FileData.outputName = FileData.containerPath.Remove(0, FileData.containerPath.LastIndexOf('\\') + 1) + ".txt";

            //pBExtract.Value = 20;

            FileProcessor.Init(new FileProcessor());
            ExtractData.Init(new ExtractData());

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
            containerExtract.Text = "";

            FileData.containerPath = containerPath.Text;
            FileData.hideFilePath = hideFilePath.Text;
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
        public static void Init(FileProcessor ob, bool hide = false)
        {
            FileData.container = ob.ImageOpen(FileData.containerPath);
            if (hide)
            {
                FileData.hidingText = ob.TextOpen(FileData.hideFilePath);
            }
        }

        Bitmap ImageOpen(string filepath)
        {
            try
            {
                Bitmap sourceBitmap = new Bitmap(filepath);
                FileData.container2 = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, sourceBitmap.PixelFormat); //HACK: найти альтернативу

                return sourceBitmap;
            }
            catch (Exception exp)
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
            string preamble = "<!preamble size=" + FileData.hidingText.Length.ToString().PadLeft(16, '0') + "!>";
            FileData.hidingText = Utils.ToBin(preamble) + FileData.hidingText;
            int i = 0;
            for (int y = 0; y < FileData.container.Height; ++y)
            {   // TODO: создать событие для изменения процентажа
                for (int x = 0; x < FileData.container.Width; ++x)
                {
                    Color c = FileData.container.GetPixel(x, y);
                    byte[] colorsArr = { c.R, c.G, c.B };
                    byte[] outputColors = new byte[3];
                    for (int it = 0; it < 3; it++)
                    {
                        outputColors[it] = (byte)(colorsArr[it] + DeltaCalculation(colorsArr[it], i));
                        i += 2;
                    }
                    try
                    {
                        FileData.container2.SetPixel(x, y, Color.FromArgb(outputColors[0], outputColors[1], outputColors[2]));
                    }
                    catch (Exception exp)
                    {
                        DialogResult error = MessageBox.Show("Произошла ошибка " + exp, "Ошибка при записи", MessageBoxButtons.OK);
                    }
                }
            }
            FileData.container2.Save(FileData.outputName);
            DialogResult result = MessageBox.Show("Сокрытие прошло успешно", "Успех", MessageBoxButtons.OK);
        }

        int DeltaCalculation(byte dig, int i)
        {
            while (dig > 3)
            {
                dig -= 4;
            }
            byte hidingNum = 0;
            if (i + 1 < FileData.hidingText.Length) hidingNum = Utils.ToDec(String.Concat(FileData.hidingText[i], FileData.hidingText[i + 1]));
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
            string binaryOutput = null;
            for (int y = 0, i = 0, size = 273; y < FileData.container.Height; ++y)
            {
                for (int x = 0; x < FileData.container.Width; ++x)
                {
                    Color c = FileData.container.GetPixel(x, y);
                    byte[] colorsArr = { c.R, c.G, c.B };
                    for (int it = 0; it < 3; it++)
                    {
                        binaryOutput += Extracting(colorsArr[it]);
                        i++;
                    }
                    if (i == size)
                    {
                        string output = Utils.ToText(binaryOutput);
                        if (size == 273 && string.Compare(Utils.ToText(binaryOutput), 0, "<!preamble size=", 0, 16) == 0)
                        {
                            size = int.Parse(Utils.ToText(binaryOutput).Substring(16, 16)) + 34;
                            //output = null;
                        }
                        else if (string.Compare(Utils.ToText(binaryOutput), 0, "<!preamble size=", 0, 16) != 0)
                        {
                            DialogResult result = MessageBox.Show("Данный файл не является контейнером", "Ошибка", MessageBoxButtons.OK);
                            return;
                        }
                        else
                        {
                            try
                            {
                                FileStream fs = new FileStream(FileData.outputName, FileMode.OpenOrCreate);
                                StreamWriter str_wr = new StreamWriter(fs);
                                str_wr.Write(output.Remove(0, 34));
                                str_wr.Close();
                                fs.Close();
                                DialogResult result = MessageBox.Show("Восстановление данных выполнено", "Успех", MessageBoxButtons.OK);
                                return;
                            }
                            catch
                            {
                                DialogResult result = MessageBox.Show("Ошибка записи в файл", "Ошибка", MessageBoxButtons.OK);
                            }

                        }
                    }
                }
            }
        }

        string Extracting(byte color)
        {
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
            return text.Length <= image.Height * image.Width * 3 ? true : false;
        }
    }

    public class FileData
    {
        public static string containerPath { get; set; }
        public static string hideFilePath { get; set; }
        public static string outputName { get; set; }

        public static string hidingText { get; set; }
        public static Bitmap container { get; set; }

        public static Bitmap container2 { get; set; }
    }
}