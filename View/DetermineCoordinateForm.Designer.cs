namespace View
{
    partial class CoordinateDeterminationForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.exitButton = new System.Windows.Forms.Button();
            this.removeCalculationButton = new System.Windows.Forms.Button();
            this.addCalculationButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.findButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.discardButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 263);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры расчетов";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 70;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(402, 235);
            this.dataGridView1.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(439, 285);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(94, 28);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Выход";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // removeCalculationButton
            // 
            this.removeCalculationButton.Location = new System.Drawing.Point(6, 56);
            this.removeCalculationButton.Name = "removeCalculationButton";
            this.removeCalculationButton.Size = new System.Drawing.Size(94, 28);
            this.removeCalculationButton.TabIndex = 2;
            this.removeCalculationButton.Text = "Удалить";
            this.removeCalculationButton.UseVisualStyleBackColor = true;
            this.removeCalculationButton.Click += new System.EventHandler(this.removeCalculationButton_Click);
            // 
            // addCalculationButton
            // 
            this.addCalculationButton.Location = new System.Drawing.Point(6, 21);
            this.addCalculationButton.Name = "addCalculationButton";
            this.addCalculationButton.Size = new System.Drawing.Size(94, 28);
            this.addCalculationButton.TabIndex = 1;
            this.addCalculationButton.Text = "Добавить";
            this.addCalculationButton.UseVisualStyleBackColor = true;
            this.addCalculationButton.Click += new System.EventHandler(this.addCalculationButton_Click);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(6, 21);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(94, 28);
            this.openButton.TabIndex = 4;
            this.openButton.Text = "Загрузить";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(6, 56);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(94, 28);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(6, 91);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(94, 28);
            this.findButton.TabIndex = 6;
            this.findButton.Text = "Поиск";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.addCalculationButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.saveButton);
            this.groupBox2.Controls.Add(this.openButton);
            this.groupBox2.Location = new System.Drawing.Point(433, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 93);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Файл";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.discardButton);
            this.groupBox3.Controls.Add(this.addCalculationButton);
            this.groupBox3.Controls.Add(this.removeCalculationButton);
            this.groupBox3.Controls.Add(this.findButton);
            this.groupBox3.Location = new System.Drawing.Point(433, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 163);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Расчет";
            // 
            // discardButton
            // 
            this.discardButton.Location = new System.Drawing.Point(6, 125);
            this.discardButton.Name = "discardButton";
            this.discardButton.Size = new System.Drawing.Size(94, 28);
            this.discardButton.TabIndex = 8;
            this.discardButton.Text = "Сбросить";
            this.discardButton.UseVisualStyleBackColor = true;
            this.discardButton.Click += new System.EventHandler(this.discardButton_Click);
            // 
            // CoordinateDeterminationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 321);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "CoordinateDeterminationForm";
            this.Text = "Расчет координаты движения тела";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button removeCalculationButton;
        private System.Windows.Forms.Button addCalculationButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button discardButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

