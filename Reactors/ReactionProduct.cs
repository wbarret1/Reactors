using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    public class ReactionProduct : ReactionParticipant
    {
        public ReactionProduct(Chemical chemical, double stoichiomety) : base(chemical)
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
