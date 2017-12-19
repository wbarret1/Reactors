using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactors
{
    internal class ChemicalCollectionConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is ChemicalCollection)
            {
                ChemicalCollection chemColl = (ChemicalCollection)value;
                return chemColl.Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    static class Chemicals
    {
        static ChemicalCollection chemicals;

        static Chemicals()
        {
            chemicals = new ChemicalCollection();
        }

        public static void Add(Chemical chem)
        {
            chemicals.Add(chem);
        }

        public static Chemical[] AllChemcials
        {
            get
            {
                return chemicals.ToArray();
            }
        }
    }

    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(ChemicalCollectionConverter))]
    public class ChemicalCollection : System.Collections.Generic.List<Chemical>, System.ComponentModel.ICustomTypeDescriptor
    {
        public ChemicalCollection()
        {

        }
   
        public string Name { get; set; }

        //public new void Add(Chemical chem)
        //{
        //    Chemicals.Add(chem);
        //    base.Add(chem);
        //}

        //public void Add(Material c)
        //{
        //    this.List.Add(c);
        //}

        //public Material this[int index]
        //{
        //    get
        //    {
        //        return (Material)this.List[index];
        //    }
        //}

        public System.ComponentModel.AttributeCollection GetAttributes()
        {
            return System.ComponentModel.TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return System.ComponentModel.TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return System.ComponentModel.TypeDescriptor.GetComponentName(this, true);
        }

        public System.ComponentModel.TypeConverter GetConverter()
        {
            return System.ComponentModel.TypeDescriptor.GetConverter(this, true);
        }

        public System.ComponentModel.EventDescriptor GetDefaultEvent()
        {
            return System.ComponentModel.TypeDescriptor.GetDefaultEvent(this, true);
        }

        public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
        {
            return System.ComponentModel.TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return System.ComponentModel.TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public System.ComponentModel.EventDescriptorCollection GetEvents()
        {
            return System.ComponentModel.TypeDescriptor.GetEvents(this, true);
        }

        public System.ComponentModel.EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return System.ComponentModel.TypeDescriptor.GetEvents(this, attributes, true);
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object PropertyDescriptorCollection
            System.ComponentModel.PropertyDescriptorCollection pds = new System.ComponentModel.PropertyDescriptorCollection(null);

            // Iterate the list of employees
            for (int i = 0; i < this.Count; i++)
            {
                // For each employee create a property descriptor 
                // and add it to the 
                // PropertyDescriptorCollection instance
                ChemicalCollectionPropertyDescriptor pd = new ChemicalCollectionPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            return pds;
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return this.GetProperties();
        }

        public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            return this;
        }
    }

    public class ChemicalCollectionPropertyDescriptor : System.ComponentModel.PropertyDescriptor
    {
        private ChemicalCollection collection = null;
        private int index = -1;

        public ChemicalCollectionPropertyDescriptor(ChemicalCollection coll, int idx) : base("#" + idx.ToString(), null)
        {
            this.collection = coll;
            this.index = idx;
        }

        public override System.ComponentModel.AttributeCollection Attributes
        {
            get
            {
                return new System.ComponentModel.AttributeCollection(null);
            }
        }

        public override bool CanResetValue(object Material)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return this.collection.GetType();
            }
        }

        public override string DisplayName
        {
            get
            {
                Chemical chem = this.collection[index];
                return chem.Name;
            }
        }

        public override string Description
        {
            get
            {
                Chemical chem = this.collection[index];
                //StringBuilder sb = new StringBuilder();
                //sb.Append(emp.LastName);
                //sb.Append(",");
                //sb.Append(emp.FirstName);
                //sb.Append(",");
                //sb.Append(emp.Age);
                //sb.Append(" years old, working for ");
                //sb.Append(emp.Department);
                //sb.Append(" as ");
                //sb.Append(emp.Role);

                return chem.Name;
            }
        }

        public override object GetValue(object Material)
        {
            return this.collection[index];
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override string Name
        {
            get { return "#" + index.ToString(); }
        }

        public override Type PropertyType
        {
            get { return this.collection[index].GetType(); }
        }

        public override void ResetValue(object Material) { }

        public override bool ShouldSerializeValue(object Material)
        {
            return true;
        }

        public override void SetValue(object Material, object value)
        {
            // this.collection[index] = value;
        }
    }
}
