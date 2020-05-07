using System.Collections.Generic;

namespace Infrastructure.Services
{
    public abstract class AbstactCloneService<T> : ICloneService<T>
    {
        public virtual IList<T> CloneCollection(IList<T> clonableCollection)
        {
            return new List<T>();
        }

        public virtual T CloneObject(T clonableObject)
        {
            return (T)new object();
        }
    }
}
