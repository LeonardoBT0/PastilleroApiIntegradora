using PastilleroApiIntegradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastilleroApiIntegradora.Modelos
{
	public class ControlesDto
	{
        public int Id { get; set; }
        public TimeSpan Horarios { get; set; }
        public virtual TratamientoDto Tratamiento { get; set; }

    }
}