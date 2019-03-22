namespace ContestProject
{
    partial class InputForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.t_input = new System.Windows.Forms.TextBox();
            this.t_output = new System.Windows.Forms.TextBox();
            this.b_proceed = new System.Windows.Forms.Button();
            this.pasteClipboard = new System.Windows.Forms.Button();
            this.copyToClipboard = new System.Windows.Forms.Button();
            this.t_InputFile = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Controls.Add(this.t_input, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.t_output, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.b_proceed, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pasteClipboard, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.copyToClipboard, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(743, 331);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // t_input
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.t_input, 2);
            this.t_input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t_input.Location = new System.Drawing.Point(33, 3);
            this.t_input.Multiline = true;
            this.t_input.Name = "t_input";
            this.t_input.Size = new System.Drawing.Size(707, 145);
            this.t_input.TabIndex = 0;
            // 
            // t_output
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.t_output, 2);
            this.t_output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t_output.Location = new System.Drawing.Point(3, 183);
            this.t_output.Multiline = true;
            this.t_output.Name = "t_output";
            this.t_output.Size = new System.Drawing.Size(707, 145);
            this.t_output.TabIndex = 1;
            // 
            // b_proceed
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.b_proceed, 3);
            this.b_proceed.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_proceed.Location = new System.Drawing.Point(3, 154);
            this.b_proceed.Name = "b_proceed";
            this.b_proceed.Size = new System.Drawing.Size(737, 23);
            this.b_proceed.TabIndex = 2;
            this.b_proceed.Text = "proceed";
            this.b_proceed.UseVisualStyleBackColor = true;
            this.b_proceed.Click += new System.EventHandler(this.ProceedClick);
            // 
            // pasteClipboard
            // 
            this.pasteClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pasteClipboard.Location = new System.Drawing.Point(3, 3);
            this.pasteClipboard.Name = "pasteClipboard";
            this.pasteClipboard.Size = new System.Drawing.Size(24, 145);
            this.pasteClipboard.TabIndex = 3;
            this.pasteClipboard.Text = "p a s t e";
            this.pasteClipboard.UseVisualStyleBackColor = true;
            this.pasteClipboard.Click += new System.EventHandler(this.PasteClipboardClick);
            // 
            // copyToClipboard
            // 
            this.copyToClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copyToClipboard.Location = new System.Drawing.Point(716, 183);
            this.copyToClipboard.Name = "copyToClipboard";
            this.copyToClipboard.Size = new System.Drawing.Size(24, 145);
            this.copyToClipboard.TabIndex = 4;
            this.copyToClipboard.Text = "c o p y";
            this.copyToClipboard.UseVisualStyleBackColor = true;
            this.copyToClipboard.Click += new System.EventHandler(this.CopyToClipboardClick);
            // 
            // t_InputFile
            // 
            this.t_InputFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.t_InputFile.Location = new System.Drawing.Point(0, 0);
            this.t_InputFile.Name = "t_InputFile";
            this.t_InputFile.Size = new System.Drawing.Size(743, 20);
            this.t_InputFile.TabIndex = 5;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 351);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.t_InputFile);
            this.Name = "InputForm";
            this.Text = "Coding Contest";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox t_input;
        private System.Windows.Forms.TextBox t_output;
        private System.Windows.Forms.Button b_proceed;
        private System.Windows.Forms.Button pasteClipboard;
        private System.Windows.Forms.Button copyToClipboard;
        private System.Windows.Forms.TextBox t_InputFile;
    }
}

