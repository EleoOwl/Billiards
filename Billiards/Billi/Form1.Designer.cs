namespace Billi
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
            this.components = new System.ComponentModel.Container();
            this.TIMER = new System.Windows.Forms.Timer(this.components);
            this.TimesHit = new System.Windows.Forms.Timer(this.components);
            this.Plus = new System.Windows.Forms.Label();
            this.shtr = new System.Windows.Forms.Label();
            this.timelabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TIMER
            // 
            this.TIMER.Interval = 50;
            this.TIMER.Tick += new System.EventHandler(this.TIMER_Tick);
            // 
            // TimesHit
            // 
            this.TimesHit.Interval = 10;
            this.TimesHit.Tick += new System.EventHandler(this.TimesHit_Tick);
            // 
            // Plus
            // 
            this.Plus.AutoSize = true;
            this.Plus.Font = new System.Drawing.Font("OpenSymbol", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Plus.Location = new System.Drawing.Point(67, -1);
            this.Plus.Name = "Plus";
            this.Plus.Size = new System.Drawing.Size(280, 19);
            this.Plus.TabIndex = 1;
            this.Plus.Text = "Количество набранных очков:  0";
            // 
            // shtr
            // 
            this.shtr.AutoSize = true;
            this.shtr.Font = new System.Drawing.Font("OpenSymbol", 14.25F, System.Drawing.FontStyle.Bold);
            this.shtr.Location = new System.Drawing.Point(444, -1);
            this.shtr.Name = "shtr";
            this.shtr.Size = new System.Drawing.Size(276, 19);
            this.shtr.TabIndex = 2;
            this.shtr.Text = "Количество штрафных очков:  0";
            // 
            // timelabel
            // 
            this.timelabel.AutoSize = true;
            this.timelabel.Font = new System.Drawing.Font("OpenSymbol", 14.25F, System.Drawing.FontStyle.Bold);
            this.timelabel.Location = new System.Drawing.Point(101, 465);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(196, 19);
            this.timelabel.TabIndex = 3;
            this.timelabel.Text = "Ускорение времени: х1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pictureBox1.Image = global::Billi.Properties.Resources.table3;
            this.pictureBox1.Location = new System.Drawing.Point(1, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(799, 492);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(797, 490);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.shtr);
            this.Controls.Add(this.Plus);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer TIMER;
        private System.Windows.Forms.Timer TimesHit;
        private System.Windows.Forms.Label Plus;
        private System.Windows.Forms.Label shtr;
        private System.Windows.Forms.Label timelabel;
    }
}

