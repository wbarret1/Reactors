using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    public class ReactionParticipant
    {
        Chemical m_Chemical;

        public ReactionParticipant(Chemical chemical)
        {
            m_Chemical = chemical;
        }

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
    }
}
