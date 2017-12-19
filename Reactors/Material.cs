using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    public delegate void MaterialEventHandler(object sender, MaterialEventArgs e);

    public class MaterialEventArgs : EventArgs
    {
        public MaterialEventArgs(string s)
        {
            msg = s;
        }
        private string msg;
        public string Message
        {
            get { return msg; }
        }
    }

    internal class MaterialConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is Material)
            {
                Material mat = (Material)value;
                return mat.Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class BasisConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context)
        {
            // note you can also look at context etc to build list
            return new StandardValuesCollection(new string[] { "mass", "mole" });
        }
    }

    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(MaterialConverter))]
    public class Material
    {

        List<Chemical> chemicals;
        ComponentCollection components;

        public Material()
        {
            chemicals = new List<Chemical>();
            components = new ComponentCollection();
            Name = string.Empty;
            Description = string.Empty;
            m_basis = "mass";
        }

        public Material(string name = "", string description = "")
        {
            chemicals = new List<Chemical>();
            components = new ComponentCollection();
            Name = name;
            Description = description;
            m_basis = "mass";
        }

        public event MaterialEventHandler RaiseMaterialChangedEvent;

        public virtual void OnRaiseMaterialChangedEvent(MaterialEventArgs e)
        {
            RaiseMaterialChangedEvent?.Invoke(this, e);
        }

        [System.ComponentModel.Category("Components")]
        public ComponentCollection Components
        {
            get
            {
                return components;
            }            
        }

        [System.ComponentModel.Category("Identification")]
        public string Name { get; set; }
        [System.ComponentModel.Category("Identification")]
        public string Description { get; set; }
        string m_basis;
        [System.ComponentModel.Category("Basis")]
        [TypeConverter(typeof(BasisConverter))]
        public string Basis {
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

        double m_FlowRate;
        [System.ComponentModel.Category("Conditions")]
        public double FlowRate
        {
            get
            {
                return m_FlowRate;
            }
            set
            {
                foreach (Component c in this.Components)
                {
                    c.Flow = c.Concentration * value;
                    m_FlowRate = value;
                }
                OnRaiseMaterialChangedEvent(new MaterialEventArgs("Flow rate changed."));
            }
        }
        [System.ComponentModel.Category("Conditions")]
        public double Temperature { get; set; }
        [System.ComponentModel.Category("Conditions")]
        public double Pressure { get; set; }

        public void AddChemical(Chemical chem, double concentration = 0.0)
        {
            chemicals.Add(chem);
            Component comp = new Component
            {
                Name = chem.Name,
                Concentration = concentration
            };
            this.Components.Add(comp);
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
                double[] retVal = new double[this.Components.Count];
                for (int i = 0; i < this.Components.Count; i++)
                {
                    retVal[i] = this.Components[i].Concentration;
                }
                return retVal;
            }
            set
            {
                FlowRate = 0;
                for (int i = 0; i < this.Components.Count; i++)
                {
                    this.Components[i].Concentration = value[i];
                    this.Components[i].Flow = value[i] * this.FlowRate;
                }
                OnRaiseMaterialChangedEvent(new MaterialEventArgs("Concentrations changed."));
            }
        }

        [System.ComponentModel.Browsable(false)]
        public double[] Flows
        {
            get
            {
                double[] retVal = new double[this.Components.Count];
                for (int i = 0; i < this.Components.Count; i++)
                {
                    retVal[i] = this.Components[i].Flow;
                }
                return retVal;
            }
            set
            {
                FlowRate = 0;
                foreach (double flow in value) FlowRate = FlowRate + flow;
                for (int i = 0; i < this.Components.Count; i++)
                {
                    this.Components[i].Flow = value[i];
                    this.Components[i].Concentration = value[i] / FlowRate;
                }
                OnRaiseMaterialChangedEvent(new MaterialEventArgs("Flow rate changed."));
            }
        }

        public void SetConcentration(string chemical, double concentration)
        {
            foreach(Component c in this.Components)
            {
                if (c.Name == chemical) c.Concentration = concentration;
            }
            OnRaiseMaterialChangedEvent(new MaterialEventArgs("Concentrations changed."));
        }

        public double GetConcetration(string chemical)
        {
            foreach (Component c in this.Components)
            {
                if (c.Name == chemical) return c.Concentration;
            }
            return double.NaN;
        }

        //public System.ComponentModel.AttributeCollection GetAttributes()
        //{
        //    return System.ComponentModel.TypeDescriptor.GetAttributes(this, true);
        //}

        //public string GetClassName()
        //{
        //    return System.ComponentModel.TypeDescriptor.GetClassName(this, true);
        //}

        //public string GetComponentName()
        //{
        //    return System.ComponentModel.TypeDescriptor.GetComponentName(this, true);
        //}

        //public System.ComponentModel.TypeConverter GetConverter()
        //{
        //    return System.ComponentModel.TypeDescriptor.GetConverter(this, true);
        //}

        //public System.ComponentModel.EventDescriptor GetDefaultEvent()
        //{
        //    return System.ComponentModel.TypeDescriptor.GetDefaultEvent(this, true);
        //}

        //public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
        //{
        //    return System.ComponentModel.TypeDescriptor.GetDefaultProperty(this, true);
        //}

        //public object GetEditor(Type editorBaseType)
        //{
        //    return System.ComponentModel.TypeDescriptor.GetEditor(this, editorBaseType, true);
        //}

        //public System.ComponentModel.EventDescriptorCollection GetEvents()
        //{
        //    return System.ComponentModel.TypeDescriptor.GetEvents(this, true);
        //}

        //public System.ComponentModel.EventDescriptorCollection GetEvents(Attribute[] attributes)
        //{
        //    return System.ComponentModel.TypeDescriptor.GetEvents(this, attributes, true);
        //}

        //public System.ComponentModel.PropertyDescriptorCollection GetProperties()
        //{
        //    // Create a new collection object PropertyDescriptorCollection
        //    System.ComponentModel.PropertyDescriptorCollection pds = new System.ComponentModel.PropertyDescriptorCollection(null);

        //    // Iterate the list of components
        //    for (int i = 0; i < this.Components.Count; i++)
        //    {
        //        // For each component create a property descriptor 
        //        // and add it to the 
        //        // PropertyDescriptorCollection instance
        //        MaterialPropertyDescriptor pd = new MaterialPropertyDescriptor(this, i);
        //        pds.Add(pd);
        //    }
        //    //pds.Add(new DoublePropertyDescriptor(this, "FlowRate"));
        //    //pds.Add(new DoublePropertyDescriptor(this, "Pressure"));
        //    //pds.Add(new DoublePropertyDescriptor(this, "Temperature"));
        //    return pds;
        //}

        //public System.ComponentModel.PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        //{
        //    return this.GetProperties();
        //}

        //public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        //{
        //    return this;
        //}
    }

    //public class DoublePropertyDescriptor : System.ComponentModel.PropertyDescriptor
    //{
    //    Object obj;
    //    string propName;

    //    public DoublePropertyDescriptor(Object objName, string name): base(name, null)
    //    {
    //        obj = objName;
    //        propName = name;
    //    }

    //    public override Type ComponentType
    //    {
    //        get
    //        {
    //            return obj.GetType();
    //        }
    //    }

    //    public override bool IsReadOnly
    //    {
    //    get
    //        {
    //            return false;
    //        }
    //    }


    //    public override Type PropertyType
    //    {
    //        get
    //        {
    //            return typeof(double);
    //        }
    //    }

    //    public override bool CanResetValue(object component)
    //    {
    //        return false;
    //    }

    //    public override object GetValue(object component)
    //    {
    //        var value = obj.GetType().GetProperty(propName).GetValue(obj, null);
    //        return value;
    //    }

    //    public override void ResetValue(object component)
    //    {
    //    }

    //    public override void SetValue(object component, object value)
    //    {
    //        obj.GetType().GetProperty(propName).SetValue(obj, (double)value, null);
    //    }

    //    public override bool ShouldSerializeValue(object component)
    //    {
    //        return true; ;
    //    }
    //}

    //internal class MaterialPropertyDescriptor : System.ComponentModel.PropertyDescriptor
    //{
    //    private Material collection = null;
    //    private int index = -1;

    //    public MaterialPropertyDescriptor(Material  coll, int idx) : base("#" + idx.ToString(), null)
    //    {
    //        this.collection = coll;
    //        this.index = idx;
    //    }

    //    public override System.ComponentModel.AttributeCollection Attributes
    //    {
    //        get
    //        {
    //            return new System.ComponentModel.AttributeCollection(null);
    //        }
    //    }

    //    public override bool CanResetValue(object component)
    //    {
    //        return true;
    //    }

    //    public override Type ComponentType
    //    {
    //        get
    //        {
    //            return this.collection.GetType();
    //        }
    //    }

    //    public override string DisplayName
    //    {
    //        get
    //        {
    //            Component comp = this.collection.Components[index];
    //            return comp.Name;
    //        }
    //    }

    //    public override string Description
    //    {
    //        get
    //        {
    //            Component comp = this.collection.Components[index];
    //            //StringBuilder sb = new StringBuilder();
    //            //sb.Append(emp.LastName);
    //            //sb.Append(",");
    //            //sb.Append(emp.FirstName);
    //            //sb.Append(",");
    //            //sb.Append(emp.Age);
    //            //sb.Append(" years old, working for ");
    //            //sb.Append(emp.Department);
    //            //sb.Append(" as ");
    //            //sb.Append(emp.Role);

    //            return comp.Name;
    //        }
    //    }

    //    public override object GetValue(object component)
    //    {
    //        return "Concentration = " + this.collection.Components[index].Concentration;
    //    }

    //    public override bool IsReadOnly
    //    {
    //        get { return true; }
    //    }

    //    public override string Name
    //    {
    //        get { return "#" + index.ToString(); }
    //    }

    //    public override Type PropertyType
    //    {
    //        get { return this.collection.Components[index].GetType(); }
    //    }

    //    public override void ResetValue(object component) { }

    //    public override bool ShouldSerializeValue(object component)
    //    {
    //        return true;
    //    }

    //    public override void SetValue(object component, object value)
    //    {
    //        // this.collection[index] = value;
    //    }
    //}
}
