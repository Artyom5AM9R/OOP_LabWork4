using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Model;

namespace View
{
    /// <summary>
    /// Класс для сериализации данных
    /// </summary>
    public class Serializer
    {
        /// <summary>
        /// Метод для открытия файла с объектами класса MotionBase
        /// </summary>
        /// <param name="fileName">Путь к открываемому файлу</param>
        /// <param name="list">Список для сохренения данных из файла</param>
        /// <returns>Список типа List с объектами класса MotionBase</returns>
        public List<MotionBase> OpenFile(string fileName, List<MotionBase> list)
        {
            var reader = new XmlSerializer(typeof(List<MotionBase>));

            using (var file = System.IO.File.OpenRead(fileName))
            {
                list = new List<MotionBase>((IEnumerable<MotionBase>)reader.
                    Deserialize(file));
            }

            return list;
        }

        /// <summary>
        /// Метод для сохранения данных об объектах класса MotionBase
        /// </summary>
        /// <param name="fileName">Путь к сохраняемому файлу</param>
        /// <param name="list">Список, который необходимо сохранить в файл</param>
        public void SaveFile(string fileName, List<MotionBase> list)
        {
            var writer = new XmlSerializer(typeof(List<MotionBase>));

            using (var file = System.IO.File.Create(fileName))
            {
                writer.Serialize(file, list);
            }
        }
    }
}
