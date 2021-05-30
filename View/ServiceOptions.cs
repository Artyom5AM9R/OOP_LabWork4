using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace View
{
    /// <summary>
    /// Класс с полями и методами специального назначения
    /// </summary>
    public class ServiceOptions
    {
        /// <summary>
        /// Шаблон для отбора правильных значений параметров движения
        /// </summary>
        /// <remarks>
        /// Не является корректным для параметра StartingPosition из 
        /// класса OscillatoryMotion
        /// </remarks>
        public const string TmpParametrsValue = @"(^(-)?([0-9]+)(\,|\.)?([0-9])+$)|" +
            @"(^(-)?([0-9])+$)";

        /// <summary>
        /// Шаблон для отбора правильных значений для параметра StartingPosition  из 
        /// класса OscillatoryMotion
        /// </summary>
        /// /// <remarks>
        /// Не является корректным для других параметров движения
        /// </remarks>
        public const string TmpStartingPositionValue = @"^[0-1]{1,1}$";

        /// <summary>
        /// Метод для приведения значения перечисления в удобочитаемый формат.
        /// </summary>
        /// <param name="enumElement">Элемент перечисления</param>
        /// <returns>Название элемента</returns>
        public string GetDescription(Enum enumElement)
        {
            var type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes
                    (typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumElement.ToString();
        }

        /// <summary>
        /// Метод для получения названия типа объекта из пространства имен Model по его 
        /// описанию из MotionType
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Название типа</returns>
        public string GetEnumElementName(string description)
        {
            foreach (Enum element in Enum.GetValues(typeof(MotionType)))
            {
                if (GetDescription(element) == description)
                {
                    return $"{typeof(MotionBase).Namespace}.{element}";
                }
            }

            return description;
        }
    }
}
