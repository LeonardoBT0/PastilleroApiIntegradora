using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class TratamientoDto
	{
        public int Id { get; set; }
        public string TipoTratamiento { get; set; }
        public string Descripcion { get; set; }
        public int IdMedicamento { get; set; }
        public int IdControl { get; set; }
        public virtual ControlesDto Control { get; set; }
        public virtual HistorialDto Historial { get; set; }
        public virtual MedicamentoDto Medicamentos { get; set; }
    }
}