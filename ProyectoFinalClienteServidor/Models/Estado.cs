using System;
using System.Collections.Generic;

namespace ProyectoFinalClienteServidor.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Vuelos = new HashSet<Vuelo>();
        }

        public int IdEstado { get; set; }
        public string Estado1 { get; set; } = null!;

        public virtual ICollection<Vuelo> Vuelos { get; set; }
    }
}
