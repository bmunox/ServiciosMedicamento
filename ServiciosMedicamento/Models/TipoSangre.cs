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
    
    public partial class TipoSangre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoSangre()
        {
            this.Paciente = new HashSet<Paciente>();
        }
    
        public int IIDTIPOSANGRE { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paciente> Paciente { get; set; }
    }
}
