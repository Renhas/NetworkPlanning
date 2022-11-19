using CompanyNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkPlanning
{
    public partial class DataOutput : Form
    {
        Company company;
        public DataOutput(Company company)
        {
            InitializeComponent();
            this.company = company;
        }

        private void DataOutput_Load(object sender, EventArgs e)
        {
            OutputBox.Text = company.ToString();
        }
    }
}
