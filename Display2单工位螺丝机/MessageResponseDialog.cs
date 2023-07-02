using System;
using System.Drawing;
using System.Windows.Forms;
namespace ScrewMachineManagementSystem
{
    public partial class MessageResponseDialog : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="defaultBUtton">1默认OK，0默认esc</param>
        public MessageResponseDialog(string msg, string title, int defaultBUtton = 0)
        {
            InitializeComponent();
            _title = title;

            _msg = msg;
            _defaultBUtton = defaultBUtton;
        }


        string _msg = "";
        string _title = "";
        int _defaultBUtton = 0;



        private void cbtOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void MessageDialog_Load(object sender, EventArgs e)
        {

            labelTitle.Text = "    " + _title;
            labelMsg.Text = _msg;
            if (_defaultBUtton == 1)
                cbtOK.Focus();
            else
                cbtNo.Focus();
        }

        private void cbtOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.Yes;
                Close();
            }
        }

        private void cbtNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void cbtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.No;
                Close();
            }
        }
    }
}