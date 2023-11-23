namespace GKLab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBox = new PictureBox();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            labelM = new Label();
            labelKs = new Label();
            labelKd = new Label();
            trackBarM = new TrackBar();
            trackBarKs = new TrackBar();
            trackBarKd = new TrackBar();
            groupBox1 = new GroupBox();
            labelY = new Label();
            labelX = new Label();
            trackBarY = new TrackBar();
            trackBarX = new TrackBar();
            colorDialog = new ColorDialog();
            objectColorButton = new Button();
            lightColorButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarKs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarKd).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarX).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.Controls.Add(pictureBox, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(908, 579);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(3, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(625, 575);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(lightColorButton);
            panel1.Controls.Add(objectColorButton);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(638, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(267, 579);
            panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(labelM);
            groupBox2.Controls.Add(labelKs);
            groupBox2.Controls.Add(labelKd);
            groupBox2.Controls.Add(trackBarM);
            groupBox2.Controls.Add(trackBarKs);
            groupBox2.Controls.Add(trackBarKd);
            groupBox2.Location = new Point(8, 135);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(250, 150);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Light properties";
            // 
            // labelM
            // 
            labelM.AutoSize = true;
            labelM.Location = new Point(6, 106);
            labelM.Name = "labelM";
            labelM.Size = new Size(37, 20);
            labelM.TabIndex = 5;
            labelM.Text = "m: 1";
            // 
            // labelKs
            // 
            labelKs.AutoSize = true;
            labelKs.Location = new Point(5, 67);
            labelKs.Name = "labelKs";
            labelKs.Size = new Size(39, 20);
            labelKs.TabIndex = 4;
            labelKs.Text = "Ks: 0";
            // 
            // labelKd
            // 
            labelKd.AutoSize = true;
            labelKd.Location = new Point(6, 26);
            labelKd.Name = "labelKd";
            labelKd.Size = new Size(42, 20);
            labelKd.TabIndex = 3;
            labelKd.Text = "Kd: 0";
            // 
            // trackBarM
            // 
            trackBarM.Location = new Point(66, 106);
            trackBarM.Name = "trackBarM";
            trackBarM.Size = new Size(173, 56);
            trackBarM.TabIndex = 2;
            trackBarM.ValueChanged += trackBarM_ValueChanged;
            // 
            // trackBarKs
            // 
            trackBarKs.Location = new Point(66, 67);
            trackBarKs.Name = "trackBarKs";
            trackBarKs.Size = new Size(173, 56);
            trackBarKs.TabIndex = 1;
            trackBarKs.ValueChanged += trackBarKs_ValueChanged;
            // 
            // trackBarKd
            // 
            trackBarKd.Location = new Point(66, 26);
            trackBarKd.Name = "trackBarKd";
            trackBarKd.Size = new Size(173, 56);
            trackBarKd.TabIndex = 0;
            trackBarKd.ValueChanged += trackBarKd_ValueChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelY);
            groupBox1.Controls.Add(labelX);
            groupBox1.Controls.Add(trackBarY);
            groupBox1.Controls.Add(trackBarX);
            groupBox1.Location = new Point(8, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(250, 126);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Triangulation";
            // 
            // labelY
            // 
            labelY.AutoSize = true;
            labelY.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelY.ForeColor = SystemColors.ControlText;
            labelY.Location = new Point(6, 81);
            labelY.Name = "labelY";
            labelY.Size = new Size(37, 23);
            labelY.TabIndex = 3;
            labelY.Text = "Y: 4";
            // 
            // labelX
            // 
            labelX.AutoSize = true;
            labelX.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelX.Location = new Point(6, 35);
            labelX.Name = "labelX";
            labelX.Size = new Size(38, 23);
            labelX.TabIndex = 2;
            labelX.Text = "X: 4";
            // 
            // trackBarY
            // 
            trackBarY.Location = new Point(66, 81);
            trackBarY.Name = "trackBarY";
            trackBarY.Size = new Size(173, 56);
            trackBarY.TabIndex = 1;
            trackBarY.ValueChanged += trackBarY_ValueChanged;
            // 
            // trackBarX
            // 
            trackBarX.Location = new Point(66, 35);
            trackBarX.Name = "trackBarX";
            trackBarX.Size = new Size(178, 56);
            trackBarX.TabIndex = 0;
            trackBarX.ValueChanged += trackBarX_ValueChanged;
            // 
            // objectColorButton
            // 
            objectColorButton.Location = new Point(8, 296);
            objectColorButton.Name = "objectColorButton";
            objectColorButton.Size = new Size(100, 29);
            objectColorButton.TabIndex = 2;
            objectColorButton.Text = "Object color";
            objectColorButton.UseVisualStyleBackColor = true;
            objectColorButton.Click += objectColorButton_Click;
            // 
            // lightColorButton
            // 
            lightColorButton.Location = new Point(148, 296);
            lightColorButton.Name = "lightColorButton";
            lightColorButton.Size = new Size(99, 29);
            lightColorButton.TabIndex = 3;
            lightColorButton.Text = "Light color";
            lightColorButton.UseVisualStyleBackColor = true;
            lightColorButton.Click += lightColorButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(932, 603);
            Controls.Add(tableLayoutPanel1);
            MaximumSize = new Size(950, 650);
            MinimumSize = new Size(950, 650);
            Name = "Form1";
            Text = "Light simulation";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarM).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarKs).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarKd).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarY).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarX).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox;
        private Panel panel1;
        private GroupBox groupBox1;
        private TrackBar trackBarY;
        private TrackBar trackBarX;
        private Label labelX;
        private Label labelY;
        private GroupBox groupBox2;
        private TrackBar trackBarKd;
        private TrackBar trackBarKs;
        private TrackBar trackBarM;
        private Label labelKd;
        private Label labelKs;
        private Label labelM;
        private ColorDialog colorDialog;
        private Button lightColorButton;
        private Button objectColorButton;
    }
}