namespace ProyectoFinalClienteServidor.DTOS
{
    public class VueloDTO
    {
        public int IdVuelo { get; set; }
        public TimeSpan Hora { get; set; }
        public string Salida { get; set; } = null!;
        public TimeSpan HoraLlegada { get; set; }
        public string CodigoVuelo { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}
