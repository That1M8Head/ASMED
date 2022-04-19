using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace A2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void fontSelect()
        {
            fontDialog1.Font = textArea.Font;
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                if (fontDialog1.Font.Size >= 12)
                {
                    textArea.Font = fontDialog1.Font;
                    echoArea.Text = "Chosen font too big, not updating UI";
                }
                else
                {
                    foreach (Control c in Controls)
                    {
                        c.Font = fontDialog1.Font;
                    }
                }
            }
        }

        private void fontButton_Click(object sender, EventArgs e)
        {
            fontSelect();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.M))
            {
                fontSelect();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.N))
            {
                textArea.Text = "";
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                if (textArea.Text == "")
                {
                    echoArea.Text = "Cannot save, text area is empty";
                }
                else
                {
                    saveFileDialog1.ShowDialog();
                }
                return true;
            }
            else if (keyData == (Keys.Control | Keys.F))
            {
                openFileDialog1.ShowDialog();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.H))
            {
                helpFunc();
            }
            else if (keyData == (Keys.Alt | Keys.Q))
            {
                Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void helpFunc()
        {
            textArea.Text = 
                "ASMED (Another SMol EDitor) is a volatile text editor, meaning that there are no restrictions against unsaved work or bad files." +
                Environment.NewLine +
                Environment.NewLine +
                "This is the text area, where you can write in."+
                Environment.NewLine +
                "The bar at the bottom is the echo area, where error messages will be printed." +
                Environment.NewLine +
                Environment.NewLine +
                "Keyboard shortcuts" + 
                Environment.NewLine + "====================" + 
                Environment.NewLine + 
                Environment.NewLine + 
                "C means use the Control key, A means use the Alt key" + 
                Environment.NewLine + 
                Environment.NewLine + 
                "<C-n> Clear the text area." + 
                Environment.NewLine + 
                "<C-s> Save the contents of the text area to a file." + 
                Environment.NewLine + 
                "<C-f> Open a file." + 
                Environment.NewLine +
                "<C-m> Change the font." + 
                Environment.NewLine + 
                "<C-h> Display this help." + 
                Environment.NewLine + 
                "<A-q> Exit.";
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            textArea.Text = "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (textArea.Text == "")
            {
                echoArea.Text = "Cannot save, text area is empty";
            }
            else
            {
                saveFileDialog1.ShowDialog();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(saveFileDialog1.FileName, textArea.Text);
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textArea.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            helpFunc();
        }

        private void textArea_TextChanged(object sender, EventArgs e)
        {
            echoArea.Text = "";
        }
    }
}
