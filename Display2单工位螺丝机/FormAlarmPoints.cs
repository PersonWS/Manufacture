using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class FormAlarmPoints : Form
    {
        public FormAlarmPoints()
        {
            InitializeComponent();
        }

        private void FormAlarmPoints_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
