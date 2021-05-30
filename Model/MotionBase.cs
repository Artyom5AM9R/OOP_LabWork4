using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [Serializable]
    [XmlInclude(typeof(UniformMotion))]
    [XmlInclude(typeof(AcceleratedMotion))]
    [XmlInclude(typeof(OscillatoryMotion))]

    /// <summary>
    /// Класс для описания механического движения объектов
    /// </summary>
    public abstract class MotionBase
    {
        /// <summary>
        /// Время движения тела
        /// </summary>
        private double _time;

        /// <summary>
        /// Свойство для доступа к данным о координате нахождения объекта.
        /// Значение координаты округляется до 2-х знаков после запятой.
        /// </summary>
        public double Coordinate
        {
            get
            {
                return CalculateCoordinate();
            }
        }

        /// <summary>
        /// Свойство для доступа к данным о времени движения объекта
        /// </summary>
        public double Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (value > 0)
                {
                    _time = value;
                }
                else
                {
                    throw new Exception("Время должно быть больше нуля.");
                }
            }
        }

        /// <summary>
        /// Метод для определения координаты нахождения объекта
        /// </summary>
        protected abstract double CalculateCoordinate();
    }
}
