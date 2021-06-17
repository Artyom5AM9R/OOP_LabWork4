using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        /// Список для хранения начального состояния расчетов
        /// </summary>
        private List<MotionBase> _startingStateOfMotionList = new List<MotionBase>();

        /// <summary>
        /// Дейтсвия при открытии формы
        /// </summary>
        public CoordinateDeterminationForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            openFileDialog.Filter = "Specific files(*.mtn) | *.mtn";
            saveFileDialog.Filter = "Specific files(*.mtn) | *.mtn";
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            FormClosed += Form_FormClosed;

            void Form_FormClosed(object senderForm, FormClosedEventArgs f)
            {
                if (f.CloseReason == CloseReason.UserClosing)
                {
                    FileChangeCheck(senderForm, f);
                }
            }
        }

        /// <summary>
        /// Действие при нажатии кнопки для добавления объектов в список _motionList
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления, вызвавший событие</param>
        /// <param name="e">Данные о событии</param>
        private void AddCalculationButton_Click(object sender, EventArgs e)
        {
            AddCalculationForm form = new AddCalculationForm();
            form.ShowDialog();

            if (form.DialogResult != DialogResult.OK) return;

            _motionList.Add(form.TmpMotion);
            RefreshOfDataGridView(_motionList);
        }

        /// <summary>
        /// Метод, присваивающий источник данных для DataGridView и обновляющий 
        /// его содержание
        /// </summary>
        /// <param name="list">Список для вывода на экран</param>
        private void RefreshOfDataGridView(List<MotionBase> list)
        {
            dataGridView.DataSource = null;
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("Number", "№");
            dataGridView.Columns[0].Width = 30;
            dataGridView.Columns.Add("Type", "Тип движения");
            dataGridView.Columns[1].Width = 105;

            dataGridView.DataSource = new List<MotionBase>(list);

            if (dataGridView.Rows.Count != 0)
            {
                dataGridView.SelectedRows[0].Selected = false;
            }

            dataGridView.RowHeadersVisible = false;
            dataGridView.Columns[2].HeaderText = "Координата";
            dataGridView.Columns[3].HeaderText = "Время";

            foreach (DataGridViewRow line in dataGridView.Rows)
            {
                line.Cells[0].Value = line.Index + 1;

                var service = new ServiceOptions();

                switch (list[line.Index].GetType().Name)
                {
                    case nameof(MotionType.UniformMotion):
                        line.Cells[1].Value = service.GetDescription(
                            MotionType.UniformMotion);
                        break;
                    case nameof(MotionType.AcceleratedMotion):
                        line.Cells[1].Value = service.GetDescription(
                            MotionType.AcceleratedMotion);
                        break;
                    case nameof(MotionType.OscillatoryMotion):
                        line.Cells[1].Value = service.GetDescription(
                            MotionType.OscillatoryMotion);
                        break;
                }

                double fullValue = double.Parse(line.Cells[2].Value.ToString());
                int integerVulue = (int)fullValue;

                if ((fullValue - integerVulue).ToString().Length > 6)
                {
                    line.Cells[2].Style.Format = "0.0000";
                }
            }
        }

        /// <summary>
        /// Действие при нажитии кнопки для закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Метод для проверки начаньного и конечного состояния рабочего файла.
        /// </summary>
        /// <param name="sender">Элемент, во время активности которого 
        /// нужно запускать проверку</param>
        /// <param name="e">Информация о событии, которое инициирует 
        /// элемент</param>
        /// <remarks>
        /// Открывает окно сохранения файла в том случае, если файл закрывается, 
        /// а его содержимое было изменено и не зафиксировано.
        /// </remarks>
        private void FileChangeCheck(object sender, EventArgs e)
        {
            if (_startingStateOfMotionList.SequenceEqual(_motionList))
            {
                return;
            }

            var result = MessageBox.Show("Текущий файл не был сохранен. " +
                "Сохранить файл?", "Предупреждение", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                SaveButton_Click(sender, e);
            }
            else
            {
                _startingStateOfMotionList.Clear();
                _startingStateOfMotionList.AddRange(_motionList.ToArray());
            }
        }

        /// <summary>
        /// Действия при нажатии кнопки для поиска расчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount == 0)
            {
                MessageBox.Show("Поиск не возможен, расчеты отсутсвуют.", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FindCalculationForm form = new FindCalculationForm();

                form.ShowDialog(this);

                var findResultList = new List<MotionBase>();

                if (form.DialogResult != DialogResult.OK) return;

                foreach (var node in _motionList)
                {
                    switch (form.Time)
                    {
                        case 0:
                            if (Math.Round(node.Coordinate, 4) == form.Coordinate)
                            {
                                findResultList.Add(node);
                            }
                            break;
                        default:
                            if (node.Time == form.Time)
                            {
                                findResultList.Add(node);
                            }
                            else if (Math.Round(node.Coordinate, 4) == form.Coordinate &&
                                     node.Time == form.Time)
                            {
                                findResultList.Add(node);
                            }
                            break;
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

        /// <summary>
        /// Действия при нажатии кнопки для открытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton_Click(object sender, EventArgs e)
        {
            FileChangeCheck(sender, e);

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                var serializer = new Serializer();

                _motionList = serializer.OpenFile(openFileDialog.FileName, _motionList);
                _startingStateOfMotionList.Clear();
                _startingStateOfMotionList.AddRange(_motionList.ToArray());
                RefreshOfDataGridView(_motionList);
            }
            catch
            {
                MessageBox.Show("Файл поврежден, не возможно открыть.", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Действия при нажатии кнопки для сброса результатов поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscardButton_Click(object sender, EventArgs e)
        {
            RefreshOfDataGridView(_motionList);
        }

        /// <summary>
        /// Действия при нажатии кнопки для сохранения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                var serializer = new Serializer();
                serializer.SaveFile(saveFileDialog.FileName, _motionList);
                _startingStateOfMotionList.Clear();
                _startingStateOfMotionList.AddRange(_motionList.ToArray());
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку для удаления расчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveCalculationButton_Click(object sender, EventArgs e)
        {
            var listForDelete = new List<MotionBase>();

            if (dataGridView.RowCount == 0)
            {
                MessageBox.Show("Удаление не возможно, расчеты отсутсвуют.", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Удаление не возможно, нет выделенных строк.", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var _service = new ServiceOptions();
                
                foreach (DataGridViewRow line in dataGridView.SelectedRows)
                {
                    string type = _service.GetEnumElementName(line.Cells[1].Value.ToString());

                    foreach (MotionBase motion in _motionList)
                    {
                        if (type == motion.GetType().ToString() &&
                            line.Cells[2].Value.ToString() == motion.Coordinate.ToString() &&
                            line.Cells[3].Value.ToString() == motion.Time.ToString())
                        {
                            listForDelete.Add(motion);
                        }
                    }
                }

                foreach (var node in listForDelete)
                {
                    _motionList.Remove(node);
                }

                RefreshOfDataGridView(_motionList);
            }
        }
    }
}
