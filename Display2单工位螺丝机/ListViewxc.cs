using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewMachineManagementSystem
{
    public partial class ListViewxc : Component
    {
        public ListViewxc()
        {
            InitializeComponent();
        }

        public ListViewxc(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
