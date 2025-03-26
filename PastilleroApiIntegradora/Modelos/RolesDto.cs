using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class RolesDto
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual UsuariosDto Usuarios { get; set; }
    }
}