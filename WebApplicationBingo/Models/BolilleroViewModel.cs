
using System.Numerics;

namespace WebApplicationBingo.Models
{
    public class BolilleroViewModel
    {
        public List<int> BolillasObtenidas { get; set; }
        private Random _lanzador;
        
        public BolilleroViewModel()
        {
            BolillasObtenidas = new List<int>();
            _lanzador = new Random();
        }

        public int ObtenerBolilla()
        {

            int bolilla = _lanzador.Next(1, 90);
            while (BolillasObtenidas.Contains(bolilla)){
                bolilla = _lanzador.Next(1, 90);
            }
            BolillasObtenidas.Add(bolilla);
            return bolilla;
        }
    }
}
