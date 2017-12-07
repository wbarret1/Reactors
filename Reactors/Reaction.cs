using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    public class Reaction
    {
        List<Reactant> m_Reactants;
        double m_Extent;
        double m_ResidenceTime;

        public Reaction()
        {
            m_Reactants = new List<Reactant>();
            m_Extent = 0;
        }

        public Reactant[] Reactants
        {
            get
            {
                return m_Reactants.ToArray();
            }
        }

        public void AddReactant(Chemical chemical, double stoichiomety)
        {
            m_Reactants.Add(new Reactant(chemical, stoichiomety));
        }

        public double Stoichiometry(string chemical)
        {
            foreach (Reactant r in m_Reactants)
            {
                if (r.Chemical.Name == chemical) return r.StoichiometricCoefficient;
            }
            return 0.0;
        }

        public double Extent
        {
            get
            {
                return m_Extent;
            }
            set
            {
                m_Extent = value;
            }
        }

        public double ResidenceTime
        {
            get
            {
                return m_ResidenceTime;
            }
            set
            {
                m_ResidenceTime = value;
            }
        }


    }
}
