using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyNamespace;

namespace NetworkPlanning
{
    public partial class MainForm : Form
    {
        Company company;
        public MainForm()
        {
            InitializeComponent();
        }

        private void PathSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "XML-файлы (*.xml) | *.xml",
                Title = "Открыть файл"
            };
            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                company = XMLReader.Read(dialog.FileName);
                ResultTextBox.Text = company.ToString();
            }
        }
    }
}
