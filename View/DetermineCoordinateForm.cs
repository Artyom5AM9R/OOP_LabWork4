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
        /// Дейтсвия при открытии формы
        /// </summary>
        public CoordinateDeterminationForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //TODO: масштабирование формы +++
            MaximizeBox = false;
            openFileDialog.Filter = "Specific files(*.mtn) | *.mtn";
            saveFileDialog.Filter = "Specific files(*.mtn) | *.mtn";
            //TODO: сделать выделение цельным для всей строки, а не только для ячейки +++
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Действие при нажатии кнопки для добавления объектов в список _motionList
        /// </summary>
        /// <param name="sender">Ссылка на элемент управления, вызвавший событие</param>
        /// <param name="e">Данные о событии</param>
        private void AddCalculationButton_Click(object sender, EventArgs e)
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
                //TODO: добавить информацию о типе движения +++
                switch (list[line.Index].GetType().Name)
                {
                    case nameof(MotionType.UniformMotion):
                        line.Cells[1].Value = service.GetDescription(MotionType.
                            UniformMotion);
                        break;
                    case nameof(MotionType.AcceleratedMotion):
                        line.Cells[1].Value = service.GetDescription(MotionType.
                            AcceleratedMotion);
                        break;
                    case nameof(MotionType.OscillatoryMotion):
                        line.Cells[1].Value = service.GetDescription(MotionType.
                            OscillatoryMotion);
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
                form.FormClosed += form_FormClosed;
                form.Show();

                void form_FormClosed(object senderForm, FormClosedEventArgs f)
                {
                    var findResultList = new List<MotionBase>();

                    if (form.DialogResult == DialogResult.OK)
                    {
                        foreach (MotionBase node in _motionList)
                        {
                            //TODO: сделать поиск по каждому из параметров в отдельности +++
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
            }
        }

        /// <summary>
        /// Действия при нажатии кнопки для открытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            var reader = new XmlSerializer(typeof(List<MotionBase>));

            using (var file = System.IO.File.OpenRead(openFileDialog.FileName))
            {
                _motionList = new List<MotionBase>((IEnumerable<MotionBase>)reader.
                    Deserialize(file));
            }

            RefreshOfDataGridView(_motionList);
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
                //TODO: убрать форму для удаления, сделать удаление через выделение строк в dataGridView +++
                foreach (DataGridViewRow line in dataGridView.Rows)
                {
                    if (line.Selected)
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
                }

                foreach (MotionBase node in listForDelete)
                {
                    _motionList.Remove(node);
                }

                RefreshOfDataGridView(_motionList);
            }
        }
    }
}
