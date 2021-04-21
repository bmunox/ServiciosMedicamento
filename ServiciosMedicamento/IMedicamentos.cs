using ServiciosMedicamento.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosMedicamento
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMedicamentos
    {
        // TODO: Add your service operations here
        //Listado de medicamentos
        //Lista medicamentos
        [OperationContract]
        List<MedicamentoCLS> listarMedicamentos();
        //Lista forma facrmaceutica
        [OperationContract]
        List<FormaFarmaceuticaCLS> listarFormaFarmaceutica();
        //Recuperar Medicamento
        [OperationContract]
        MedicamentoCLS recuperarMedicamento(int iidMedicamento);
        //Agregar y editar medicamento
        [OperationContract]
        int AgregarEditarMedicamento(MedicamentoCLS oMedicamento);
        //eliminar medicamento
        [OperationContract]
        int eliminarMedicamento(int iidMedicamento);
    }
}
