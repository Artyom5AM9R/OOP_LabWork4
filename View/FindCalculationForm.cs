using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace View
{
    /// <summary>
    /// Класс для описания функционала формы "Поиск расчета"
    /// </summary>
    public partial class FindCalculationForm : Form
    {
        /// <summary>
        /// Координата тела для поиска расчета
        /// </summary>
        public double Coordinate;

        /// <summary>
        /// Время движения тела для поиска расчета
        /// </summary>
        public double Time;

        /// <summary>
        ///Действие при открытии формы
        /// </summary>
        public FindCalculationForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
                coordinateTextBox.Text = coordinateTextBox.Text.Replace(".", ",");
                timeTextBox.Text = timeTextBox.Text.Replace(".", ",");

                if (double.TryParse(coordinateTextBox.Text, out Coordinate) &&
                    double.TryParse(timeTextBox.Text, out Time) && Time > 0)
                {
                    CoordinateDeterminationForm.Flag = true;
                    Close();
                }
                else if (double.TryParse(timeTextBox.Text, out Time) && Time <= 0)
                {
                    throw new Exception("Время должно быть больше нуля.");
                }
                else if (string.IsNullOrEmpty(coordinateTextBox.Text) || string.IsNullOrEmpty(timeTextBox.Text))
                {
                    throw new Exception("Параметры поиска не могут быть пустыми.");
                }
                else
                {
                    throw new Exception("Параметры поиска должны быть числами.");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Действие при нажатии кнопки "Назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
