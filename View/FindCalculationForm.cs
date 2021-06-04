using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public double Coordinate { get; private set; }

        /// <summary>
        /// Время движения тела для поиска расчета
        /// </summary>
        public double Time { get; private set; }

        /// <summary>
        ///Действие при открытии формы
        /// </summary>
        public FindCalculationForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        /// <summary>
        /// Действия при нажатии кнопки "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                Time = 0;
                Regex correctValueRegex = new Regex(ServiceOptions.TmpParametersValue);

                if (string.IsNullOrEmpty(coordinateTextBox.Text) && 
                    string.IsNullOrEmpty(timeTextBox.Text))
                {
                    throw new Exception("Параметры поиска не могут быть пустыми.");
                }
                else if ((!string.IsNullOrEmpty(coordinateTextBox.Text) && 
                    !correctValueRegex.IsMatch(coordinateTextBox.Text)) || 
                    (!string.IsNullOrEmpty(timeTextBox.Text) && 
                    !correctValueRegex.IsMatch(timeTextBox.Text)))
                {
                    throw new Exception("Параметры поиска должны быть числами.");
                }

                if (correctValueRegex.IsMatch(coordinateTextBox.Text))
                {
                    coordinateTextBox.Text = coordinateTextBox.Text.Replace(".", ",");
                    Coordinate = double.Parse(coordinateTextBox.Text);
                }
                
                if (correctValueRegex.IsMatch(timeTextBox.Text))
                {
                    timeTextBox.Text = timeTextBox.Text.Replace(".", ",");

                    if (double.Parse(timeTextBox.Text) > 0)
                    {
                        Time = double.Parse(timeTextBox.Text);
                    }
                    else
                    {
                        throw new Exception("Время должно быть больше нуля.");
                    }
                }

                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Действие при нажатии кнопки "Назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
