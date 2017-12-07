using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    public class Reactor: Vessel
    {
        List<Material> m_Inputs;
        List<Material> m_Outputs;

        public Reactor(double volume = double.NaN, double operatingPressure = 62.5, double aspectRatio = 4, bool horizontal = false): base(volume, operatingPressure, aspectRatio, VesselOrientation.Vertical)
        {
            m_Inputs = new List<Material>();
            m_Outputs = new List<Material>();
        }

        Reaction m_Reaction;

        public Reaction Reaction
        {
            get
            {
                return m_Reaction;
            }
            set
            {
                m_Reaction = value;
            }
        }

        public void AddInput(Material input)
        {
            this.m_Inputs.Add(input);
        }

        public Material[] Inputs
        {
            get
            {
                return m_Inputs.ToArray();
            }
        }

        public void AddOutput(Material output)
        {
            this.m_Outputs.Add(output);
        }

        public Material[] Outputs
        {
            get
            {
                return m_Outputs.ToArray();
            }
        }

        public void Calculate()
        {
            string[] chems = m_Inputs[0].ChemicalNames;
            double[] flows = new double[m_Inputs[0].Chemicals.Length];
            for(int i = 0; i < flows.Length; i++) flows[i] = 0;
            foreach (Material m in m_Inputs)
            {
                for (int i = 0; i < flows.Length; i++)
                {
                    flows[i] = flows[i] + m.Concentrations[i] * m.FlowRate;
                }
            }
            for (int i = 0; i < chems.Length; i++)
            {
                flows[i] = flows[i] + m_Reaction.Stoichiometry(chems[i]) * m_Reaction.Extent;
            }
            m_Outputs[0].Flows = flows;
            Volume = m_Outputs[0].FlowRate * m_Reaction.ResidenceTime;
        }
    }
}

