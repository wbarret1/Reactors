using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    //internal class ChemicalConverter : System.ComponentModel.ExpandableObjectConverter
    //{
    //    public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context,
    //                         System.Globalization.CultureInfo culture,
    //                         object value, Type destType)
    //    {
    //        if (destType == typeof(string) && value is Chemical)
    //        {
    //            Chemical chem = (Chemical)value;
    //            return chem.Name;
    //        }
    //        return base.ConvertTo(context, culture, value, destType);
    //    }
    //}

    [Serializable]
    //[System.ComponentModel.TypeConverter(typeof(ChemicalConverter))]
    public class Chemical
    {
        public Chemical(string name, string casNo, double MolWt, double heatOfFormation, double entropyOfFormation)
        {
            Name = name;
            CasNo = casNo;
            MolecularWeight = MolWt;
            HeatOfFormation = heatOfFormation;
            EntropyOfFormation = entropyOfFormation;
        }
        public string Name { get; set; }


        private string m_CasNo;
        public string CasNo
        {
            get
            {
                return m_CasNo;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    m_CasNo = string.Empty;
                    return;
                }
                string[] numbers = value.Split('-');
                if (numbers.Length != 3)
                {
                    throw new System.Exception("Incorrect format.");
                }
                m_CasNo = numbers[0] + "-" + numbers[1] + "-" + numbers[2];
            }
        }
        public double MolecularWeight { get; set; }
        public double HeatOfFormation { get; set; }
        public double EntropyOfFormation { get; set; }

    }


}
