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
    
    public partial class tblCompany_services
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCompany_services()
        {
            this.tblAdded_service = new HashSet<tblAdded_service>();
        }
    
        public int ID { get; set; }
        public string C_service_name { get; set; }
        public int Category { get; set; }
        public int Price { get; set; }
        public int Subscription_fee { get; set; }
        public string Details { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAdded_service> tblAdded_service { get; set; }
    }
}
