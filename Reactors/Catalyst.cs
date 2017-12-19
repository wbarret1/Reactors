using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    public class Catalyst : ReactionParticipant
    {
        public Catalyst(Chemical chemical) : base(chemical)
        {
        }
    }
}
