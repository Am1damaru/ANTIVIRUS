using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.DomainObjects.Base;
using Core.DomainObjects.Bases;
using Core.DomainObjects.scan;
using Core.DomainObjects.Scan;

namespace AVui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowNewFolderButton = false;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                ScanSession session = new ScanSession(@FBD.SelectedPath, "User");

                ScanReport report = session.getReport();
                foreach (var s in report.listThread)
                {
                    string[] f = s.Split(' ');
                    listBox1.Items.Add("Расположение файла" + f[0] + "\tИмя вируса:" + f[1]);

                }
                report.SaveResult();
            }
  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1 != null && textBox2 != null && textBox3 != null && textBox4 != null)
            {
                AVBase avBase = new AVBase();
                avBase.AddBase(new ThreadSignature(textBox1.Text, textBox2.Text, Convert.ToUInt64(textBox3.Text), Convert.ToUInt64(textBox4.Text)));

            }


        }
    }
}
