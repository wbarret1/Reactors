﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    internal class ReactantConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is Reactant)
            {
                Reactant react = (Reactant)value;
                return react.Chemical.Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(ReactantConverter))]
    public class Reactant : ReactionParticipant
    {

        public Reactant(Chemical chemical, double stoichiomety): base(chemical)
        {
            m_StoichiometricCoefficient = stoichiomety;
        }

        double m_StoichiometricCoefficient;
        public double StoichiometricCoefficient
        {
            get
            {
                return m_StoichiometricCoefficient;
            }
            set
            {
                m_StoichiometricCoefficient = value;
            }
        }

        // Default comparer for Part type.
        public int CompareTo(Reactant compareReactant)
        {
            // A null value means that this object is greater.
            if (compareReactant == null)
                return 1;

            else
                return this.StoichiometricCoefficient.CompareTo(compareReactant.StoichiometricCoefficient);
        }
    }
}
