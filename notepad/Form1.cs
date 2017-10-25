using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notepad
{
    public partial class NotepadForm : Form
    {
        string tmpPath;
        string currentPath;

        private void checkCurrentPath()
        {
            if (this.currentPath != null)
            {
                this.currentPath = System.IO.Path.GetFullPath(this.Text);
            }
        }

        public NotepadForm()
        {
            InitializeComponent();
            this.tmpPath = null;
            this.currentPath = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this.Close();
            
            // TODO:
            // notify user to save file if the content has been changed.
            //MessageBox.Show(MainRichTextBox.Text);
            //MessageBox.Show(System.IO.File.ReadAllText(this.currentPath));
            
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Text = "";
            MainRichTextBox.Clear();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Undo();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = true;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
        }

        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MainRichTextBox.Text.Length > 0)
            {
                undoToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
            }
            else
            {
                undoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Text += DateTime.Now;
        }

        private void strikeThroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Strikeout);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Regular);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                MainRichTextBox.LoadFile(fd.FileName, RichTextBoxStreamType.PlainText);
                this.Text = fd.FileName;
            }
            checkCurrentPath();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkCurrentPath(); 
            if (this.currentPath == this.tmpPath)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                MainRichTextBox.SaveFile(this.currentPath, RichTextBoxStreamType.PlainText);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Text Doc(*.txt)|*.txt|All Files(*.*)|";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                MainRichTextBox.SaveFile(fd.FileName, RichTextBoxStreamType.PlainText);
                this.Text = fd.FileName;
                checkCurrentPath();
            }
        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = MainRichTextBox.SelectionFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                MainRichTextBox.SelectionFont = fd.Font;
            }
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog fd = new ColorDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                MainRichTextBox.BackColor = fd.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
