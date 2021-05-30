using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace View
{
    /// <summary>
    /// Тип движения тела
    /// </summary>
    public enum MotionType
    {
        [Description("Равномерное")]
        UniformMotion,

        [Description("Равноускоренное")] 
        AcceleratedMotion,

        [Description("Колебательное")] 
        OscillatoryMotion
    }
}
