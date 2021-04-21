using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace ServiciosMedicamento.Seguridad
{
    public class Autenticacion : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("El usuario no puede ser nulo o vacio");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("El password no puede ser nulo o vacio");
            }
            if (!(userName.ToLower().Equals("bryan")) &&(password.ToLower().Equals("1234")))
            {
                throw new FaultException("Usuario o contraseña incorrectos");
            }
        }
    }
}