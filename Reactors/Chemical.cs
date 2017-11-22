using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    class Chemical
    {
        public Chemical()
        {

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
                string[] numbers = value.Split('-');
                if (numbers.Length != 3)
                {
                    throw new System.Exception("Incorrect format.");
                }
                m_CasNo = numbers[0] + "-" + numbers[1] + "-" + numbers[2];
            }
        }
        public double MolecularWeight { get; set; }
        public double heatOfFormation { get; set; }
        public double entropyOfFormation { get; set; }

    }
}
