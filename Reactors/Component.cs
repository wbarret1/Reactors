using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(ComponentConverter))]
    public class Component
    {
        public string Name { get; set; }
        public double Concentration { get; set; }
        public double Flow { get; set; }
    }

    internal class ComponentConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is Component)
            {
                Component comp = (Component)value;
                return "Concentration = " + comp.Concentration;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
