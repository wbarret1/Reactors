using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    internal class ReactionConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is Reaction)
            {
                Reaction react = (Reaction)value;
                return react.ToString();
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(ReactionConverter))]
    public class Reaction
    {
        List<Reactant> m_Reactants;
        List<ReactionProduct> m_ReactionProducts;
        List<ReactionByProduct> m_ReactionByProducts;
        List<Catalyst> m_Catalysts;
        List<Solvent> m_Solvents;
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

        public ReactionProduct[] ReactionProducts
        {
            get
            {
                return m_ReactionProducts.ToArray();
            }
        }

        public ReactionByProduct[] ReactionByProducts
        {
            get
            {
                return m_ReactionByProducts.ToArray();
            }
        }

        public Catalyst[] Catalysts
        {
            get
            {
                return m_Catalysts.ToArray();
            }
        }

        public Solvent[] Solvents
        {
            get
            {
                return m_Solvents.ToArray();
            }
        }

        public void AddReactant(Chemical chemical, double stoichiomety)
        {
            m_Reactants.Add(new Reactant(chemical, stoichiomety));
        }

        public void AddReactionProduct(Chemical chemical, double stoichiomety)
        {
            m_ReactionProducts.Add(new ReactionProduct(chemical, stoichiomety));
        }

        public void AddReactionByProduct(Chemical chemical, double stoichiomety)
        {
            m_ReactionByProducts.Add(new ReactionByProduct(chemical, stoichiomety));
        }

        public void AddCatalyst(Chemical chemical)
        {
            m_Catalysts.Add(new Catalyst(chemical));
        }

        public void AddSolvent(Chemical chemical, double stoichiomety)
        {
            m_Solvents.Add(new Solvent(chemical));
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

        public override string ToString()
        {
            List<Reactant> temp = new List<Reactant>();
            temp.AddRange(m_Reactants.ToArray());
            int countNeg = 0;
            foreach(Reactant r in temp)
            {
                if (r.StoichiometricCoefficient < 0)
                    countNeg++;
            }
            string retVal = Math.Abs(temp[0].StoichiometricCoefficient).ToString() + temp[0].Chemical.Name + " ";
            for (int i = 1; i < countNeg; i++)
            {
                retVal = retVal + Math.Abs(temp[i].StoichiometricCoefficient).ToString() + temp[i].Chemical.Name + " ";
            }
            retVal = retVal + "-> ";
            for (int i = countNeg; i < temp.Count; i++)
            {
                retVal = retVal + Math.Abs(temp[i].StoichiometricCoefficient).ToString() + temp[i].Chemical.Name + " ";
            }
            return retVal.Trim();
        }

    }
}
