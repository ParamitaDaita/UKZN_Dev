//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WroseModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Permission_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_Permission_Master()
        {
            this.User_Role_Permission = new HashSet<User_Role_Permission>();
        }
    
        public int ID { get; set; }
        public string Permimssion_Name { get; set; }
        public string page_URL { get; set; }
        public byte Active { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Role_Permission> User_Role_Permission { get; set; }
    }
}
