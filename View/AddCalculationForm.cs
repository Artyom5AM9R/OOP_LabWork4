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
        public MotionBase TmpMotion;

        /// <summary>
        /// Список полей объекта класса MotionBase
        /// </summary>
        private PropertyInfo[] _fields;

        /// <summary>
        /// Регулярное выражение для отбора правильных вариантов заполнения всех textBox, 
        /// кроме startingPositionTextBox
        /// </summary>
        private Regex _correctValueRegex = new Regex(@"^(-)?([0-9]+)(.|,)?([0-9])+$");

        /// <summary>
        /// Регулярное выражение для отбора правильных вариантов заполнения startingPositionTextBox 
        /// </summary>
        private Regex _startingPositionRegex = new Regex(@"^[0-1]{1,1}$");

        /// <summary>
        /// Приведение значения перечисления в удобочитаемый формат.
        /// </summary>
        /// <param name="enumElement">Элемент перечисления</param>
        /// <returns>Название элемента</returns>
        static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumElement.ToString();
        }

        /// <summary>
        /// Действия, выполняемые при открытии формы
        /// </summary>
        public AddCalculationForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            comboBox.Items.Add(GetDescription(MotionType.UniformMotion));
            comboBox.Items.Add(GetDescription(MotionType.AcceleratedMotion));
            comboBox.Items.Add(GetDescription(MotionType.OscillatoryMotion));

            TextBox_Validating(errorProvider1, timeTextBox);
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
                if (GetDescription(field) == comboBox.Text)
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
                if (node.Name != GetDescription(MotionFieldsType.Time) && 
                    node.Name != GetDescription(MotionFieldsType.Coordinate))
                {
                    var label = new Label();

                    if (node.Name == GetDescription(MotionFieldsType.Speed))
                    {
                        label.Text = "Скорость, м/с:";
                    }
                    else if (node.Name == GetDescription(MotionFieldsType.Acceleration))
                    {
                        label.Text = "Ускорение, м/с^2:";
                    }
                    else if (node.Name == GetDescription(MotionFieldsType.StartCoordinate))
                    {
                        label.Text = "Начальная координата, м:";
                    }
                    else if (node.Name == GetDescription(MotionFieldsType.Amplitude))
                    {
                        label.Text = "Амплитуда отклонения, м:";
                    }
                    else if (node.Name == GetDescription(MotionFieldsType.StartingPosition))
                    {
                        label.Text = "Начальное положение:";
                    }
                    else if (node.Name == GetDescription(MotionFieldsType.InitialPhase))
                    {
                        label.Text = "Начальная фаза, град:";
                    }
                    else if (node.Name == GetDescription(MotionFieldsType.CyclicFrequency))
                    {
                        label.Text = "Циклическая частота, рад/с:";
                    }

                    /*switch (node.Name)
                    {
                        case "Speed" :
                            label.Text = "Скорость, м/с:";
                            break;
                        case "Acceleration":
                            label.Text = "Ускорение, м/с^2:";
                            break;
                        case "StartCoordinate":
                            label.Text = "Начальная координата, м:";
                            break;
                        case "Amplitude":
                            label.Text = "Амплитуда отклонения, м:";
                            break;
                        case "StartingPosition":
                            label.Text = "Начальное положение:";
                            break;
                        case "InitialPhase":
                            label.Text = "Начальная фаза, град:";
                            break;
                        case "CyclicFrequency":
                            label.Text = "Циклическая частота, рад/с:";
                            break;
                    }*/

                    label.Name = $"{node.Name}Label";

                    if (node.Name == GetDescription(MotionFieldsType.StartingPosition))
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

                    if (node.Name == GetDescription(MotionFieldsType.StartingPosition))
                    {
                        errorProvider.SetError(label, "0 - положение равновесия, 1 - положение " +
                            "максимального отклонения.");
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
                    else if (!ctrl.Name.Contains(GetDescription(MotionFieldsType.StartingPosition)) && 
                        !_correctValueRegex.IsMatch(ctrl.Text))
                    {
                        errorProvider.SetError(ctrl, "Допускается только ввод цифр.");
                    }
                    else if (ctrl.Name.Contains(GetDescription(MotionFieldsType.StartingPosition)) && 
                        !_startingPositionRegex.IsMatch(ctrl.Text))
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
                foreach (Control ctrl in groupBox.Controls)
                {
                    if (ctrl.GetType() == typeof(TextBox))
                    {
                        if (string.IsNullOrEmpty(ctrl.Text))
                        {
                            throw new Exception("Строки ввода не должны быть пустыми.");
                        }
                        else if (!_correctValueRegex.IsMatch(ctrl.Text) &&
                            !ctrl.Name.Contains(GetDescription(MotionFieldsType.StartingPosition)) 
                            || (!_startingPositionRegex.IsMatch(ctrl.Text) 
                            && ctrl.Name.Contains(GetDescription(MotionFieldsType.StartingPosition))))
                        {
                            throw new Exception("В качестве значений параметров могут быть введены " +
                                "только цифры.");
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

                if (!string.IsNullOrEmpty(comboBox.Text))
                {
                    CoordinateDeterminationForm.Flag = true;
                    Close();
                }
                else
                {
                    throw new Exception("Тип движения не выбран!");
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
                if (ctrl.GetType() == typeof(TextBox))
                {
                    if (ctrl.Name.Contains(GetDescription(MotionFieldsType.StartingPosition)))
                    {
                        ctrl.Text = rand.Next(0, 
                            Enum.GetNames(typeof(StartingPositionType)).Length).ToString();
                    }
                    else if (ctrl.Name.Contains(GetDescription(MotionFieldsType.InitialPhase)))
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
}
