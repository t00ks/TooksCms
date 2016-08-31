using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Bases
{
    [Serializable]
    public class CollectionBase<T> : List<T> where T : ModelBase
    {
        #region Constructors

        public CollectionBase(IEnumerable<T> list)
            : base(list)
        {

        }

        public CollectionBase() { }

        #endregion

        #region Find

        public T Find(int id)
        {
            return (from ModelBase child in this where child.Id == id select child as T).FirstOrDefault();
        }

        public T Find(Guid uid)
        {
            return (from ModelBase child in this where child.Uid == uid select child as T).FirstOrDefault();
        }

        #endregion

        #region Parent/Child Support

        public virtual bool IsDirty
        {
            get { return this.Cast<ModelBase>().Any(child => child.IsDirty | child.IsNew | child.IsDeleted); }
        }

        #endregion
    }
}
