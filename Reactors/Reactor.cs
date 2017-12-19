using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    [Serializable]
    public class Reactor: Vessel
    {
        MaterialCollection m_Inputs;
        MaterialCollection m_Outputs;

        public Reactor(string name, double volume = double.NaN, double operatingPressure = 62.5, double aspectRatio = 4, bool horizontal = false): base(name, volume, operatingPressure, aspectRatio, VesselOrientation.Vertical)
        {
            m_Inputs = new MaterialCollection();
            m_Inputs.Name = "Input materials";
            m_Outputs = new MaterialCollection();
            m_Outputs.Name = "Output materials";

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

        public double HeatDuty { get; set; }
        public void AddInput(Material input)
        {
            this.m_Inputs.Add(input);
            input.RaiseMaterialChangedEvent += this.HandleMaterialChangedEvent;
        }

        // Define what actions to take when the event is raised.
        void HandleMaterialChangedEvent(object sender, MaterialEventArgs e)
        {
            this.Calculate();
        }

        public MaterialCollection Inputs
        {
            get
            {
                return m_Inputs;
            }
        }

        public void AddOutput(Material output)
        {
            this.m_Outputs.Add(output);
        }

        public MaterialCollection Outputs
        {
            get
            {
                return m_Outputs;
            }
        }

        public void Calculate()
        {
            string[] chems = m_Inputs[0].ChemicalNames;
            double[] flows = new double[m_Inputs[0].Chemicals.Length];
            for(int i = 0; i < flows.Length; i++) flows[i] = 0;
            HeatDuty = 0.0;
            foreach (Material m in m_Inputs)
            {
                if (m.Basis.ToLower() == "mole")
                {
                    for (int i = 0; i < flows.Length; i++)
                    {
                        flows[i] = flows[i] + m.Concentrations[i] * m.FlowRate;
                    }
                }
                else
                {
                    for (int i = 0; i < flows.Length; i++)
                    {
                        flows[i] = flows[i] + m.Concentrations[i] * m.FlowRate/m.Chemicals[i].MolecularWeight;
                    }

                }
            }
            for (int i = 0; i < chems.Length; i++)
            {
                flows[i] = flows[i] + m_Reaction.Stoichiometry(chems[i]) * m_Reaction.Extent;
                HeatDuty = HeatDuty + m_Reaction.Stoichiometry(chems[i]) * m_Reaction.Extent * m_Outputs[0].Chemicals[i].HeatOfFormation;
                if (m_Outputs[0].Basis == "mass")
                {
                    flows[i] = flows[i] * m_Outputs[0].Chemicals[i].MolecularWeight;
                }
            }
            m_Outputs[0].Flows = flows;
            Volume = m_Outputs[0].FlowRate * m_Reaction.ResidenceTime;
        }
    }
}

