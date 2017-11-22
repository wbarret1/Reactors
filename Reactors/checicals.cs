using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    class checicals
    {
    }


    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public int id { get; set; }
        public string casNo { get; set; }
        public string accessionNo { get; set; }
        public string pmnNo { get; set; }
        public string epaId { get; set; }
        public Synonym[] synonyms { get; set; }
    }

    public class Synonym
    {
        public int id { get; set; }
        public string chemicalName { get; set; }
        public bool isIupac { get; set; }
        public bool isTscaInv { get; set; }
        public bool isRegistry { get; set; }
        public bool isSystematic { get; set; }
        public bool isUnregistered { get; set; }
        public bool isWorkPlan { get; set; }
        public bool isTsca12B { get; set; }
        public int sortOrder { get; set; }
    }

}
