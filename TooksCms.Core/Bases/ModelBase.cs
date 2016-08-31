using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Xml.Serialization;

namespace TooksCms.Core.Bases
{
    [Serializable]
    public abstract class ModelBase : ICloneable
    {
        #region Status Tracking

        protected int _id = -1;
        protected Guid _uid = Guid.NewGuid();

        protected bool _isNew = true;
        protected bool _isDirty = false;
        protected bool _isDeleted = false;

        public void MarkOld() { _isNew = false; _isDirty = false; }
        public void MarkNew() { _isNew = true; }
        public void MarkDirty() { _isDirty = true; }
        public void MarkDeleted() { _isDeleted = true; }
        public void UnDelete() { _isDeleted = false; }

        [XmlAttribute("id")]
        public int Id { get { return _id; } set { _id = value; } }
        [XmlAttribute("Uid")]
        public Guid Uid { get { return _uid; } set { _uid = value; } }
        public virtual bool IsDirty { get { return _isDirty; } }
        public virtual bool IsNew { get { return _isNew; } }
        public virtual bool IsDeleted { get { return _isDeleted; } }

        #endregion

        #region IClonable Members

        public object Clone()
        {
            using (MemoryStream buffer = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(buffer, this);
                buffer.Position = 0;
                object temp = formatter.Deserialize(buffer);
                return temp;
            }
        }

        #endregion

        #region Xml Serialisation

        public string Serialize()
        {
            XmlSerializer xsr = new XmlSerializer(GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            xsr.Serialize(tw, this);
            return sb.ToString();
        }

        public ModelBase Desrialize(string xml)
        {
            XmlSerializer xsr = new XmlSerializer(GetType());
            StringReader sr = new StringReader(xml);
            object obj = xsr.Deserialize(sr);
            ((ModelBase)obj).MarkOld();
            return (ModelBase)obj;
        }

        #endregion

        #region Parent/Child Support

        internal virtual void Save(ModelBase parent)
        {
            throw new NotImplementedException("You must override Save method for parent support");
        }

        #endregion

        #region Property Methods

        public virtual void SetPropertyValue(string propertyName, object propertyValue)
        {
            Type type = this.GetType();
            PropertyInfo info = type.GetProperty(propertyName);
            info.SetValue(this, Convert.ChangeType(propertyValue, info.PropertyType), null);
        }

        public virtual object GetPropertyValue(string propertyName)
        {
            Type type = this.GetType();
            PropertyInfo info = type.GetProperty(propertyName);
            return info.GetValue(this, null);
        }

        #endregion

        #region JSON

        public virtual object GetJSONModel() { return this; }

        #endregion
    }
}
