using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class DireccionesDto
	{
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public int CodigoPostal { get; set; }
        public string NumExterior { get; set; }
        public string NumInterior { get; set; }
    }
}