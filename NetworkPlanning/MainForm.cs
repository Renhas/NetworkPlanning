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
using Graph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

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
                Calculate();
                DataSeeButton.Visible = true;
                DirectiveResult.Visible = true;
                int directive = company.DirectiveCalculate();
                if (directive == 0) DirectiveResult.Text = "Директивные сроки соблюдены";
                else DirectiveResult.Text = $"Директивные сроки нарушены на {company.DirectiveCalculate()} тактов";

            }
            else 
            {
                DirectiveResult.Visible = false;
                DataSeeButton.Visible = false;
            }
        }

        private void Calculate() 
        {
            if (company == null) return;
            GantInit(Algorythm.Run(company));
            
        }

        private void GantInit(int tacts)
        {
            GantColumnsInit(tacts);
            GantRowsInit();
            Gant.Refresh();
            Gant.Visible = true;
        }

        private void GantColumnsInit(int tacts) 
        {
            Gant.Columns.Clear();
            Gant.Columns.Add(
                new DataGridViewColumn()
                {
                    HeaderText = "Name",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                    Frozen = true,
                    DisplayIndex = 0,
                    Name = "Name",
                    ValueType = typeof(string),
                    CellTemplate = new DataGridViewTextBoxCell()
                });
            for (int i = 1; i < tacts + 1; i++)
            {
                Gant.Columns.Add(new DataGridViewColumn()
                {
                    HeaderText = i.ToString(),
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                    Width = 20,
                    Frozen = false,
                    DisplayIndex = i,
                    Name = $"Tact{i}",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 0.1f

                }
                );
            }
            Gant.ColumnCount = tacts + 1;
        }
        private void GantRowsInit() 
        {
            if (company == null) return;
            if (company.ProductsCount == 0) return;

            Gant.Rows.Clear();
            for (int i = 0; i < company.ProductsCount; i++) 
            {
                foreach (var node in company.GetProduct(i).ToSortedList())
                {
                    Gant.Rows.Add(
                        new DataGridViewRow()
                        {
                            Height = 20
                        });
                    var row = Gant.Rows[Gant.Rows.Count - 1];
                    row.Cells[0].Value = $"{company.GetProduct(i).ProductName}{node.Id}";
                    GantRowsPaint(row, node);
                }
            }
            Gant.RowCount = Gant.Rows.Count;
        }
        private void GantRowsPaint(DataGridViewRow row, GraphNode node) 
        {
            for (int i = 1; i < Gant.ColumnCount; i++) 
            {
                Color color;
                if (i < node.StartTime || i > node.EndTime) color = Color.White;
                else color = Color.OrangeRed;
                row.Cells[i].Style.BackColor = color;
            }
        }

        private void DataSeeButton_Click(object sender, EventArgs e)
        {
            new DataOutput(company).ShowDialog();
        }
    }
}
