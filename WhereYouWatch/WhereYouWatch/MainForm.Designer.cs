namespace WhereYouWatch
{
    partial class MainForm
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
            this.MakePhotoButton = new System.Windows.Forms.Button();
            this.mainPicture = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.LHFilterButton = new System.Windows.Forms.Button();
            this.addBrightButton = new System.Windows.Forms.Button();
            this.removeBrightButton = new System.Windows.Forms.Button();
            this.lineBoundsButton = new System.Windows.Forms.Button();
            this.realPicture = new System.Windows.Forms.PictureBox();
            this.clarityButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.realPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // MakePhotoButton
            // 
            this.MakePhotoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MakePhotoButton.Location = new System.Drawing.Point(12, 325);
            this.MakePhotoButton.Name = "MakePhotoButton";
            this.MakePhotoButton.Size = new System.Drawing.Size(88, 54);
            this.MakePhotoButton.TabIndex = 3;
            this.MakePhotoButton.Text = "Сделать фото";
            this.MakePhotoButton.UseVisualStyleBackColor = true;
            this.MakePhotoButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainPicture
            // 
            this.mainPicture.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mainPicture.Location = new System.Drawing.Point(270, 29);
            this.mainPicture.Name = "mainPicture";
            this.mainPicture.Size = new System.Drawing.Size(256, 250);
            this.mainPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainPicture.TabIndex = 4;
            this.mainPicture.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(106, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 54);
            this.button1.TabIndex = 5;
            this.button1.Text = "Бинаризировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // LHFilterButton
            // 
            this.LHFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LHFilterButton.Location = new System.Drawing.Point(221, 326);
            this.LHFilterButton.Name = "LHFilterButton";
            this.LHFilterButton.Size = new System.Drawing.Size(117, 53);
            this.LHFilterButton.TabIndex = 6;
            this.LHFilterButton.Text = "Низкочастотный фильтр";
            this.LHFilterButton.UseVisualStyleBackColor = true;
            this.LHFilterButton.Click += new System.EventHandler(this.LHFilterButton_Click);
            // 
            // addBrightButton
            // 
            this.addBrightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBrightButton.Location = new System.Drawing.Point(450, 327);
            this.addBrightButton.Name = "addBrightButton";
            this.addBrightButton.Size = new System.Drawing.Size(75, 23);
            this.addBrightButton.TabIndex = 7;
            this.addBrightButton.Text = "Якрость +";
            this.addBrightButton.UseVisualStyleBackColor = true;
            this.addBrightButton.Click += new System.EventHandler(this.addBrightButton_Click);
            // 
            // removeBrightButton
            // 
            this.removeBrightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeBrightButton.Location = new System.Drawing.Point(450, 356);
            this.removeBrightButton.Name = "removeBrightButton";
            this.removeBrightButton.Size = new System.Drawing.Size(75, 23);
            this.removeBrightButton.TabIndex = 8;
            this.removeBrightButton.Text = "Яркость -";
            this.removeBrightButton.UseVisualStyleBackColor = true;
            this.removeBrightButton.Click += new System.EventHandler(this.removeBrightButton_Click);
            // 
            // lineBoundsButton
            // 
            this.lineBoundsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lineBoundsButton.Location = new System.Drawing.Point(344, 327);
            this.lineBoundsButton.Name = "lineBoundsButton";
            this.lineBoundsButton.Size = new System.Drawing.Size(100, 52);
            this.lineBoundsButton.TabIndex = 9;
            this.lineBoundsButton.Text = "Фильтр подчеркивания границ";
            this.lineBoundsButton.UseVisualStyleBackColor = true;
            this.lineBoundsButton.Click += new System.EventHandler(this.lineBoundsButton_Click);
            // 
            // realPicture
            // 
            this.realPicture.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.realPicture.Location = new System.Drawing.Point(12, 29);
            this.realPicture.Name = "realPicture";
            this.realPicture.Size = new System.Drawing.Size(249, 250);
            this.realPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.realPicture.TabIndex = 10;
            this.realPicture.TabStop = false;
            // 
            // clarityButton
            // 
            this.clarityButton.Location = new System.Drawing.Point(12, 285);
            this.clarityButton.Name = "clarityButton";
            this.clarityButton.Size = new System.Drawing.Size(75, 36);
            this.clarityButton.TabIndex = 11;
            this.clarityButton.Text = "Фильтр четкости";
            this.clarityButton.UseVisualStyleBackColor = true;
            this.clarityButton.Click += new System.EventHandler(this.clarityButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 388);
            this.Controls.Add(this.clarityButton);
            this.Controls.Add(this.realPicture);
            this.Controls.Add(this.lineBoundsButton);
            this.Controls.Add(this.removeBrightButton);
            this.Controls.Add(this.addBrightButton);
            this.Controls.Add(this.LHFilterButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainPicture);
            this.Controls.Add(this.MakePhotoButton);
            this.Name = "MainForm";
            this.Text = "WhereYouWatch";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.realPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button MakePhotoButton;
        private System.Windows.Forms.PictureBox mainPicture;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button LHFilterButton;
        private System.Windows.Forms.Button addBrightButton;
        private System.Windows.Forms.Button removeBrightButton;
        private System.Windows.Forms.Button lineBoundsButton;
        private System.Windows.Forms.PictureBox realPicture;
        private System.Windows.Forms.Button clarityButton;
    }
}

