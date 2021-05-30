using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Класс для описания равноускоренного движения объектов
    /// </summary>
    public class AcceleratedMotion : MotionBase
    {
        /// <summary>
        /// Ускорение тела
        /// </summary>
        private double _acceleration;

        /// <summary>
        /// Скорость движения тела
        /// </summary>
        private double _speed;

        /// <summary>
        /// Свойство для доступа к данным о скорости движения объекта
        /// </summary>
        public double Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                if (value > 0 && value <= UniformMotion.MaxSpeed)
                {
                    _speed = value;
                }
                else
                {
                    throw new Exception($"Скорость должна быть больше нуля и не превышать " +
                        $"{UniformMotion.MaxSpeed} м/c.");
                }
            }
        }

        /// <summary>
        /// Свойство для доступа к данным о начальной координате нахождения объекта
        /// </summary>
        public double StartCoordinate { get; set; }


        /// <summary>
        /// Свойство для доступа к данным об ускорении тела
        /// </summary>
        public double Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                if (value != 0 && Math.Abs(value) <= UniformMotion.MaxSpeed)
                {
                    _acceleration = value;
                }
                else
                {
                    throw new Exception($"Ускорение должно быть отличным от нуля и не превышать " +
                        $"{UniformMotion.MaxSpeed} м/с^2.");
                }
            }
        }

        /// <summary>
        /// Метод для определения координаты нахождения объекта
        /// </summary>
        /// <returns>Значение типа double</returns>
        protected override double CalculateCoordinate()
        {
            return StartCoordinate + Speed * Time + Acceleration * Math.Pow(Time, 2) / 2;
        }
    }
}
