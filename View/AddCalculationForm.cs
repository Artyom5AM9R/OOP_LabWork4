using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace View
{
    /// <summary>
    /// Класс для описания функционала формы "Новый расчет"
    /// </summary>
    public partial class AddCalculationForm : Form
    {
        /// <summary>
        /// Экземляр класса MotionBase, выступающий в качестве шаблона при создании 
        /// объектов данного класса
        /// </summary>
        public MotionBase TmpMotion { get; private set; }

        /// <summary>
        /// Список полей объекта класса MotionBase
        /// </summary>
        private PropertyInfo[] _fields;

        /// <summary>
        /// Экземляр класса ServiceOptions для доступа к служебным полям и методам
        /// </summary>
        private ServiceOptions _service = new ServiceOptions();

        /// <summary>
        /// Регулярное выражение для отбора правильных значений параметров движения
        /// </summary>
        private Regex _correctValueRegex = new Regex(ServiceOptions.TmpParametersValue);

        /// <summary>
        /// Регулярное выражение для отбора правильных значений параметра StartingPosition 
        /// из класса OscillatoryMotion
        /// </summary>
        private Regex _startingPositionRegex = new Regex(ServiceOptions.TmpStartingPositionValue);

        /// <summary>
        /// Действия, выполняемые при открытии формы
        /// </summary>
        public AddCalculationForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            comboBox.SelectedIndexChanged += СomboBox_SelectedIndexChanged;
            comboBox.Items.Add(_service.GetDescription(MotionType.UniformMotion));
            comboBox.Items.Add(_service.GetDescription(MotionType.AcceleratedMotion));
            comboBox.Items.Add(_service.GetDescription(MotionType.OscillatoryMotion));

            TextBox_Validating(errorProvider1, timeTextBox);
        }

        /// <summary>
        /// Метод для запрета ввода в comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Действия после выбора значения в comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void СomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MotionType tmp = MotionType.UniformMotion;

            foreach (MotionType field in Enum.GetValues(typeof(MotionType)))
            {
                if (_service.GetDescription(field) == comboBox.Text)
                {
                    tmp = field;
                }
            }

            switch (tmp)
            {
                case MotionType.UniformMotion:
                    TmpMotion = new UniformMotion();
                    break;
                case MotionType.AcceleratedMotion:
                    TmpMotion = new AcceleratedMotion();
                    break;
                case MotionType.OscillatoryMotion:
                    TmpMotion = new OscillatoryMotion();
                    break;
            }

            timeTextBox.Text = null;

            ControlsRemove();

            int coordinateY = 58;
            _fields = TmpMotion.GetType().GetProperties();

            foreach (PropertyInfo node in _fields)
            {
                if (node.Name != _service.GetDescription(MotionFieldsType.Time) && 
                    node.Name != _service.GetDescription(MotionFieldsType.Coordinate))
                {
                    var label = new Label();
                    CreateLabel(label, node, coordinateY);
                    
                    var textBox = new TextBox();
                    CreateTextBox(textBox, label, node, coordinateY);

                    coordinateY = coordinateY + 35;
                }
            }
        }

        /// <summary>
        /// Метод для очистки groupBox от всех элементов управления, которые не связаны с 
        /// параметром Time
        /// </summary>
        private void ControlsRemove()
        {
            for (int i = groupBox.Controls.Count - 1; i >= 2; i--)
            {
                groupBox.Controls.Remove(groupBox.Controls[i]);
            }
        }

        /// <summary>
        /// Метод для добавления в groupBox строковых записей в соответствии с 
        /// указываемыми названиями параметров движения
        /// </summary>
        /// <param name="label">Строковая запись, текст которой нужно изменить</param>
        /// <param name="field">Параметр движения, в соответсвии с которым нужно 
        /// изметь текст строковой записи</param>
        /// <param name="coordinate">Вертикальная координа начала размещения 
        /// строковых записей в groupBox</param>
        private void CreateLabel (Label label, MemberInfo field, int coordinate)
        {
            if (field.Name == _service.GetDescription(MotionFieldsType.Speed))
            {
                label.Text = "Скорость, м/с:";
            }
            else if (field.Name == _service.GetDescription(MotionFieldsType.Acceleration))
            {
                label.Text = "Ускорение, м/с^2:";
            }
            else if (field.Name == _service.GetDescription(MotionFieldsType.StartCoordinate))
            {
                label.Text = "Начальная координата, м:";
            }
            else if (field.Name == _service.GetDescription(MotionFieldsType.Amplitude))
            {
                label.Text = "Амплитуда отклонения, м:";
            }
            else if (field.Name == _service.GetDescription(MotionFieldsType.StartingPosition))
            {
                label.Text = "Начальное положение:";
            }
            else if (field.Name == _service.GetDescription(MotionFieldsType.InitialPhase))
            {
                label.Text = "Начальная фаза, град:";
            }
            else if (field.Name == _service.GetDescription(MotionFieldsType.CyclicFrequency))
            {
                label.Text = "Циклическая частота, рад/с:";
            }

            label.Name = $"{field.Name}Label";

            var labelWidth =
                field.Name == _service.GetDescription(MotionFieldsType.StartingPosition)
                    ? 125
                    : 155;
            label.Size = new Size(labelWidth, 18);
            

            label.Location = new Point(8, coordinate);
            groupBox.Controls.Add(label);
        }

        /// <summary>
        /// Метод для добавления окон для ввода значений параметров движения и 
        /// добавления к ним указателей правильности ввода (errorProvider)
        /// </summary>
        /// <param name="textBox">Добавляемое окно ввода значения параметра</param>
        /// <param name="label">Строковая запись для параметра StartingPosition</param>
        /// <param name="field">Параметр движения, для которого добавляется окно ввода</param>
        /// <param name="coordinate">Вертикальная координа начала размещения 
        /// строковых записей в groupBox</param>
        private void CreateTextBox(TextBox textBox, Label label, MemberInfo field, int coordinate)
        {
            textBox.Name = $"{field.Name}TextBox";
            textBox.Size = new Size(46, 22);
            textBox.Location = new Point(168, coordinate - 2);
            groupBox.Controls.Add(textBox);

            var errorProvider = new ErrorProvider {BlinkRate = 0};

            if (field.Name == _service.GetDescription(MotionFieldsType.StartingPosition))
            {
                var infoErrorProvider = new ErrorProvider
                {
                    BlinkRate = 0, 
                    Icon = Properties.Resources.help
                };
                infoErrorProvider.SetError(label, "0 - положение равновесия, " +
                                                  "1 - положение максимального отклонения.");

                TextBox_Validating(errorProvider, textBox);
            }
            else
            {
                TextBox_Validating(errorProvider, textBox);
            }
        }

        /// <summary>
        /// Метод для проверки правильности информации, введенной в textBox
        /// </summary>
        /// <param name="errorProvider">Указатель правильности ввода для сигнализации 
        /// об ошибке</param>
        /// <param name="control">Элемент управления, к которому должен относиться 
        /// указатель правильности ввода</param>
        private void TextBox_Validating(ErrorProvider errorProvider, Control ctrl)
        {
            ctrl.Validating += Validation;

            void Validation(object sender, CancelEventArgs e)
            {
                try
                {
                    if (string.IsNullOrEmpty(ctrl.Text))
                    {
                        errorProvider.SetError(ctrl, "Значение должно быть заполнено.");
                    }                    
                    else if (!ctrl.Name.Contains(_service.GetDescription(MotionFieldsType.
                        StartingPosition)) && !_correctValueRegex.IsMatch(ctrl.Text))
                    {
                        errorProvider.SetError(ctrl, "Допускается только ввод цифр.");
                    }
                    else if (ctrl.Name.Contains(_service.GetDescription(MotionFieldsType.
                        StartingPosition)) && !_startingPositionRegex.IsMatch(ctrl.Text))
                    {
                        errorProvider.SetError(ctrl, "Параметр может принимать значение, " +
                            "равное только 0 или 1.");
                    }
                    else
                    {
                        errorProvider.Clear();
                    }
                }
                catch (Exception exception)
                {
                    errorProvider.SetError(ctrl, exception.Message);
                }
                                           
            }
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
                if (string.IsNullOrEmpty(comboBox.Text))
                {
                    throw new Exception("Тип движения не выбран!");
                }
                
                foreach (Control ctrl in groupBox.Controls)
                {
                    if (ctrl.GetType() == typeof(TextBox))
                    {
                        if (string.IsNullOrEmpty(ctrl.Text))
                        {
                            throw new Exception("Строки ввода не должны быть пустыми.");
                        }
                        else if (!_correctValueRegex.IsMatch(ctrl.Text) && !ctrl.Name.Contains
                            (_service.GetDescription(MotionFieldsType.StartingPosition)) || 
                            (!_startingPositionRegex.IsMatch(ctrl.Text) && ctrl.Name.Contains
                            (_service.GetDescription(MotionFieldsType.StartingPosition))))
                        {
                            throw new Exception("В качестве значений параметров могут быть " +
                                "введены только цифры.");
                        }
                    }
                }

                AddMotionParamentersValue();

                Close();
            }
            catch (Exception exception)
            {                
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод для считывания значений из окон ввода и их присвоения параметрам движения
        /// </summary>
        /// <remarks>
        /// Присутствует замена точек на запятые в считываемых значениях
        /// </remarks>
        private void AddMotionParamentersValue()
        {
            var valueList = new List<string>();

            foreach (Control ctrl in groupBox.Controls)
            {
                if (ctrl.GetType() != typeof(TextBox)) continue;

                if (ctrl.Text.Contains("."))
                {
                    ctrl.Text = ctrl.Text.Replace(".", ",");
                }

                valueList.Add(ctrl.Text);
            }

            switch (TmpMotion)
            {
                case UniformMotion motion:
                    motion.Time = double.Parse(valueList[0]);
                    motion.Speed = double.Parse(valueList[1]);
                    break;
                case AcceleratedMotion motion:
                    motion.Time = double.Parse(valueList[0]);
                    motion.Speed = double.Parse(valueList[1]);
                    motion.StartCoordinate = double.Parse(valueList[2]);
                    motion.Acceleration = double.Parse(valueList[3]);
                    break;
                case OscillatoryMotion motion:
                    motion.Time = double.Parse(valueList[0]);
                    motion.Amplitude = double.Parse(valueList[1]);
                    motion.StartingPosition = (StartingPositionType)int.Parse(valueList[2]);
                    motion.CyclicFrequency = double.Parse(valueList[3]);
                    motion.InitialPhase = double.Parse(valueList[4]);
                    break;
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

        /// <summary>
        /// Действия при нажатии кнопки "Случайное заполнение"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateRandomDataButton_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in groupBox.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    ctrl.Text = null;
                }
            }
            
            Random rand = new Random();
            int choice = rand.Next(0, comboBox.Items.Count);

            comboBox.Text = comboBox.Items[choice].ToString();

            foreach (Control ctrl in groupBox.Controls)
            {
                if (ctrl.GetType() != typeof(TextBox)) continue;

                if (ctrl.Name.Contains(_service.GetDescription(MotionFieldsType.
                    StartingPosition)))
                {
                    ctrl.Text = rand.Next(0, 
                        Enum.GetNames(typeof(StartingPositionType)).Length).ToString();
                }
                else if (ctrl.Name.Contains(_service.GetDescription(MotionFieldsType.
                    InitialPhase)))
                {
                    ctrl.Text = rand.Next(-(OscillatoryMotion.MaxPhase - 1), 
                        OscillatoryMotion.MaxPhase).ToString();
                }
                else
                {
                    ctrl.Text = rand.Next(1, 501).ToString();
                }
            }
        }
    }
}
