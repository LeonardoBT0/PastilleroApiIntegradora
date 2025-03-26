using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class HistorialDto
	{
        public int Id { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFinal { get; set; }
        public int IdPadecimiento { get; set; }
        public int IdTratamiento { get; set; }
        public virtual FichaTecnicaDto FichaTecnica { get; set; }
        public virtual PadecimientosDto Padecimientos { get; set; }
        public virtual TratamientoDto Tratamiento { get; set; }
    }
}