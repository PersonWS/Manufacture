using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace ScrewMachineManagementSystem
{
    public class ListViewCombox
    {
        ListView _listView;
        ComboBox _combox;
        int _showColumn = 0;
        ListViewSubItem _selectedSubItem;

        /// <summary>
        /// 列表combox
        /// </summary>
        /// <param name="listView">listView控件</param>
        /// <param name="combox">要呈现的combox控件</param>
        /// <param name="showColumn">要在哪一列显示combox(从0开始)</param>
        public ListViewCombox(ListView listView, ComboBox combox, int showColumn)
        {
            _listView = listView;
            _combox = combox;
            _showColumn = showColumn;
            BindComboxEvent();
        }

        /// <summary>
        /// 定位combox
        /// </summary>
        /// <param name="x">点击的x坐标</param>
        /// <param name="y">点击的y坐标</param>
        public void Location(int x, int y)
        {
            ListViewItem item = _listView.GetItemAt(x, y);
            if (item != null)
            {
                _selectedSubItem = item.GetSubItemAt(x, y);
                if (_selectedSubItem != null)
                {
                    int clickColumn = item.SubItems.IndexOf(_selectedSubItem);
                    if (clickColumn == 0)
                    {
                        _combox.Visible = false;
                    }
                    else if (clickColumn == _showColumn)
                    {
                        int padding = 2;
                        Rectangle rect = _selectedSubItem.Bounds;
                        rect.X += _listView.Left + padding;
                        rect.Y += _listView.Top + padding;
                        rect.Width = _listView.Columns[clickColumn].Width + padding;
                        if (_combox != null)
                        {
                            _combox.Bounds = rect;
                            _combox.Text = _selectedSubItem.Text;
                            _combox.Visible = true;
                            _combox.BringToFront();
                            _combox.Focus();
                        }
                    }
                }
            }
        }

        private void BindComboxEvent()
        {
            if (_combox != null)
            {
                _combox.SelectedIndexChanged += combox_SelectedIndexChanged;
                _combox.Leave += combox_Leave;
            }
        }

        private void combox_Leave(object sender, EventArgs e)
        {
            if (_selectedSubItem != null)
            {
                _selectedSubItem.Text = _combox.Text;
                _combox.Visible = false;
            }
        }

        private void combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedSubItem != null)
            {
                _selectedSubItem.Text = _combox.Text;
                _combox.Visible = false;
            }
        }
    }
}
