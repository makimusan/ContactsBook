using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ICloneService<T>
    {
        T CloneObject(T clonableObject);

        IList<T> CloneCollection(IList<T> clonableCollection);
    }
}
