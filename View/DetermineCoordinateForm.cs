using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Model;

namespace View
{
    /// <summary>
    /// Класс для описания функционала формы "Расчет координаты движения тела"
    /// </summary>
    public partial class CoordinateDeterminationForm : Form
    {
        /// <summary>
        /// Список для хранения информации о расчетах
        /// </summary>
        private List<MotionBase> _motionList = new List<MotionBase>();

        /// <summary>
        /// Дейтсвия при открытии формы
        /// </summary>
        public CoordinateDeterminationForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            openFileDialog.Filter = "Specific files(*.mtn) | *.mtn";
            saveFileDialog.Filter = "Specific files(*.mtn) | *.mtn";
        }

        /// <summary>
        /// Действие при нажатии кнопки для добавления объектов в список _motionList
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления, вызвавший событие</param>
        /// <param name="e">Данные о событии</param>
        private void addCalculationButton_Click(object sender, EventArgs e)
        {
            AddCalculationForm form = new AddCalculationForm();
            form.FormClosed += form_FormClosed;
            form.Show();
                        
            void form_FormClosed(object senderForm, FormClosedEventArgs f)
            {
                if (form.DialogResult == DialogResult.OK)
                {
                    _motionList.Add(form.TmpMotion);
                    RefreshOfDataGridView(_motionList);
                }
            }
        }

        /// <summary>
        /// Метод, присваивающий источник данных для DataGridView и обновляющий его содержание
        /// </summary>
        /// <param name="list">Список для вывода на экран</param>
        private void RefreshOfDataGridView(List<MotionBase> list)
        {
            dataGridView.DataSource = null;
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("Number", "№");
            dataGridView.Columns[0].Width = 30;
            dataGridView.DataSource = new List<MotionBase>(list);
            dataGridView.RowHeadersVisible = false;
            dataGridView.Columns[1].HeaderText = "Координата";
            dataGridView.Columns[2].HeaderText = "Время";
            int counter = 1;

            foreach (DataGridViewRow line in dataGridView.Rows)
            {
                line.Cells[0].Value = counter;
                counter++;

                double fullValue = double.Parse(line.Cells[1].Value.ToString());
                int integerVulue = (int)fullValue;

                if ((fullValue - integerVulue).ToString().Length > 6)
                {
                    line.Cells[1].Style.Format = "0.0000";
                }
            }
        }

        /// <summary>
        /// Действие при нажитии кнопки для закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Действия при нажатии кнопки для поиска расчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount == 0)
            {
                MessageBox.Show("Поиск не возможен, расчеты отсутсвуют.", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FindCalculationForm form = new FindCalculationForm();
                form.FormClosed += form_FormClosed;
                form.Show();

                void form_FormClosed(object senderForm, FormClosedEventArgs f)
                {
                    var findResultList = new List<MotionBase>();

                    if (form.DialogResult == DialogResult.OK)
                    {
                        foreach (MotionBase node in _motionList)
                        {
                            if (Math.Round(node.Coordinate, 4) == form.Coordinate &&
                                node.Time == form.Time)
                            {
                                findResultList.Add(node);
                            }
                        }

                        if (findResultList.Count != 0)
                        {
                            RefreshOfDataGridView(findResultList);
                        }
                        else
                        {
                            MessageBox.Show("Расчет с указанными параметрами отсутствует. " +
                                "Уточните параметры поиска.", "Уведомление");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Действия при нажатии кнопки для открытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            var reader = new XmlSerializer(typeof(List<MotionBase>));

            using (var file = System.IO.File.OpenRead(openFileDialog.FileName))
            {
                _motionList = new List<MotionBase>((IEnumerable<MotionBase>)reader.Deserialize(file));
            }

            RefreshOfDataGridView(_motionList);
        }

        /// <summary>
        /// Действия при нажатии кнопки для сброса результатов поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void discardButton_Click(object sender, EventArgs e)
        {
            RefreshOfDataGridView(_motionList);
        }

        /// <summary>
        /// Действия при нажатии кнопки для сохранения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            var writer = new XmlSerializer(typeof(List<MotionBase>));

            using (var file = System.IO.File.Create(saveFileDialog.FileName))
            {
                writer.Serialize(file, _motionList);
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку для удаления расчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeCalculationButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount == 0)
            {
                MessageBox.Show("Удаление не возможно, расчеты отсутсвуют.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Обновление dataGridView перед удалением на тот случай, если отображались
                //только результаты поиска
                //
                discardButton_Click(sender, e);

                var form = new DeleteCalculationForm();
                form.FormClosed += form_FormClosed;
                form.Show();

                void form_FormClosed(object senderForm, FormClosedEventArgs f)
                {
                    if (form.DialogResult == DialogResult.OK && form.DeleteNumber != 0)
                    {
                        if (_motionList.Count >= form.DeleteNumber && _motionList.Count != 0)
                        {
                            _motionList.RemoveAt(form.DeleteNumber - 1);
                            RefreshOfDataGridView(_motionList);
                        }
                        else
                        {
                            MessageBox.Show("Удаление невозможно. Расчет с указанным номером отсутствует.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
