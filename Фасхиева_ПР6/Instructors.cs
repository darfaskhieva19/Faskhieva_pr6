//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Фасхиева_ПР6
{
    using System;
    using System.Collections.Generic;
    
    public partial class Instructors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Instructors()
        {
            this.Training = new HashSet<Training>();
        }
    
        public int idInstruct { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronimyc { get; set; }
        public string phone { get; set; }
        public int idCategory { get; set; }
        public int idEducation { get; set; }
        public int idPost { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Education Education { get; set; }
        public virtual Posts Posts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Training> Training { get; set; }
    }
}
