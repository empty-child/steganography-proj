namespace steganographia_kurs
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ControlPanel = new System.Windows.Forms.TabControl();
            this.HideData = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ExtractData = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ControlPanel.SuspendLayout();
            this.HideData.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.HideData);
            this.ControlPanel.Controls.Add(this.ExtractData);
            this.ControlPanel.Location = new System.Drawing.Point(12, 12);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.SelectedIndex = 0;
            this.ControlPanel.Size = new System.Drawing.Size(439, 263);
            this.ControlPanel.TabIndex = 0;
            // 
            // HideData
            // 
            this.HideData.Controls.Add(this.button3);
            this.HideData.Controls.Add(this.label2);
            this.HideData.Controls.Add(this.textBox2);
            this.HideData.Controls.Add(this.button2);
            this.HideData.Controls.Add(this.label1);
            this.HideData.Controls.Add(this.textBox1);
            this.HideData.Controls.Add(this.button1);
            this.HideData.Location = new System.Drawing.Point(4, 22);
            this.HideData.Name = "HideData";
            this.HideData.Padding = new System.Windows.Forms.Padding(3);
            this.HideData.Size = new System.Drawing.Size(431, 237);
            this.HideData.TabIndex = 0;
            this.HideData.Text = "Скрыть данные";
            this.HideData.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(303, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ExtractData
            // 
            this.ExtractData.Location = new System.Drawing.Point(4, 22);
            this.ExtractData.Name = "ExtractData";
            this.ExtractData.Padding = new System.Windows.Forms.Padding(3);
            this.ExtractData.Size = new System.Drawing.Size(431, 237);
            this.ExtractData.TabIndex = 1;
            this.ExtractData.Text = "Извлечь данные";
            this.ExtractData.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Файл для сокрытия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Контейнер:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(26, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(303, 20);
            this.textBox2.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(336, 77);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(131, 136);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(169, 63);
            this.button3.TabIndex = 7;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 287);
            this.Controls.Add(this.ControlPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ControlPanel.ResumeLayout(false);
            this.HideData.ResumeLayout(false);
            this.HideData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ControlPanel;
        private System.Windows.Forms.TabPage HideData;
        private System.Windows.Forms.TabPage ExtractData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}

