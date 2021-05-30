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
        private Regex _correctValueRegex = new Regex(ServiceOptions.TmpParametrsValue);

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
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
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
        private void ComboBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Действия после выбора значения в comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void comboBox_SelectedIndexChanged(object sender, EventArgs e)
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
            
            for (int i = groupBox.Controls.Count - 1; i >= 2; i--)
            {
                groupBox.Controls.Remove(groupBox.Controls[i]);                
            }

            int coordinateY = 58;
            _fields = TmpMotion.GetType().GetProperties();

            foreach (PropertyInfo node in _fields)
            {
                if (node.Name != _service.GetDescription(MotionFieldsType.Time) && 
                    node.Name != _service.GetDescription(MotionFieldsType.Coordinate))
                {
                    var label = new Label();

                    if (node.Name == _service.GetDescription(MotionFieldsType.Speed))
                    {
                        label.Text = "Скорость, м/с:";
                    }
                    else if (node.Name == _service.GetDescription(MotionFieldsType.Acceleration))
                    {
                        label.Text = "Ускорение, м/с^2:";
                    }
                    else if (node.Name == _service.GetDescription(MotionFieldsType.StartCoordinate))
                    {
                        label.Text = "Начальная координата, м:";
                    }
                    else if (node.Name == _service.GetDescription(MotionFieldsType.Amplitude))
                    {
                        label.Text = "Амплитуда отклонения, м:";
                    }
                    else if (node.Name == _service.GetDescription(MotionFieldsType.StartingPosition))
                    {
                        label.Text = "Начальное положение:";
                    }
                    else if (node.Name == _service.GetDescription(MotionFieldsType.InitialPhase))
                    {
                        label.Text = "Начальная фаза, град:";
                    }
                    else if (node.Name == _service.GetDescription(MotionFieldsType.CyclicFrequency))
                    {
                        label.Text = "Циклическая частота, рад/с:";
                    }

                    label.Name = $"{node.Name}Label";

                    if (node.Name == _service.GetDescription(MotionFieldsType.StartingPosition))
                    {
                        label.Size = new Size(125, 18);
                    }
                    else
                    {
                        label.Size = new Size(155, 18);
                    }
                    
                    label.Location = new Point(8, coordinateY);
                    groupBox.Controls.Add(label);

                    var textBox = new TextBox();
                    textBox.Name = $"{node.Name}TextBox";
                    textBox.Size = new Size(46, 22);
                    textBox.Location = new Point(168, coordinateY - 2);
                    groupBox.Controls.Add(textBox);

                    var errorProvider = new ErrorProvider();
                    errorProvider.BlinkRate = 0;

                    if (node.Name == _service.GetDescription(MotionFieldsType.StartingPosition))
                    {
                        var infoErrorProvider = new ErrorProvider();
                        infoErrorProvider.BlinkRate = 0;
                        infoErrorProvider.Icon = Properties.Resources.help;
                        infoErrorProvider.SetError(label, "0 - положение равновесия, " +
                            "1 - положение максимального отклонения.");

                        TextBox_Validating(errorProvider, textBox);
                    }
                    else
                    {
                        TextBox_Validating(errorProvider, textBox);
                    }

                    coordinateY = coordinateY + 35;
                }
            }

        }

        /// <summary>
        /// Метод для проверки правильности информации, введенной в textBox
        /// </summary>
        /// <param name="errorProvider"></param>
        /// <param name="control"></param>
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

                var valueList = new List<string>();

                foreach (Control ctrl in groupBox.Controls)
                {                    
                    if (ctrl.GetType() == typeof(TextBox))
                    {
                        if (ctrl.Text.Contains("."))
                        {
                            ctrl.Text = ctrl.Text.Replace(".", ",");
                        }

                        valueList.Add(ctrl.Text);
                    }
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
