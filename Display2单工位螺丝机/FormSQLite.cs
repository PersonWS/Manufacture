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
    public partial class FormSQLite : Form
    {
        public FormSQLite()
        {
            InitializeComponent();
        }

        private void buttonTask_Click(object sender, EventArgs e)
        {
            List< Model.screwmachinetasklist> l=new List<Model.screwmachinetasklist>();
            
            for (int i=1;i<36;i++)
            {
                for(int j=0;j<16;j++)
                {
                    Model.screwmachinetasklist s = new Model.screwmachinetasklist();
                    s.Id = i;
                    s.TaskId = j;
                    s.Dvalue = 0;
                    l.Add(s);
                }
                
            }
            Controller.CONT_ScrewMachineTaskList.Insert(l);
        }

        private void buttonStep_Click(object sender, EventArgs e)
        {
            List<Model.screwmachineSteplist> l = new List<Model.screwmachineSteplist>();

            for (int i = 1; i < 81; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Model.screwmachineSteplist s = new Model.screwmachineSteplist();
                    s.Id = i;
                    s.TaskId = j;
                    s.Dvalue = 0;
                    l.Add(s);
                }

            }
            Controller.CONT_ScrewMachineStepList.Insert(l);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SerialCommunication.setSerial();
            
            //ushort startaddress=utility.screwmachinetaskdatas[0].startaddress;
            //ushort[] registerBuffer = SerialCommunication.master[0].ReadHoldingRegisters((byte)utility.modbusObjects[0].slaveid, startaddress, 1);
            //    utility.modbusObjects[0].serialPort.Close();
        }
    }
}
