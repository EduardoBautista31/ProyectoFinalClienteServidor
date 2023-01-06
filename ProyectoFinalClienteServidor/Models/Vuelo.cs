using System;
using System.Collections.Generic;

namespace ProyectoFinalClienteServidor.Models
{
    public partial class Vuelo
    {
        public int IdVuelo { get; set; }
        public TimeOnly Hora { get; set; }
        public string Salida { get; set; } = null!;
        public TimeOnly HoraLlegada { get; set; }
        public string CodigoVuelo { get; set; } = null!;
        public int IdEstado { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; } = null!;
    }
}
