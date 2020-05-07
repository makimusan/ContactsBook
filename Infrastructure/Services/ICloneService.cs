using System.Collections.Generic;

namespace Infrastructure.Services
{
    public interface ICloneService<T>
    {
        T CloneObject(T clonableObject);

        IList<T> CloneCollection(IList<T> clonableCollection);
    }
}
