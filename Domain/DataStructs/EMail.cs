//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactsBook.Domain.DataStructs
{
    using System;
    using System.Collections.Generic;
    
    public partial class EMail
    {
        public long ID { get; set; }
        public string EMailAddress { get; set; }
        public long ContactID { get; set; }
    
        public virtual Contact Contact { get; set; }
    }
}