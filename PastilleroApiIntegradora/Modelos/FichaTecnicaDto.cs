using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class FichaTecnicaDto
	{
        public int Id { get; set; }
        public string TipoSangre { get; set; }
        public double Estatura { get; set; }
        public int IdHistorial { get; set; }
        public virtual HistorialDto Historial { get; set; }
        public virtual PersonasDto Personas { get; set; }
    }
}