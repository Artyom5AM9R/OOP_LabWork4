namespace View
{
    partial class AddCalculationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.choiceLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(216, 300);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(80, 30);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(116, 300);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(80, 30);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Назад";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(136, 13);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(160, 24);
            this.comboBox.TabIndex = 2;
            // 
            // choiceLabel
            // 
            this.choiceLabel.AutoSize = true;
            this.choiceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.choiceLabel.Location = new System.Drawing.Point(9, 16);
            this.choiceLabel.Name = "choiceLabel";
            this.choiceLabel.Size = new System.Drawing.Size(118, 17);
            this.choiceLabel.TabIndex = 3;
            this.choiceLabel.Text = "Тип движения:";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(9, 28);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(69, 17);
            this.timeLabel.TabIndex = 4;
            this.timeLabel.Text = "Время, с:";
            // 
            // timeTextBox
            // 
            this.timeTextBox.Location = new System.Drawing.Point(224, 25);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(60, 22);
            this.timeTextBox.TabIndex = 5;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.timeLabel);
            this.groupBox.Controls.Add(this.timeTextBox);
            this.groupBox.Location = new System.Drawing.Point(12, 54);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(310, 240);
            this.groupBox.TabIndex = 7;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Параметры движения";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkRate = 0;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // AddCalculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 337);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.choiceLabel);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Name = "AddCalculationForm";
            this.Text = "Новый расчет";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label choiceLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}