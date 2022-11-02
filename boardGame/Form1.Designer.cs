
namespace boardGame
{
    partial class othForm
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
            this.othBoard = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.picTurn = new System.Windows.Forms.PictureBox();
            this.lblTurn = new System.Windows.Forms.Label();
            this.textRec = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.othBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTurn)).BeginInit();
            this.SuspendLayout();
            // 
            // othBoard
            // 
            this.othBoard.Location = new System.Drawing.Point(50, 50);
            this.othBoard.Name = "othBoard";
            this.othBoard.Size = new System.Drawing.Size(450, 450);
            this.othBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.othBoard.TabIndex = 0;
            this.othBoard.TabStop = false;
            this.othBoard.Click += new System.EventHandler(this.othBoard_Click);
            this.othBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.othBoard_Paint);
            this.othBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.othBoard_MouseClick);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.btnStart.Location = new System.Drawing.Point(668, 391);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(102, 51);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // picTurn
            // 
            this.picTurn.Location = new System.Drawing.Point(689, 91);
            this.picTurn.Name = "picTurn";
            this.picTurn.Size = new System.Drawing.Size(50, 50);
            this.picTurn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTurn.TabIndex = 2;
            this.picTurn.TabStop = false;
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.lblTurn.Location = new System.Drawing.Point(684, 61);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(62, 27);
            this.lblTurn.TabIndex = 3;
            this.lblTurn.Text = "Turn";
            this.lblTurn.Click += new System.EventHandler(this.lblTurn_Click);
            // 
            // textRec
            // 
            this.textRec.Location = new System.Drawing.Point(790, 50);
            this.textRec.Multiline = true;
            this.textRec.Name = "textRec";
            this.textRec.Size = new System.Drawing.Size(100, 450);
            this.textRec.TabIndex = 4;
            // 
            // othForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 553);
            this.Controls.Add(this.textRec);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.picTurn);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.othBoard);
            this.Name = "othForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.othBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTurn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox othBoard;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox picTurn;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.TextBox textRec;
    }
}

