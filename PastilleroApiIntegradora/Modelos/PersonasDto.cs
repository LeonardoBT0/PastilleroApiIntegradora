using PastilleroApiIntegradora.Controllers;
using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class PersonasDto
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        public byte[] FotoPerfil { get; set; }
        public int IdDirecciones { get; set; }
        public int IdUsuarios { get; set; }
        public int IdFichaTecnica { get; set; }
        public virtual DireccionesDto Direcciones { get; set; }
        public virtual FichaTecnicaDto FichaTecnica { get; set; }
        public virtual UsuariosDto Usuarios { get; set; }
    }
}