using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBook.Helpers.Interfaces
{
    public interface IClosable
    {
        void Close();

        bool? DialogResult { get; set; }
    }
}
