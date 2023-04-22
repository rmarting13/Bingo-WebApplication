using WebApplicationBingo.Data;

namespace WebApplicationBingo.Models
{
    public class HistorialCartonesViewModel : EntidadBase
    {
        public DateTime? Fecha { get; set; }
        public string? Carton1 { get; set; }
        public string? Carton2 { get; set; }
        public string? Carton3 { get; set; }
        public string? Carton4 { get; set; }

    }
}
