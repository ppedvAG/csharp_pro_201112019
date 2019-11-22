using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFBank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var client = new ServiceReference1.BLZServicePortTypeClient("BLZServiceSOAP12port_http");
            var bank = client.getBank("51050015");
            MessageBox.Show(bank.bezeichnung);
        }
    }
}
