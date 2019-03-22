using System;
using System.Windows.Forms;
using ContestProject.Properties;
using System.IO;

namespace ContestProject
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();

            this.t_InputFile.AllowDrop = true;
            this.t_InputFile.DragDrop += new DragEventHandler(HandleDragDrop);
            this.t_InputFile.DragEnter += new DragEventHandler(HandleDragEnter);
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.t_InputFile.Text = files[0];
        }

        private void ProceedClick(object sender, EventArgs e)
        {
            Code algorithm = new Code();

            if (!string.IsNullOrWhiteSpace(t_InputFile.Text) && File.Exists(t_InputFile.Text))
            {
                algorithm.ExecuteFile(t_InputFile.Text);
            }
            else
            {
                algorithm.ExecuteLine(t_input.Text);

                t_output.Text = algorithm.Output;

                CopyTextBoxContentToClipboard(t_output);
            }
        }

        private void PasteClipboardClick(object sender, EventArgs e)
        {
            t_input.Text = Clipboard.GetText();
        }

        private void CopyToClipboardClick(object sender, EventArgs e)
        {
            CopyTextBoxContentToClipboard(t_output);
        }

        private void CopyTextBoxContentToClipboard(TextBox tb)
        {
            if (!string.IsNullOrWhiteSpace(tb.Text))
                Clipboard.SetText(tb.Text);
            else
                Clipboard.Clear();
        }
    }
}
