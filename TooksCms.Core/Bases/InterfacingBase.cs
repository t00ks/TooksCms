using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using System.Reflection;

namespace TooksCms.Core.Bases
{
    public abstract class InterfacingBase
    {
        public InterfacingBase() { }

        public InterfacingBase(IInterfacingBase data, Type interfaceType)
        {
            PropertyInfo[] dataProperties = interfaceType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo[] thisProperties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            
            foreach (PropertyInfo info in dataProperties)
            {
                object value = info.GetValue(data, null);
                if (value != null) { info.SetValue(this, value, null); }
            }
        }
    }
}
