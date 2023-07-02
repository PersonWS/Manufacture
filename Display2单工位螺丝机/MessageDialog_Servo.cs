using ScrewMachineManagementSystem.Model;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ScrewMachineManagementSystem
{
    public partial class MessageDialog_Servo : Form
    {
        public MessageDialog_Servo(string msg, int interval, string title)
        {
            InitializeComponent();
            _title = title;
            _interval = interval;
            _msg = msg;
        }




        
        string cbnTitle = "";
        int _interval = 0;
        string _msg = "";
        string _title = "";
        private void cbtOK_Click(object sender, EventArgs e)
        {
           
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void MessageDialog_Load(object sender, EventArgs e)
        {
            
            labelTitle.Text = "    " + _title;
            labelMsg.Text = _msg;


            cbnTitle = cbtOK.Text;
            utility.ShowMessageIsOpened = true;
            if (_interval > 0)
            {
                timer1.Enabled = true;
                cbtOK.Text = cbnTitle + string.Format(" ({0})", _interval - s);
            }
            cbtOK.Focus();
        }

        private void cbtOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Yes;
                Close();
            }
        }
        int s = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(s> _interval)
            {
                DialogResult = DialogResult.Yes;
                Close();
            }
            else
            {
                cbtOK.Text = cbnTitle + string.Format(" ({0})", _interval-s);
                s++;
            }
            
        }

        private void MessageDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            utility.ShowMessageIsOpened=false;
        }

      
        private void buttonServo_Click(object sender, EventArgs e)
        {
            utility.bool_Servo = true;
        }
    }
}