using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    public class Reactant
    {

        //public Reactant()
        //{
        //    m_Chemical = null;
        //    m_StoichiometricCoefficient = 0.0;
        //}

        public Reactant(Chemical chemical, double stoichiomety)
        {
            m_Chemical = chemical;
            m_StoichiometricCoefficient = stoichiomety;
        }

        Chemical m_Chemical;
        double m_StoichiometricCoefficient;

        public Chemical Chemical
        {
            get
            {
                return m_Chemical;
            }
            set
            {
                m_Chemical = value;                
            }
        }

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
    }
}
