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
    
    public partial class tblInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblInvoice()
        {
            this.tblClient_transaction = new HashSet<tblClient_transaction>();
        }
    
        public int ID { get; set; }
        public int Invoice_no { get; set; }
        public int Added_service_id { get; set; }
        public int Payment_status { get; set; }
        public int Payment_type { get; set; }
    
        public virtual tblAdded_service tblAdded_service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblClient_transaction> tblClient_transaction { get; set; }
        public virtual tblCompany_services tblCompany_services { get; set; }
    }
}
