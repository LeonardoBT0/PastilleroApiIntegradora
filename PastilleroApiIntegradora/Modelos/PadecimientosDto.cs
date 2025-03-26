using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class PadecimientosDto
	{
        public int Id { get; set; }
        public string TipoPadecimiento { get; set; }
        public string Descripcion { get; set; }

        public virtual HistorialDto Historial { get; set; }
    }
}