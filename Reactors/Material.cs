using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{

    public class Component
    {
        public string Name;
        public double concentration;
        public double Flow;
    }


    public class Material //: System.ComponentModel.BindingList<Chemical>
        //System.ComponentModel.ICustomTypeDescriptor
    {

        List<Chemical> chemicals;
        List<Component> comps;
        System.Collections.Hashtable chemTable;

        public Material(string name)
        {
            chemicals = new List<Chemical>();
            comps = new List<Component>();
            chemTable = new System.Collections.Hashtable();
            Name = name;
            m_basis = "mass";
        }

        public string Name { get; set; }
        string m_basis;
        public string basis {
            get
            {
                return m_basis;
            }

            set
            {
                if (value.ToLower() == "mass" || value.ToLower() == "mole")
                    m_basis = value.ToLower();
            }
        }

        public double FlowRate { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }

        public void AddChemical(Chemical chem, double concentration = 0.0)
        {
            chemicals.Add(chem);
            Component comp = new Component();
            comp.Name = chem.Name;
            comp.concentration = concentration;
            comps.Add(comp);
            chemTable.Add(chem.Name, concentration);
        }

        public Component[] Components
        {
            get
            {
                return comps.ToArray();
            }
        }

        [System.ComponentModel.Browsable(false)]
        public Chemical[] Chemicals
        {
            get
            {                
                return this.chemicals.ToArray();
            }
        }

        [System.ComponentModel.Browsable(false)]
        public string[] ChemicalNames
        {
            get
            {
                List<string> retVal = new List<string>();
                foreach (Chemical c in this.Chemicals) retVal.Add(c.Name);
                return retVal.ToArray();
            }
        }


        [System.ComponentModel.Browsable(false)]
        public double[] Concentrations
        {
            get
            {
                double[] retVal = new double[comps.Count];
                for (int i = 0; i < comps.Count; i++)
                {
                    retVal[i] = comps[i].concentration;
                }
                return retVal;
            }
            set
            {
                FlowRate = 0;
                for (int i = 0; i < comps.Count; i++)
                {
                    comps[i].concentration = value[i];
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public double[] Flows
        {
            get
            {
                double[] retVal = new double[comps.Count];
                for (int i = 0; i < comps.Count; i++)
                {
                    retVal[i] = comps[i].Flow;
                }
                return retVal;
            }
            set
            {
                FlowRate = 0;
                foreach (double flow in value) FlowRate = FlowRate + flow;
                for (int i = 0; i < comps.Count; i++)
                {
                    comps[i].Flow = value[i];
                    comps[i].concentration = value[i] / FlowRate;
                }
            }
        }

        public void GetConcentration(string chemical, double concentration)
        {
            chemTable[chemical] = concentration;
        }

        public double GetConcetration(string chemical)
        {
            return (double)chemTable[chemical];
        }
    }
}
