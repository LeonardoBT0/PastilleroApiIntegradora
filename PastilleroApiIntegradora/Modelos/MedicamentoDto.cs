using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class MedicamentoDto
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual TratamientoDto Tratamiento { get; set; }
    }
}