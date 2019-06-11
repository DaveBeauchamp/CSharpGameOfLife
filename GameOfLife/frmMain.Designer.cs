namespace GameOfLife
{
    partial class frmMain
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
            this.btnTest = new System.Windows.Forms.Button();
            this.pbxGoLBoard = new System.Windows.Forms.PictureBox();
            this.btnPauseLife = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGoLBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(87, 839);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click_1);
            // 
            // pbxGoLBoard
            // 
            this.pbxGoLBoard.Location = new System.Drawing.Point(12, 12);
            this.pbxGoLBoard.Name = "pbxGoLBoard";
            this.pbxGoLBoard.Size = new System.Drawing.Size(810, 810);
            this.pbxGoLBoard.TabIndex = 2;
            this.pbxGoLBoard.TabStop = false;
            this.pbxGoLBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxGoLBoard_Paint);
            // 
            // btnPauseLife
            // 
            this.btnPauseLife.Location = new System.Drawing.Point(196, 839);
            this.btnPauseLife.Name = "btnPauseLife";
            this.btnPauseLife.Size = new System.Drawing.Size(75, 23);
            this.btnPauseLife.TabIndex = 3;
            this.btnPauseLife.Text = "Pause";
            this.btnPauseLife.UseVisualStyleBackColor = true;
            this.btnPauseLife.Click += new System.EventHandler(this.btnPauseLife_Click_1);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 911);
            this.Controls.Add(this.btnPauseLife);
            this.Controls.Add(this.pbxGoLBoard);
            this.Controls.Add(this.btnTest);
            this.Name = "frmMain";
            this.Text = "Game Of Life";
            ((System.ComponentModel.ISupportInitialize)(this.pbxGoLBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.PictureBox pbxGoLBoard;
        private System.Windows.Forms.Button btnPauseLife;
    }
}

