namespace NetworkPlanning
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PathSelectButton = new System.Windows.Forms.Button();
            this.Gant = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DataSeeButton = new System.Windows.Forms.Button();
            this.DirectiveResult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Gant)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PathSelectButton
            // 
            this.PathSelectButton.Location = new System.Drawing.Point(3, 3);
            this.PathSelectButton.Name = "PathSelectButton";
            this.PathSelectButton.Size = new System.Drawing.Size(88, 23);
            this.PathSelectButton.TabIndex = 0;
            this.PathSelectButton.Text = "Выбор пути";
            this.PathSelectButton.UseVisualStyleBackColor = true;
            this.PathSelectButton.Click += new System.EventHandler(this.PathSelectButton_Click);
            // 
            // Gant
            // 
            this.Gant.AllowUserToAddRows = false;
            this.Gant.AllowUserToDeleteRows = false;
            this.Gant.AllowUserToResizeColumns = false;
            this.Gant.AllowUserToResizeRows = false;
            this.Gant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gant.Location = new System.Drawing.Point(0, 33);
            this.Gant.MultiSelect = false;
            this.Gant.Name = "Gant";
            this.Gant.ReadOnly = true;
            this.Gant.RowHeadersVisible = false;
            this.Gant.RowHeadersWidth = 10;
            this.Gant.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Gant.Size = new System.Drawing.Size(1349, 417);
            this.Gant.TabIndex = 2;
            this.Gant.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.PathSelectButton);
            this.flowLayoutPanel1.Controls.Add(this.DataSeeButton);
            this.flowLayoutPanel1.Controls.Add(this.DirectiveResult);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1349, 33);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // DataSeeButton
            // 
            this.DataSeeButton.Location = new System.Drawing.Point(97, 3);
            this.DataSeeButton.Name = "DataSeeButton";
            this.DataSeeButton.Size = new System.Drawing.Size(110, 23);
            this.DataSeeButton.TabIndex = 1;
            this.DataSeeButton.Text = "Просмотр данных";
            this.DataSeeButton.UseVisualStyleBackColor = true;
            this.DataSeeButton.Visible = false;
            this.DataSeeButton.Click += new System.EventHandler(this.DataSeeButton_Click);
            // 
            // DirectiveResult
            // 
            this.DirectiveResult.Location = new System.Drawing.Point(213, 4);
            this.DirectiveResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.DirectiveResult.Name = "DirectiveResult";
            this.DirectiveResult.ReadOnly = true;
            this.DirectiveResult.Size = new System.Drawing.Size(249, 20);
            this.DirectiveResult.TabIndex = 2;
            this.DirectiveResult.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 450);
            this.Controls.Add(this.Gant);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Сетевое планирование";
            ((System.ComponentModel.ISupportInitialize)(this.Gant)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PathSelectButton;
        private System.Windows.Forms.DataGridView Gant;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button DataSeeButton;
        private System.Windows.Forms.TextBox DirectiveResult;
    }
}

