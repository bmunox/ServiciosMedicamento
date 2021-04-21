using ServiciosMedicamento.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ServiciosMedicamento.Models;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;

namespace ServiciosMedicamento
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Medicamentos : IMedicamentos
    {
        int IMedicamentos.eliminarMedicamento(int iidMedicamento)
        {
            int rpta = 0;
            try
            {
                using (var bd = new MedicoEntities())
                {
                    Medicamento oMedicamento = bd.Medicamento.Where(p => p.IIDMEDICAMENTO == iidMedicamento).First();
                    oMedicamento.BHABILITADO = 0;
                    bd.SaveChanges();
                    rpta = 1;
                }
            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;
        }

        List<FormaFarmaceuticaCLS> IMedicamentos.listarFormaFarmaceutica()
        {
            List<FormaFarmaceuticaCLS> oFormaFarmaceutica = new List<FormaFarmaceuticaCLS>();

            try
            {
                using (var bd =  new MedicoEntities())
                {
                    oFormaFarmaceutica = bd.FormaFarmaceutica.Select(p => new FormaFarmaceuticaCLS
                    { 
                        iidformafarmaceutica = p.IIDFORMAFARMACEUTICA,
                        nombreformafarmaceutica = p.NOMBRE
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                oFormaFarmaceutica = null;
            }
            return oFormaFarmaceutica;
        }

        List<MedicamentoCLS> IMedicamentos.listarMedicamentos()
        {
            List<MedicamentoCLS> oMedicamentos = new List<MedicamentoCLS>();
            try
            {
                using (var bd = new MedicoEntities())
                {
                    oMedicamentos = (from Medicamento in bd.Medicamento
                                     join FormaFarmaceutica in bd.FormaFarmaceutica
                                     on Medicamento.IIDFORMAFARMACEUTICA equals FormaFarmaceutica.IIDFORMAFARMACEUTICA
                                     select new MedicamentoCLS
                                     {
                                         iidmedicamento = Medicamento.IIDMEDICAMENTO,
                                         nombre = Medicamento.NOMBRE,
                                         precio = (decimal)Medicamento.PRECIO,
                                         nombreformafarmaceutica = FormaFarmaceutica.NOMBRE,
                                         concentracion = Medicamento.CONCENTRACION,
                                         presentacion = Medicamento.PRESENTACION,
                                         stock = (int)Medicamento.STOCK,
                                         bhabilitado = (int)Medicamento.BHABILITADO
                                     }).ToList();
                }
            }
            catch (Exception ex)
            {
                new MessageWriteToFile(ex).WriteToFile();
                oMedicamentos = null;
            }
            return oMedicamentos;
        }

        MedicamentoCLS IMedicamentos.recuperarMedicamento(int iidMedicamento)
        {
            MedicamentoCLS omedicamentoCLS = new MedicamentoCLS();
            try
            {
                using (var bd = new MedicoEntities())
                {
                    //forma 1(puede hacerlo como le de la gana)
                    /*
                    omedicamentoCLS = (from Medicamento in bd.Medicamento
                                     select new MedicamentoCLS
                                     {
                                         iidmedicamento = Medicamento.IIDMEDICAMENTO,
                                         iidformafarmaceutica = (int)Medicamento.IIDFORMAFARMACEUTICA,
                                         nombre = Medicamento.NOMBRE,
                                         precio = (decimal)Medicamento.PRECIO,
                                         stock = (int)Medicamento.STOCK,
                                         concentracion = Medicamento.CONCENTRACION,
                                         presentacion = Medicamento.PRESENTACION,
                                     }).Where(p => p.iidmedicamento == iidMedicamento).First();
                    */
                    //forma 2(puede hacerlo como le de la gana)
                    Medicamento omedicamento = bd.Medicamento.Where(p => p.IIDMEDICAMENTO == iidMedicamento).First();
                    omedicamentoCLS.iidmedicamento = omedicamento.IIDMEDICAMENTO;
                    omedicamentoCLS.iidformafarmaceutica = (int)omedicamento.IIDFORMAFARMACEUTICA ;
                    omedicamentoCLS.nombre = omedicamento.NOMBRE ;
                    omedicamentoCLS.precio = (decimal)omedicamento.PRECIO ;
                    omedicamentoCLS.stock = (int)omedicamento.STOCK ;
                    omedicamentoCLS.concentracion = omedicamento.CONCENTRACION ;
                    omedicamentoCLS.presentacion = omedicamento.PRESENTACION;

                }
            }
            catch (Exception ex )
            {
                omedicamentoCLS = null;
            }
            return omedicamentoCLS;
        }

        int IMedicamentos.AgregarEditarMedicamento(MedicamentoCLS oMedicamentoCLS)
        {
            int rpta = 0;
            try
            {
                using (var bd = new MedicoEntities())
                {
                    //Registrar
                    if (oMedicamentoCLS.iidmedicamento == 0)
                    {
                        Medicamento omedicamento = new Medicamento();
                        omedicamento.IIDMEDICAMENTO = oMedicamentoCLS.iidmedicamento;
                        omedicamento.IIDFORMAFARMACEUTICA = oMedicamentoCLS.iidformafarmaceutica;
                        omedicamento.NOMBRE = oMedicamentoCLS.nombre;
                        omedicamento.PRECIO = oMedicamentoCLS.precio;
                        omedicamento.STOCK = oMedicamentoCLS.stock;
                        omedicamento.PRESENTACION = oMedicamentoCLS.presentacion;
                        omedicamento.CONCENTRACION = oMedicamentoCLS.concentracion;
                        omedicamento.BHABILITADO = 1;
                        bd.Medicamento.Add(omedicamento);
                        bd.SaveChanges();
                        rpta = 1;
                    }
                    //Editar
                    else 
                    {
                        Medicamento omedicamento = bd.Medicamento.Where(p => p.IIDMEDICAMENTO == oMedicamentoCLS.iidmedicamento).First();

                        omedicamento.IIDMEDICAMENTO = oMedicamentoCLS.iidmedicamento;
                        omedicamento.IIDFORMAFARMACEUTICA = (int)oMedicamentoCLS.iidformafarmaceutica;
                        omedicamento.NOMBRE = oMedicamentoCLS.nombre;
                        omedicamento.PRECIO = oMedicamentoCLS.precio;
                        omedicamento.STOCK = oMedicamentoCLS.stock;
                        omedicamento.CONCENTRACION = oMedicamentoCLS.concentracion;
                        omedicamento.PRESENTACION = oMedicamentoCLS.presentacion;
                        bd.SaveChanges();
                        rpta = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;
        }
    }
    public class MessageWriteToFile
    {
        private const string Directory = "C:\\AppLogs";
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public string DefaultPath
        {
            get
            {
                var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                var folder = $"{Directory}\\{appName}";

                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }

                var fileName = $"{DateTime.Today:yyyy-MM-dd}.txt";
                return $"{Directory}\\{appName}\\{fileName}";
            }
        }

        public MessageWriteToFile(string message)
        {
            Message = message;
        }

        public MessageWriteToFile(Exception ex)
        {
            Exception = ex;
        }

        public bool WriteToFile(string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                path = DefaultPath;
            }

            try
            {
                using (var writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    writer.WriteLine();

                    if (Exception != null)
                    {
                        writer.WriteLine(Exception.GetType().FullName);
                        writer.WriteLine("Source : " + Exception.Source);
                        writer.WriteLine("Message : " + Exception.Message);
                        writer.WriteLine("StackTrace : " + Exception.StackTrace);
                        writer.WriteLine("InnerException : " + Exception.InnerException?.Message);
                    }

                    if (!string.IsNullOrEmpty(Message))
                    {
                        writer.WriteLine(Message);
                    }

                    writer.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
