//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client_management.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public partial class tblClient_profile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblClient_profile()
        {
            this.tblAdded_service = new HashSet<tblAdded_service>();
        }
    
        public int ID { get; set; }
        public string Company_name { get; set; }
        public string Password { get; set; }
        public string Logo { get; set; }
        public string Company_email { get; set; }
        public string Contact_person_name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public int Cell_1 { get; set; }
        public Nullable<int> cell_2 { get; set; }
        public Nullable<int> cell_3 { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAdded_service> tblAdded_service { get; set; }

       public HttpPostedFileBase Imagefile { get; set; }
    }
}
