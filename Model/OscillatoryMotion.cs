using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Класс для описания колебательного движения объектов
    /// </summary>
    public class OscillatoryMotion : MotionBase
    {
        /// <summary>
        /// Амплитуда колебаний
        /// </summary>
        private double _amplitude;

        /// <summary>
        /// Начальное положение тела
        /// </summary>
        private StartingPositionType _startingPosition;

        /// <summary>
        /// Циклическая частота колебаний
        /// </summary>
        private double _cyclicFrequency;

        /// <summary>
        /// Начальная фаза колебаний
        /// </summary>
        private double _initialPhase;

        /// <summary>
        /// Максимально возможное значение начальной фазы колебаний
        /// </summary>
        public const byte MaxPhase = 180;

        /// <summary>
        /// Амплитуда отклонения объекта от положения равновесия
        /// </summary>
        public double Amplitude
        {
            get
            {
                return _amplitude;
            }
            set
            {
                if (value > 0)
                {
                    _amplitude = value;
                }
                else
                {
                    throw new Exception("Амплитуда должна быть больше нуля.");
                }
            }
        }

        /// <summary>
        /// Начальное положение тела: 0 - положение равновесия, 1 - положение 
        /// максимального отклонения
        /// </summary>
        public StartingPositionType StartingPosition
        {
            get
            {
                return _startingPosition;
            }
            set
            {
                switch (value)
                {
                    case StartingPositionType.Equilibrium:
                        _startingPosition = value;
                        break;
                    case StartingPositionType.MaxDeviation:
                        _startingPosition = value;
                        break;
                    default:
                        throw new Exception($"Начальное положение тела должно принимать значение " +
                            $"{(int)StartingPositionType.Equilibrium} или {(int)StartingPositionType.MaxDeviation}.");
                }
            }
        }

        /// <summary>
        /// Циклическая частота колебаний [рад/сек]
        /// </summary>
        public double CyclicFrequency
        {
            get
            {
                return _cyclicFrequency;
            }
            set
            {
                if (value > 0)
                {
                    _cyclicFrequency = value;
                }
                else
                {
                    throw new Exception("Частота должна быть больше нуля.");
                }
            }
        }

        /// <summary>
        /// Начальная фаза колебаний [град]
        /// </summary>
        public double InitialPhase
        {
            get
            {
                return _initialPhase;
            }
            set
            {
                if (Math.Abs(value) < MaxPhase)
                {
                    _initialPhase = value * Math.PI / 180;
                }
                else
                {
                    throw new Exception($"Начальная фаза должна принимать значения в интервале " +
                        $"от {-(MaxPhase - 1)} до {MaxPhase - 1} градусов.");
                }
            }
        }

        /// <summary>
        /// Метод для определения координаты нахождения объекта
        /// </summary>
        /// <returns>Значение типа double</returns>
        protected override double CalculateCoordinate()
        {
            double coordinate;

            switch (StartingPosition)
            {
                case StartingPositionType.Equilibrium:
                    coordinate = Amplitude * Math.Sin(CyclicFrequency * Time + InitialPhase);
                    break;
                default:
                    coordinate = Amplitude * Math.Cos(CyclicFrequency * Time + InitialPhase);
                    break;
            }

            return coordinate;
        }
    }
}
