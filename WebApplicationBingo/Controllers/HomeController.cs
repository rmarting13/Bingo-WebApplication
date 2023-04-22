using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationBingo.Data.Context;
using WebApplicationBingo.Models;

namespace WebApplicationBingo.Controllers
{
    public class HomeController : Controller
    {
        private DataBaseContext? _ctx;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _ctx = new DataBaseContext();
        }

        public IActionResult Index()
        {

            var cartones = new List<CartonViewModel>();

            for (var i = 0; i < 4; i++)
                cartones.Add(new CartonViewModel(i+1));

            var model = new BingoViewModel()
            {
                Cartones = cartones,
                Fecha = DateTime.Now,
                Lanzador = new BolilleroViewModel(),
            };

            List<string> colores = new List<string>();

            for (int i = 1; i <= 90; i++)
            {
                colores.Add("bg-light");
            }
            ViewData["Lanzador"] = false;
            ViewData["Reset"] = "disabled";
            ViewBag.Colores = colores;
            ViewBag.Modelo = model;
            ViewBag.Bolilla = "Haga click en 'Lanzar Bolilla'";
            
            return View();
        }

        public IActionResult Historial()
        {
            var consultaHistorialCartones = _ctx.HistorialCartones.ToList();
            List<HistorialCartonesViewModel> cartones = new List<HistorialCartonesViewModel>();
            foreach(var datos in consultaHistorialCartones)
            {
                var cartonGanador = new HistorialCartonesViewModel()
                {
                    Id = datos.Id,
                    Fecha = datos.Fecha,
                    Carton1 = datos.Carton1,
                    Carton2 = datos.Carton2,
                    Carton3 = datos.Carton3,
                    Carton4 = datos.Carton4,
                };
                cartones.Add(cartonGanador);
            }

            var consultaHistorialBolillero = _ctx.HistorialBolillero.ToList();
            List<HistorialBolilleroViewModel> bolillas = new List<HistorialBolilleroViewModel>();
            foreach (var datos in consultaHistorialBolillero)
            {
                var bolilla = new HistorialBolilleroViewModel()
                {
                    Id = datos.Id,
                    Fecha = datos.Fecha,
                    NroBolilla = datos.NroBolilla,
                };
                bolillas.Add(bolilla);
            }

            ViewData["HistorialCartones"] = cartones;
            ViewData["HistorialBolillero"] = bolillas;
            
            return View();
        }

        public IActionResult Reset()
        {
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Launcher(List<string> colores, List<int> bolillasObtenidas, List<int> listaDeAciertos, List<int> carton1, List<int> carton2, List<int> carton3, List<int> carton4)
        {
            var cartones = new List<CartonViewModel>();

            cartones.Add(new CartonViewModel(1) { ListaDeNumeros = carton1, CantidadDeAciertos = listaDeAciertos[0] });
            cartones.Add(new CartonViewModel(2) { ListaDeNumeros = carton2, CantidadDeAciertos = listaDeAciertos[1] });
            cartones.Add(new CartonViewModel(3) { ListaDeNumeros = carton3, CantidadDeAciertos = listaDeAciertos[2] });
            cartones.Add(new CartonViewModel(4) { ListaDeNumeros = carton4, CantidadDeAciertos = listaDeAciertos[3] });

            var model = new BingoViewModel()
            {
                Cartones = cartones,
                Fecha = DateTime.Now,
                Lanzador = new BolilleroViewModel()
                {
                    BolillasObtenidas = bolillasObtenidas
                },
            };

            int bolilla = model.Lanzador.ObtenerBolilla();
            foreach (var carton in model.Cartones)
            {
                if (carton.ListaDeNumeros.Contains(bolilla))
                {
                    carton.CantidadDeAciertos += 1; 
                }
            }
            colores[bolilla] = "bg-danger text-light h6";
            ViewData["Reset"] = "disabled";
            var ganadores = cartones.FindAll(x => x.esGanador());
            var tamanio = ganadores.Count();
            if (tamanio != 0)
            {
                var listado = cartones.FindAll(x => x.esGanador());
                List<string> ganadoresId = new List<string>();
                foreach (var carton in listado)
                    ganadoresId.Add($"{carton.Id}");
                if (tamanio > 1)
                    TempData["msj"] = $"Los cartones ganadores son: {string.Join(", ", ganadoresId.ToArray())}";
                else
                    TempData["msj"] = $"El cartón ganador es: {ganadores[0].Id}";

                ViewData["Lanzador"] = true;
                ViewData["Reset"] = "enabled";

                if (tamanio < 4)
                {
                    for(int i = tamanio; i < 4; i++)
                        ganadoresId.Add(null);
                }

                //Se agrega a la base de datos el Nro de carton/es ganador/es
                _ctx.Add(new HistorialCartonesViewModel()
                {
                    Fecha = DateTime.Now,
                    Carton1 = ganadoresId[0],
                    Carton2 = ganadoresId[1],
                    Carton3 = ganadoresId[2],
                    Carton4 = ganadoresId[3]
                });
            }

            //Se agrega a la base de datos el Nro de la bolilla lanzada
            _ctx.Add(new HistorialBolilleroViewModel()
            {
                Fecha = DateTime.Now,
                NroBolilla = bolilla
            });

            _ctx.SaveChangesAsync();

            ViewBag.Modelo = model;
            ViewBag.Bolilla = bolilla;
            ViewBag.ListaBolillas = model.Lanzador.BolillasObtenidas;
            ViewBag.Colores = colores;

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}