using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Класс для описания равномерного движения объектов
    /// </summary>
    public class UniformMotion : MotionBase
    {
        /// <summary>
        /// Скорость движения тела
        /// </summary>
        private double _speed;

        /// <summary>
        /// Максимально возможная скорость тела
        /// </summary>
        public const int MaxSpeed = 299792458;

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
                if (value > 0 && value <= MaxSpeed)
                {
                    _speed = value;
                }
                else
                {
                    throw new Exception($"Скорость должна быть больше нуля и не превышать {MaxSpeed} м/с.");
                }
            }
        }

        /// <summary>
        /// Метод для определения координаты нахождения объекта с записью
        /// </summary>
        /// <returns>Значение типа double</returns>
        protected override double CalculateCoordinate()
        {
            double coordinate = Speed * Time;

            return coordinate;
        }
    }
}
