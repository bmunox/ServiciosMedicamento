//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiciosMedicamento.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Clinica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clinica()
        {
            this.Doctor = new HashSet<Doctor>();
        }
    
        public int IIDCLINICA { get; set; }
        public string NOMBRE { get; set; }
        public string DIRECCION { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
