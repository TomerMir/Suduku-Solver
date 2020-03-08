namespace SudukuSolver
{
    partial class Suduku_Form
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
            this.Solve = new System.Windows.Forms.Button();
            this.SeeSolution = new System.Windows.Forms.CheckBox();
            this.Reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Solve
            // 
            this.Solve.Location = new System.Drawing.Point(203, 2);
            this.Solve.Name = "Solve";
            this.Solve.Size = new System.Drawing.Size(86, 35);
            this.Solve.TabIndex = 1;
            this.Solve.Text = "Solve!";
            this.Solve.UseVisualStyleBackColor = true;
            this.Solve.Click += new System.EventHandler(this.Solve_Click);
            // 
            // SeeSolution
            // 
            this.SeeSolution.AutoSize = true;
            this.SeeSolution.Location = new System.Drawing.Point(52, 12);
            this.SeeSolution.Name = "SeeSolution";
            this.SeeSolution.Size = new System.Drawing.Size(84, 17);
            this.SeeSolution.TabIndex = 2;
            this.SeeSolution.Text = "See solution";
            this.SeeSolution.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SeeSolution.UseVisualStyleBackColor = true;
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(388, 5);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(80, 29);
            this.Reset.TabIndex = 3;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Suduku_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 542);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.SeeSolution);
            this.Controls.Add(this.Solve);
            this.Name = "Suduku_Form";
            this.Text = "Suduku Solver";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Solve;
        private System.Windows.Forms.CheckBox SeeSolution;
        private System.Windows.Forms.Button Reset;
    }
}

