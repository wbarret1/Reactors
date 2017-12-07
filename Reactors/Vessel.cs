using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    /// <summary>
    /// Vessel Oreintation
    /// </summary>
    public enum VesselOrientation
    {
        Vertical,
        Horizontal
    }

    public abstract class Vessel
    {

        /// <summary>
        /// Class constructor for the Vessel class. Operating Pressure has units of psig. Aspect ratio is length/diameter.
        /// </summary>
        /// <param name="volume">Volume of the tank, in ft^3.</param>
        /// <param name="operatingPressure">Operating Pressure, psig.</param>
        /// <param name="aspectRatio">Length/diameter of the tank.</param>
        public Vessel(double volume, double operatingPressure, double aspectRatio, VesselOrientation orientation)
        {
            m_Volume = volume;
            m_OperatingPressure = operatingPressure;
            m_AspectRatio = aspectRatio;
            Orientation = orientation;
            m_Diameter = Math.Pow((4 * m_Volume) / (m_AspectRatio * Math.PI), (1 / 3));
        }

        double m_Volume;
        double m_OperatingPressure;
        double m_AspectRatio;
        double m_Diameter;

        /// <summary>
        /// Tank volume in ft^3.
        /// </summary>
        public double Volume
        {
            get
            {
                return m_Volume;
            }
            set
            {
                m_Volume = value;
                m_Diameter = Math.Pow((4 * m_Volume) / (m_AspectRatio * Math.PI), (1 / 3));
            }
        }

        /// <summary>
        /// Operating Pressure in psig.
        /// </summary>
        public double OperatingPressure
        {
            get
            {
                return m_OperatingPressure;
            }
            set
            {
                m_OperatingPressure = value;
            }
        }

        /// <summary>
        /// Length/diameter.
        /// </summary>
        public double AspectRatio
        {
            get
            {
                return m_AspectRatio;
            }
            set
            {
                m_AspectRatio = value;
            }
        }

        /// <summary>
        /// Thickness of the tank walls in inches.
        /// </summary>
        public double TankWallThickness
        {
            get
            {
                if (m_OperatingPressure < 62.5) return 0.003 * 62.5;
                return 0.003 * m_OperatingPressure;
            }
        }

        /// <summary>
        /// Calulates the tank material volume in ft^3.
        /// </summary>
        public double VolumeOfTankMaterial
        {
            get
            {
                return 9 / 2 * Math.PI * Math.Pow(m_Diameter, 2) * (this.TankWallThickness / 12);
            }
        }

        public VesselOrientation Orientation { get; set; }

         /// <summary>
        /// Calulcates the land use area for a tank in ft^2.
        /// </summary>
        /// <param name="orientation">Orientation of the vessel.</param>
        /// <returns>Land area required for the tank in ft^3.</returns>
        public double LandUse(VesselOrientation orientation)
        {
            if (orientation == VesselOrientation.Vertical)
            {
                return 9 * Math.Pow(m_Diameter, 2);
            }
            return 9 * m_AspectRatio * Math.Pow(m_Diameter, 2);
        }

    }
}
