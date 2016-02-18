namespace CodeProject
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.cmdCreateFiles = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumFiles = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdEnumTest4 = new System.Windows.Forms.Button();
            this.cmdEnumTest2 = new System.Windows.Forms.Button();
            this.cmdEnumTest3 = new System.Windows.Forms.Button();
            this.cmdEnumTest1 = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSearchSubdirectories = new System.Windows.Forms.CheckBox();
            this.chkSearchSubdirectories.Checked = true;
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumFiles)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory:";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.Location = new System.Drawing.Point(93, 15);
            this.txtDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(520, 23);
            this.txtDirectory.TabIndex = 1;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Location = new System.Drawing.Point(623, 12);
            this.cmdBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(64, 28);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.Text = "...";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // cmdCreateFiles
            // 
            this.cmdCreateFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreateFiles.Location = new System.Drawing.Point(31, 74);
            this.cmdCreateFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdCreateFiles.Name = "cmdCreateFiles";
            this.cmdCreateFiles.Size = new System.Drawing.Size(612, 42);
            this.cmdCreateFiles.TabIndex = 2;
            this.cmdCreateFiles.Text = "Create Files";
            this.cmdCreateFiles.UseVisualStyleBackColor = true;
            this.cmdCreateFiles.Click += new System.EventHandler(this.cmdCreateFiles_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number of Files to create:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtNumFiles);
            this.groupBox1.Controls.Add(this.cmdCreateFiles);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(20, 106);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(667, 123);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Files";
            // 
            // txtNumFiles
            // 
            this.txtNumFiles.Location = new System.Drawing.Point(205, 38);
            this.txtNumFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNumFiles.Maximum = new decimal(new int[] {
            2000000,
            0,
            0,
            0});
            this.txtNumFiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumFiles.Name = "txtNumFiles";
            this.txtNumFiles.Size = new System.Drawing.Size(160, 23);
            this.txtNumFiles.TabIndex = 1;
            this.txtNumFiles.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cmdEnumTest4);
            this.groupBox2.Controls.Add(this.cmdEnumTest2);
            this.groupBox2.Controls.Add(this.cmdEnumTest3);
            this.groupBox2.Controls.Add(this.cmdEnumTest1);
            this.groupBox2.Location = new System.Drawing.Point(20, 261);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(667, 252);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enum Files";
            // 
            // cmdEnumTest4
            // 
            this.cmdEnumTest4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnumTest4.Location = new System.Drawing.Point(31, 182);
            this.cmdEnumTest4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdEnumTest4.Name = "cmdEnumTest4";
            this.cmdEnumTest4.Size = new System.Drawing.Size(612, 42);
            this.cmdEnumTest4.TabIndex = 3;
            this.cmdEnumTest4.Text = "Enum with FastDirectoryEnumerator.GetFiles";
            this.cmdEnumTest4.UseVisualStyleBackColor = true;
            this.cmdEnumTest4.Click += new System.EventHandler(this.cmdEnumTest4_Click);
            // 
            // cmdEnumTest2
            // 
            this.cmdEnumTest2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnumTest2.Location = new System.Drawing.Point(31, 84);
            this.cmdEnumTest2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdEnumTest2.Name = "cmdEnumTest2";
            this.cmdEnumTest2.Size = new System.Drawing.Size(612, 42);
            this.cmdEnumTest2.TabIndex = 1;
            this.cmdEnumTest2.Text = "Enum with DirectoryInfo.GetFiles";
            this.cmdEnumTest2.UseVisualStyleBackColor = true;
            this.cmdEnumTest2.Click += new System.EventHandler(this.cmdEnumTest3_Click);
            // 
            // cmdEnumTest3
            // 
            this.cmdEnumTest3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnumTest3.Location = new System.Drawing.Point(31, 133);
            this.cmdEnumTest3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdEnumTest3.Name = "cmdEnumTest3";
            this.cmdEnumTest3.Size = new System.Drawing.Size(612, 42);
            this.cmdEnumTest3.TabIndex = 2;
            this.cmdEnumTest3.Text = "Enum with FastDirectoryEnumerator.EnumFiles";
            this.cmdEnumTest3.UseVisualStyleBackColor = true;
            this.cmdEnumTest3.Click += new System.EventHandler(this.cmdEnumTest2_Click);
            // 
            // cmdEnumTest1
            // 
            this.cmdEnumTest1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnumTest1.Location = new System.Drawing.Point(31, 34);
            this.cmdEnumTest1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdEnumTest1.Name = "cmdEnumTest1";
            this.cmdEnumTest1.Size = new System.Drawing.Size(612, 42);
            this.cmdEnumTest1.TabIndex = 0;
            this.cmdEnumTest1.Text = "Enum with Directory.GetFiles";
            this.cmdEnumTest1.UseVisualStyleBackColor = true;
            this.cmdEnumTest1.Click += new System.EventHandler(this.cmdEnumTest1_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(93, 47);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(520, 23);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Filter:";
            // 
            // chkSearchSubdirectories
            // 
            this.chkSearchSubdirectories.AutoSize = true;
            this.chkSearchSubdirectories.Location = new System.Drawing.Point(93, 79);
            this.chkSearchSubdirectories.Name = "chkSearchSubdirectories";
            this.chkSearchSubdirectories.Size = new System.Drawing.Size(172, 20);
            this.chkSearchSubdirectories.TabIndex = 5;
            this.chkSearchSubdirectories.Text = "Search Subdirectories";
            this.chkSearchSubdirectories.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 535);
            this.Controls.Add(this.chkSearchSubdirectories);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(417, 478);
            this.Name = "Form1";
            this.Text = "Directory Enumerator Test Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumFiles)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.Button cmdCreateFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown txtNumFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdEnumTest3;
        private System.Windows.Forms.Button cmdEnumTest1;
        private System.Windows.Forms.Button cmdEnumTest2;
        private System.Windows.Forms.Button cmdEnumTest4;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSearchSubdirectories;
    }
}

