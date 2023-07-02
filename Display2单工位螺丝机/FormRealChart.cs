using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScrewMachineManagementSystem
{
    public partial class FormRealChart : Form
    {
        public FormRealChart()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            label1.Text = "";
            ushort passageway = 0;
            if (index != 0)
            {
                double a = Math.Pow(2, index - 1);
                passageway = (ushort)a;
                label1.Text = comboBox1.Text;
            }
            SerialCommunication.sendConmmand(0,SerialCommunication.WriteType.Write06, 61301, passageway);  //停止紧
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;
            ushort passageway = 0;
            label2.Text = "";
            if (index != 0)
            {
                double a = Math.Pow(2, index - 1);
                passageway = (ushort)a;
                label2.Text = comboBox2.Text;
            }
            SerialCommunication.sendConmmand(0, SerialCommunication.WriteType.Write06,61302, passageway);  //停止紧
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var s in chart1.Series)
            {
                s.Points.Clear();
            }

            //控制器采样波形数组大小,"与控制器的缓存能力有关;一般是1200个16bit数据。
            //如果是只选择一通道，哪每通道数量是1300; 如果选择二通道每通道采样缓冲区是600个点数; "
            ushort[] buffer = SerialCommunication.master[0].ReadHoldingRegisters(1, 61311, 1);

            buffer = SerialCommunication.master[0].ReadHoldingRegisters(1, 60635, 1);

            int pointsNum = (int)(buffer[0]);//点数
            int yushu = pointsNum % 100;
            int zhengshu = pointsNum / 100;
            ushort[] buffers = new ushort[pointsNum];


            int i = 0;
            for (i = 0; i < zhengshu; i++)
            {
                buffer = SerialCommunication.master[0].ReadHoldingRegisters(1, (ushort)(10000 + i * 100), 100);
                Array.Copy(buffer, 0, buffers, i * 100, buffer.Length);
            }
            if (yushu > 0)
            {
                buffer = SerialCommunication.master[0].ReadHoldingRegisters(1, (ushort)(10000 + i * 100), (ushort)yushu);
                Array.Copy(buffer, 0, buffers, i * 100, buffer.Length);
            }
            int x = 0;
            // 设置曲线的样式
            Series series = chart1.Series[0];
            // 画样条曲线（Spline）
            series.ChartType = SeriesChartType.Line;
            // 线宽2个像素
            series.BorderWidth = 2;
            // 线的颜色：红色
            series.Color = System.Drawing.Color.Red;
            // 图示上的文字
            foreach (float v in buffer)
            {
                float a = v;
                if (a > 32767)
                    a = a - 65536;
                series.Points.AddXY(x, a);
                x++;
            }
        }

        /// <summary>
        /// 初始化图表
        /// </summary>
        private void InitChart()
        {
            //定义图表区域
            this.chart1.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.chart1.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器
            this.chart1.Series.Clear();
            Series series1 = new Series("S1");
            series1.ChartArea = "C1";
            this.chart1.Series.Add(series1);
            //设置图表显示样式
            //this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            //this.chart1.ChartAreas[0].AxisY.Maximum = 100;
            this.chart1.ChartAreas[0].AxisX.Interval = 1;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //设置标题
            //this.chart1.Titles.Clear();
            //this.chart1.Titles.Add("S01");
            //this.chart1.Titles[0].Text = "XXX显示";
            //this.chart1.Titles[0].ForeColor = Color.RoyalBlue;
            //this.chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            //设置图表显示样式
            this.chart1.Series[0].Color = Color.Red;

            //this.chart1.Titles[0].Text = string.Format("XXX {0} 显示", "ssss");
            this.chart1.Series[0].ChartType = SeriesChartType.Line;

            this.chart1.Series[0].Points.Clear();
        }

        private void FormRealChart_Load(object sender, EventArgs e)
        {
            InitChart();
            timer1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox1.Checked;
        }

        private void comboBox1_EnabledChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox2_EnabledChanged(object sender, EventArgs e)
        {

            comboBox2.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ushort[] buffer = SerialCommunication.master[0].ReadHoldingRegisters(1, 60635, 1);
            label3.Text = buffer.Length.ToString();
        }
    }
}
