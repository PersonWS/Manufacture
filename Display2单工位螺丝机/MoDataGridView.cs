using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewMachineManagementSystem
{
    public partial class MoDataGridView : DataGridView
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                this.OnKeyPress(new KeyPressEventArgs('f'));
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                this.OnKeyPress(new KeyPressEventArgs('e'));
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                this.OnKeyPress(new KeyPressEventArgs('d'));
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
    }

}
