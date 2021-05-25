using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace View
{
    /// <summary>
    /// Параметры для описания движения тел
    /// </summary>
    public enum MotionFieldsType
    {
        [Description("Time")] Time,
        [Description("Coordinate")] Coordinate,
        [Description("Speed")] Speed,
        [Description("Acceleration")] Acceleration,
        [Description("StartCoordinate")] StartCoordinate,
        [Description("Amplitude")] Amplitude,
        [Description("StartingPosition")] StartingPosition,
        [Description("InitialPhase")] InitialPhase,
        [Description("CyclicFrequency")] CyclicFrequency
    }
}
