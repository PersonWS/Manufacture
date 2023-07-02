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
    public partial class FormMotionController : Form
    {
        public FormMotionController()
        {
            InitializeComponent();
        }

        private void FormMotionController_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            foreach (var v in utility.modbusObjects)
            {
                comboBox1.Items.Add(v.name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deviceid = utility.modbusObjects[comboBox1.SelectedIndex].orderid;
            List<Model.IOInfo> list = Controller.CONT_IOInfo.LoadList(" where deviceid="+deviceid);
            dataGridView1.DataSource = list;
        }
    }
}
