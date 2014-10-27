namespace IP_GUI
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabMenu = new System.Windows.Forms.TabControl();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.ScaleYTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ShearYTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.ShearXTB = new System.Windows.Forms.TextBox();
            this.RotateTB = new System.Windows.Forms.TextBox();
            this.ScaleXTB = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.filePath = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.BeforeBox = new System.Windows.Forms.PictureBox();
            this.AfterBox = new System.Windows.Forms.PictureBox();
            this.Before = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TabMenu.SuspendLayout();
            this.tab2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeforeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AfterBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TabMenu
            // 
            this.TabMenu.Controls.Add(this.tab2);
            this.TabMenu.Controls.Add(this.tabPage1);
            this.TabMenu.Location = new System.Drawing.Point(12, 12);
            this.TabMenu.Name = "TabMenu";
            this.TabMenu.SelectedIndex = 0;
            this.TabMenu.Size = new System.Drawing.Size(236, 685);
            this.TabMenu.TabIndex = 0;
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.label5);
            this.tab2.Controls.Add(this.ScaleYTB);
            this.tab2.Controls.Add(this.label4);
            this.tab2.Controls.Add(this.ShearYTB);
            this.tab2.Controls.Add(this.label3);
            this.tab2.Controls.Add(this.label2);
            this.tab2.Controls.Add(this.label1);
            this.tab2.Controls.Add(this.ApplyButton);
            this.tab2.Controls.Add(this.ShearXTB);
            this.tab2.Controls.Add(this.RotateTB);
            this.tab2.Controls.Add(this.ScaleXTB);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(228, 659);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "Transformations";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Scale Y";
            // 
            // ScaleYTB
            // 
            this.ScaleYTB.Location = new System.Drawing.Point(115, 28);
            this.ScaleYTB.Name = "ScaleYTB";
            this.ScaleYTB.Size = new System.Drawing.Size(100, 20);
            this.ScaleYTB.TabIndex = 9;
            this.ScaleYTB.Text = "1.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Shear Y-Axis";
            // 
            // ShearYTB
            // 
            this.ShearYTB.Location = new System.Drawing.Point(115, 130);
            this.ShearYTB.Name = "ShearYTB";
            this.ShearYTB.Size = new System.Drawing.Size(100, 20);
            this.ShearYTB.TabIndex = 7;
            this.ShearYTB.Text = "0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Shear X-Axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rotation (Degrees/Anticlockise)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scale X";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(43, 260);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(104, 55);
            this.ApplyButton.TabIndex = 3;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // ShearXTB
            // 
            this.ShearXTB.Location = new System.Drawing.Point(9, 130);
            this.ShearXTB.Name = "ShearXTB";
            this.ShearXTB.Size = new System.Drawing.Size(100, 20);
            this.ShearXTB.TabIndex = 2;
            this.ShearXTB.Text = "0.0";
            // 
            // RotateTB
            // 
            this.RotateTB.Location = new System.Drawing.Point(9, 78);
            this.RotateTB.Name = "RotateTB";
            this.RotateTB.Size = new System.Drawing.Size(100, 20);
            this.RotateTB.TabIndex = 1;
            this.RotateTB.Text = "0.0";
            // ScaleXTB
            // 
            this.ScaleXTB.Location = new System.Drawing.Point(9, 28);
            this.ScaleXTB.Name = "ScaleXTB";
            this.ScaleXTB.Size = new System.Drawing.Size(100, 20);
            this.ScaleXTB.TabIndex = 0;
            this.ScaleXTB.Text = "1.0";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.filePath);
            this.tabPage1.Controls.Add(this.BrowseButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(228, 659);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Open";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(7, 54);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(215, 20);
            this.filePath.TabIndex = 6;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(7, 80);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(81, 32);
            this.BrowseButton.TabIndex = 5;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // BeforeBox
            // 
            this.BeforeBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BeforeBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BeforeBox.Location = new System.Drawing.Point(270, 112);
            this.BeforeBox.Name = "BeforeBox";
            this.BeforeBox.Size = new System.Drawing.Size(530, 414);
            this.BeforeBox.TabIndex = 1;
            this.BeforeBox.TabStop = false;
            // 
            // AfterBox
            // 
            this.AfterBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.AfterBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AfterBox.Location = new System.Drawing.Point(806, 112);
            this.AfterBox.Name = "AfterBox";
            this.AfterBox.Size = new System.Drawing.Size(530, 414);
            this.AfterBox.TabIndex = 2;
            this.AfterBox.TabStop = false;
            // 
            // Before
            // 
            this.Before.AutoSize = true;
            this.Before.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Before.Location = new System.Drawing.Point(264, 78);
            this.Before.Name = "Before";
            this.Before.Size = new System.Drawing.Size(94, 31);
            this.Before.TabIndex = 3;
            this.Before.Text = "Before";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(800, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 31);
            this.label6.TabIndex = 4;
            this.label6.Text = "After";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 711);
            this.Controls.Add(this.BeforeBox);
            this.Controls.Add(this.AfterBox);
            this.Controls.Add(this.TabMenu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Before);
            this.Name = "GUI";
            this.Text = "IP Task 1";
            this.TabMenu.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeforeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AfterBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabMenu;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.TextBox ScaleXTB;
        private System.Windows.Forms.TextBox ShearXTB;
        private System.Windows.Forms.TextBox RotateTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ShearYTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ScaleYTB;
        private System.Windows.Forms.PictureBox BeforeBox;
        private System.Windows.Forms.PictureBox AfterBox;
        private System.Windows.Forms.Label Before;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button BrowseButton;
    }
}

