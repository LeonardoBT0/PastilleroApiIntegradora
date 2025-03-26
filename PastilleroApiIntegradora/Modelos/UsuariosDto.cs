using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class UsuariosDto
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int IdRol { get; set; }
        public virtual PersonasDto Personas { get; set; }
        public virtual RolesDto Roles { get; set; }
    }
}