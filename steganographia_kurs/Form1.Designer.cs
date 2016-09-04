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
            this.HideDataTab = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.outputName = new System.Windows.Forms.TextBox();
            this.pBHide = new System.Windows.Forms.ProgressBar();
            this.hideButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.containerPath = new System.Windows.Forms.TextBox();
            this.chooseContainer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.hideFilePath = new System.Windows.Forms.TextBox();
            this.chooseHideFile = new System.Windows.Forms.Button();
            this.ExtractData = new System.Windows.Forms.TabPage();
            this.pBExtract = new System.Windows.Forms.ProgressBar();
            this.extractButton = new System.Windows.Forms.Button();
            this.containerExChoose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.containerExtract = new System.Windows.Forms.TextBox();
            this.ControlPanel.SuspendLayout();
            this.HideDataTab.SuspendLayout();
            this.ExtractData.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.HideDataTab);
            this.ControlPanel.Controls.Add(this.ExtractData);
            this.ControlPanel.Location = new System.Drawing.Point(12, 12);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.SelectedIndex = 0;
            this.ControlPanel.Size = new System.Drawing.Size(439, 317);
            this.ControlPanel.TabIndex = 0;
            // 
            // HideDataTab
            // 
            this.HideDataTab.Controls.Add(this.label4);
            this.HideDataTab.Controls.Add(this.outputName);
            this.HideDataTab.Controls.Add(this.pBHide);
            this.HideDataTab.Controls.Add(this.hideButton);
            this.HideDataTab.Controls.Add(this.label2);
            this.HideDataTab.Controls.Add(this.containerPath);
            this.HideDataTab.Controls.Add(this.chooseContainer);
            this.HideDataTab.Controls.Add(this.label1);
            this.HideDataTab.Controls.Add(this.hideFilePath);
            this.HideDataTab.Controls.Add(this.chooseHideFile);
            this.HideDataTab.Location = new System.Drawing.Point(4, 22);
            this.HideDataTab.Name = "HideDataTab";
            this.HideDataTab.Padding = new System.Windows.Forms.Padding(3);
            this.HideDataTab.Size = new System.Drawing.Size(431, 291);
            this.HideDataTab.TabIndex = 0;
            this.HideDataTab.Text = "Скрыть данные";
            this.HideDataTab.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Имя выходного файла:";
            // 
            // outputName
            // 
            this.outputName.Location = new System.Drawing.Point(26, 123);
            this.outputName.Name = "outputName";
            this.outputName.Size = new System.Drawing.Size(303, 20);
            this.outputName.TabIndex = 9;
            // 
            // pBHide
            // 
            this.pBHide.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pBHide.Location = new System.Drawing.Point(26, 242);
            this.pBHide.MarqueeAnimationSpeed = 1;
            this.pBHide.Name = "pBHide";
            this.pBHide.Size = new System.Drawing.Size(385, 28);
            this.pBHide.TabIndex = 8;
            this.pBHide.Visible = false;
            // 
            // hideButton
            // 
            this.hideButton.Location = new System.Drawing.Point(131, 164);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(169, 63);
            this.hideButton.TabIndex = 7;
            this.hideButton.Text = "Скрыть";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
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
            // containerPath
            // 
            this.containerPath.Location = new System.Drawing.Point(26, 79);
            this.containerPath.Name = "containerPath";
            this.containerPath.ReadOnly = true;
            this.containerPath.Size = new System.Drawing.Size(303, 20);
            this.containerPath.TabIndex = 5;
            // 
            // chooseContainer
            // 
            this.chooseContainer.Location = new System.Drawing.Point(336, 77);
            this.chooseContainer.Name = "chooseContainer";
            this.chooseContainer.Size = new System.Drawing.Size(75, 23);
            this.chooseContainer.TabIndex = 4;
            this.chooseContainer.Text = "Обзор";
            this.chooseContainer.UseVisualStyleBackColor = true;
            this.chooseContainer.Click += new System.EventHandler(this.chooseContainer_Click);
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
            // hideFilePath
            // 
            this.hideFilePath.Location = new System.Drawing.Point(26, 37);
            this.hideFilePath.Name = "hideFilePath";
            this.hideFilePath.ReadOnly = true;
            this.hideFilePath.Size = new System.Drawing.Size(303, 20);
            this.hideFilePath.TabIndex = 2;
            // 
            // chooseHideFile
            // 
            this.chooseHideFile.Location = new System.Drawing.Point(336, 35);
            this.chooseHideFile.Name = "chooseHideFile";
            this.chooseHideFile.Size = new System.Drawing.Size(75, 23);
            this.chooseHideFile.TabIndex = 1;
            this.chooseHideFile.Text = "Обзор";
            this.chooseHideFile.UseVisualStyleBackColor = true;
            this.chooseHideFile.Click += new System.EventHandler(this.chooseHideFile_Click);
            // 
            // ExtractData
            // 
            this.ExtractData.Controls.Add(this.pBExtract);
            this.ExtractData.Controls.Add(this.extractButton);
            this.ExtractData.Controls.Add(this.containerExChoose);
            this.ExtractData.Controls.Add(this.label3);
            this.ExtractData.Controls.Add(this.containerExtract);
            this.ExtractData.Location = new System.Drawing.Point(4, 22);
            this.ExtractData.Name = "ExtractData";
            this.ExtractData.Padding = new System.Windows.Forms.Padding(3);
            this.ExtractData.Size = new System.Drawing.Size(431, 291);
            this.ExtractData.TabIndex = 1;
            this.ExtractData.Text = "Извлечь данные";
            this.ExtractData.UseVisualStyleBackColor = true;
            // 
            // pBExtract
            // 
            this.pBExtract.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pBExtract.Location = new System.Drawing.Point(26, 242);
            this.pBExtract.MarqueeAnimationSpeed = 1;
            this.pBExtract.Name = "pBExtract";
            this.pBExtract.Size = new System.Drawing.Size(385, 28);
            this.pBExtract.TabIndex = 9;
            this.pBExtract.Visible = false;
            // 
            // extractButton
            // 
            this.extractButton.Location = new System.Drawing.Point(131, 164);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(169, 63);
            this.extractButton.TabIndex = 8;
            this.extractButton.Text = "Изъять";
            this.extractButton.UseVisualStyleBackColor = true;
            this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
            // 
            // containerExChoose
            // 
            this.containerExChoose.Location = new System.Drawing.Point(336, 35);
            this.containerExChoose.Name = "containerExChoose";
            this.containerExChoose.Size = new System.Drawing.Size(75, 23);
            this.containerExChoose.TabIndex = 2;
            this.containerExChoose.Text = "Обзор";
            this.containerExChoose.UseVisualStyleBackColor = true;
            this.containerExChoose.Click += new System.EventHandler(this.containerExChoose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Контейнер";
            // 
            // containerExtract
            // 
            this.containerExtract.Location = new System.Drawing.Point(26, 37);
            this.containerExtract.Name = "containerExtract";
            this.containerExtract.ReadOnly = true;
            this.containerExtract.Size = new System.Drawing.Size(303, 20);
            this.containerExtract.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 341);
            this.Controls.Add(this.ControlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Стеганография";
            this.ControlPanel.ResumeLayout(false);
            this.HideDataTab.ResumeLayout(false);
            this.HideDataTab.PerformLayout();
            this.ExtractData.ResumeLayout(false);
            this.ExtractData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ControlPanel;
        private System.Windows.Forms.TabPage HideDataTab;
        private System.Windows.Forms.TabPage ExtractData;
        private System.Windows.Forms.Button chooseHideFile;
        private System.Windows.Forms.TextBox hideFilePath;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox containerPath;
        private System.Windows.Forms.Button chooseContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox containerExtract;
        private System.Windows.Forms.Button containerExChoose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.ProgressBar pBHide;
        private System.Windows.Forms.ProgressBar pBExtract;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox outputName;
    }
}

