﻿namespace WhereYouWatch
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
            this.binarizationFilter = new System.Windows.Forms.Button();
            this.LHFilterButton = new System.Windows.Forms.Button();
            this.addBrightButton = new System.Windows.Forms.Button();
            this.removeBrightButton = new System.Windows.Forms.Button();
            this.lineBoundsButton = new System.Windows.Forms.Button();
            this.realPicture = new System.Windows.Forms.PictureBox();
            this.clarityButton = new System.Windows.Forms.Button();
            this.addContrastButton = new System.Windows.Forms.Button();
            this.removeContrastButton = new System.Windows.Forms.Button();
            this.gausButton = new System.Windows.Forms.Button();
            this.medialFilterButton = new System.Windows.Forms.Button();
            this.unsharpFilterButton = new System.Windows.Forms.Button();
            this.robertsFilterButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Detecting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.realPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // MakePhotoButton
            // 
            this.MakePhotoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MakePhotoButton.Location = new System.Drawing.Point(24, 669);
            this.MakePhotoButton.Margin = new System.Windows.Forms.Padding(6);
            this.MakePhotoButton.Name = "MakePhotoButton";
            this.MakePhotoButton.Size = new System.Drawing.Size(176, 104);
            this.MakePhotoButton.TabIndex = 3;
            this.MakePhotoButton.Text = "Сделать фото";
            this.MakePhotoButton.UseVisualStyleBackColor = true;
            this.MakePhotoButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainPicture
            // 
            this.mainPicture.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mainPicture.Location = new System.Drawing.Point(540, 56);
            this.mainPicture.Margin = new System.Windows.Forms.Padding(6);
            this.mainPicture.Name = "mainPicture";
            this.mainPicture.Size = new System.Drawing.Size(512, 481);
            this.mainPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainPicture.TabIndex = 4;
            this.mainPicture.TabStop = false;
            // 
            // binarizationFilter
            // 
            this.binarizationFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.binarizationFilter.Location = new System.Drawing.Point(212, 669);
            this.binarizationFilter.Margin = new System.Windows.Forms.Padding(6);
            this.binarizationFilter.Name = "binarizationFilter";
            this.binarizationFilter.Size = new System.Drawing.Size(218, 104);
            this.binarizationFilter.TabIndex = 5;
            this.binarizationFilter.Text = "Бинаризировать";
            this.binarizationFilter.UseVisualStyleBackColor = true;
            this.binarizationFilter.Click += new System.EventHandler(this.binarizationFilter_Click);
            // 
            // LHFilterButton
            // 
            this.LHFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LHFilterButton.Location = new System.Drawing.Point(442, 671);
            this.LHFilterButton.Margin = new System.Windows.Forms.Padding(6);
            this.LHFilterButton.Name = "LHFilterButton";
            this.LHFilterButton.Size = new System.Drawing.Size(234, 102);
            this.LHFilterButton.TabIndex = 6;
            this.LHFilterButton.Text = "Низкочастотный фильтр";
            this.LHFilterButton.UseVisualStyleBackColor = true;
            this.LHFilterButton.Click += new System.EventHandler(this.LHFilterButton_Click);
            // 
            // addBrightButton
            // 
            this.addBrightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBrightButton.Location = new System.Drawing.Point(900, 673);
            this.addBrightButton.Margin = new System.Windows.Forms.Padding(6);
            this.addBrightButton.Name = "addBrightButton";
            this.addBrightButton.Size = new System.Drawing.Size(150, 44);
            this.addBrightButton.TabIndex = 7;
            this.addBrightButton.Text = "Якрость +";
            this.addBrightButton.UseVisualStyleBackColor = true;
            this.addBrightButton.Click += new System.EventHandler(this.addBrightButton_Click);
            // 
            // removeBrightButton
            // 
            this.removeBrightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeBrightButton.Location = new System.Drawing.Point(900, 729);
            this.removeBrightButton.Margin = new System.Windows.Forms.Padding(6);
            this.removeBrightButton.Name = "removeBrightButton";
            this.removeBrightButton.Size = new System.Drawing.Size(150, 44);
            this.removeBrightButton.TabIndex = 8;
            this.removeBrightButton.Text = "Яркость -";
            this.removeBrightButton.UseVisualStyleBackColor = true;
            this.removeBrightButton.Click += new System.EventHandler(this.removeBrightButton_Click);
            // 
            // lineBoundsButton
            // 
            this.lineBoundsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lineBoundsButton.Location = new System.Drawing.Point(688, 673);
            this.lineBoundsButton.Margin = new System.Windows.Forms.Padding(6);
            this.lineBoundsButton.Name = "lineBoundsButton";
            this.lineBoundsButton.Size = new System.Drawing.Size(200, 100);
            this.lineBoundsButton.TabIndex = 9;
            this.lineBoundsButton.Text = "Фильтр подчеркивания границ";
            this.lineBoundsButton.UseVisualStyleBackColor = true;
            this.lineBoundsButton.Click += new System.EventHandler(this.lineBoundsButton_Click);
            // 
            // realPicture
            // 
            this.realPicture.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.realPicture.Location = new System.Drawing.Point(24, 56);
            this.realPicture.Margin = new System.Windows.Forms.Padding(6);
            this.realPicture.Name = "realPicture";
            this.realPicture.Size = new System.Drawing.Size(498, 481);
            this.realPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.realPicture.TabIndex = 10;
            this.realPicture.TabStop = false;
            // 
            // clarityButton
            // 
            this.clarityButton.Location = new System.Drawing.Point(24, 548);
            this.clarityButton.Margin = new System.Windows.Forms.Padding(6);
            this.clarityButton.Name = "clarityButton";
            this.clarityButton.Size = new System.Drawing.Size(176, 98);
            this.clarityButton.TabIndex = 11;
            this.clarityButton.Text = "Фильтр четкости";
            this.clarityButton.UseVisualStyleBackColor = true;
            this.clarityButton.Click += new System.EventHandler(this.clarityButton_Click);
            // 
            // addContrastButton
            // 
            this.addContrastButton.Location = new System.Drawing.Point(900, 550);
            this.addContrastButton.Margin = new System.Windows.Forms.Padding(6);
            this.addContrastButton.Name = "addContrastButton";
            this.addContrastButton.Size = new System.Drawing.Size(150, 44);
            this.addContrastButton.TabIndex = 12;
            this.addContrastButton.Text = "Контраст +";
            this.addContrastButton.UseVisualStyleBackColor = true;
            this.addContrastButton.Click += new System.EventHandler(this.addContrastButton_Click);
            // 
            // removeContrastButton
            // 
            this.removeContrastButton.Location = new System.Drawing.Point(900, 606);
            this.removeContrastButton.Margin = new System.Windows.Forms.Padding(6);
            this.removeContrastButton.Name = "removeContrastButton";
            this.removeContrastButton.Size = new System.Drawing.Size(150, 44);
            this.removeContrastButton.TabIndex = 13;
            this.removeContrastButton.Text = "Контраст -";
            this.removeContrastButton.UseVisualStyleBackColor = true;
            this.removeContrastButton.Click += new System.EventHandler(this.removeContrastButton_Click);
            // 
            // gausButton
            // 
            this.gausButton.Location = new System.Drawing.Point(212, 548);
            this.gausButton.Margin = new System.Windows.Forms.Padding(6);
            this.gausButton.Name = "gausButton";
            this.gausButton.Size = new System.Drawing.Size(218, 98);
            this.gausButton.TabIndex = 14;
            this.gausButton.Text = "Фильтр Гаусса";
            this.gausButton.UseVisualStyleBackColor = true;
            this.gausButton.Click += new System.EventHandler(this.gausButton_Click);
            // 
            // medialFilterButton
            // 
            this.medialFilterButton.Location = new System.Drawing.Point(442, 550);
            this.medialFilterButton.Margin = new System.Windows.Forms.Padding(6);
            this.medialFilterButton.Name = "medialFilterButton";
            this.medialFilterButton.Size = new System.Drawing.Size(234, 96);
            this.medialFilterButton.TabIndex = 15;
            this.medialFilterButton.Text = "Медианный фильтр";
            this.medialFilterButton.UseVisualStyleBackColor = true;
            this.medialFilterButton.Click += new System.EventHandler(this.medialFilterButton_Click);
            // 
            // unsharpFilterButton
            // 
            this.unsharpFilterButton.Location = new System.Drawing.Point(690, 548);
            this.unsharpFilterButton.Margin = new System.Windows.Forms.Padding(6);
            this.unsharpFilterButton.Name = "unsharpFilterButton";
            this.unsharpFilterButton.Size = new System.Drawing.Size(198, 98);
            this.unsharpFilterButton.TabIndex = 16;
            this.unsharpFilterButton.Text = "Фильтр повышения резкости";
            this.unsharpFilterButton.UseVisualStyleBackColor = true;
            this.unsharpFilterButton.Click += new System.EventHandler(this.unsharpFilterButton_Click);
            // 
            // robertsFilterButton
            // 
            this.robertsFilterButton.Location = new System.Drawing.Point(1064, 550);
            this.robertsFilterButton.Margin = new System.Windows.Forms.Padding(6);
            this.robertsFilterButton.Name = "robertsFilterButton";
            this.robertsFilterButton.Size = new System.Drawing.Size(150, 44);
            this.robertsFilterButton.TabIndex = 17;
            this.robertsFilterButton.Text = "Робертса";
            this.robertsFilterButton.UseVisualStyleBackColor = true;
            this.robertsFilterButton.Click += new System.EventHandler(this.robertsFilterButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // Detecting
            // 
            this.Detecting.Location = new System.Drawing.Point(1077, 111);
            this.Detecting.Name = "Detecting";
            this.Detecting.Size = new System.Drawing.Size(121, 113);
            this.Detecting.TabIndex = 18;
            this.Detecting.Text = "Detect Face";
            this.Detecting.UseVisualStyleBackColor = true;
            this.Detecting.Click += new System.EventHandler(this.Detecting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 790);
            this.Controls.Add(this.Detecting);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.robertsFilterButton);
            this.Controls.Add(this.unsharpFilterButton);
            this.Controls.Add(this.medialFilterButton);
            this.Controls.Add(this.gausButton);
            this.Controls.Add(this.removeContrastButton);
            this.Controls.Add(this.addContrastButton);
            this.Controls.Add(this.clarityButton);
            this.Controls.Add(this.realPicture);
            this.Controls.Add(this.lineBoundsButton);
            this.Controls.Add(this.removeBrightButton);
            this.Controls.Add(this.addBrightButton);
            this.Controls.Add(this.LHFilterButton);
            this.Controls.Add(this.binarizationFilter);
            this.Controls.Add(this.mainPicture);
            this.Controls.Add(this.MakePhotoButton);
            this.Margin = new System.Windows.Forms.Padding(6);
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
        private System.Windows.Forms.Button binarizationFilter;
        private System.Windows.Forms.Button LHFilterButton;
        private System.Windows.Forms.Button addBrightButton;
        private System.Windows.Forms.Button removeBrightButton;
        private System.Windows.Forms.Button lineBoundsButton;
        private System.Windows.Forms.PictureBox realPicture;
        private System.Windows.Forms.Button clarityButton;
        private System.Windows.Forms.Button addContrastButton;
        private System.Windows.Forms.Button removeContrastButton;
        private System.Windows.Forms.Button gausButton;
        private System.Windows.Forms.Button medialFilterButton;
        private System.Windows.Forms.Button unsharpFilterButton;
        private System.Windows.Forms.Button robertsFilterButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Detecting;
    }
}

