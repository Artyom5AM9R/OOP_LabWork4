using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    /// <summary>
    /// Класс для описания функционала формы "Удалить"
    /// </summary>
    public partial class DeleteCalculationForm : Form
    {
        /// <summary>
        /// Номер удаляемой строки из списка существующих расчетов
        /// </summary>
        public int DeleteNumber;

        /// <summary>
        /// Действие при открытии формы
        /// </summary>
        public DeleteCalculationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Действия при нажатии кнопки "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(deleteTextBox.Text) && int.Parse(deleteTextBox.Text) > 0)
                {
                    DeleteNumber = int.Parse(deleteTextBox.Text);
                    CoordinateDeterminationForm.Flag = true;
                    Close();
                }
                else
                {
                    throw new Exception("Номер строки должен принимать целое положительное число больше нуля.");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Действия при нажатии кнопки "Назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            CoordinateDeterminationForm.Flag = false;
            Close();
        }
    }
}
