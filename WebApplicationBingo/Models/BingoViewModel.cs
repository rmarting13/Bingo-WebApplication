using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplicationBingo.Models
{
    public class BingoViewModel
    {
        public List<CartonViewModel>? Cartones { get; set; }
        public DateTime Fecha { get; set; }
        [NotMapped]
        public BolilleroViewModel? Lanzador { get; set; }

    }
}