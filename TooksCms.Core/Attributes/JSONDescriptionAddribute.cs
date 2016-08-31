using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooksCms.Core.Attributes
{
    public class JSONDescriptionAttribute : Attribute
    {
        public static readonly JSONDescriptionAttribute Default = new JSONDescriptionAttribute("");

        public JSONDescriptionAttribute() { DescriptionValue = ""; }
        public JSONDescriptionAttribute(string description) { DescriptionValue = description; }

        public virtual string Description
        {
            get
            {
                return DescriptionValue;
            }
        }

        protected string DescriptionValue { get; set; }

        public override bool Equals(object obj)
        {
            return ((JSONDescriptionAttribute)obj).Description == this.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return this.Equals(JSONDescriptionAttribute.Default);
        }
    }
}
