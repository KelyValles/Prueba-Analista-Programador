using Microsoft.AspNetCore.Mvc;
using ProyectoExcursionistas.Models;
using System.Diagnostics;

namespace ProyectoExcursionistas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Index(int minCalorias, int maxPeso)
        {
            List<Elemento> conjuntoOptimo = EncontrarConjuntoOptimo(minCalorias, maxPeso);

            var model = new Tuple<int, int, List<Elemento>>(minCalorias, maxPeso, conjuntoOptimo);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private List<Elemento> elementos = new()
        {
            new Elemento { Nombre = "Pies de gato", Peso = 2, Calorias = 3 },
            new Elemento { Nombre = "Cuerda", Peso = 5, Calorias = 7 },
            new Elemento { Nombre = "Arnes", Peso = 10, Calorias = 5 },
            new Elemento { Nombre = "Asegurador", Peso = 3, Calorias = 5 },
            new Elemento { Nombre = "Cintas exprés", Peso = 4, Calorias = 3 },
            new Elemento { Nombre = "Mosquetones", Peso = 2, Calorias = 2 },
            new Elemento { Nombre = "Casco", Peso = 14, Calorias = 12 },
            new Elemento { Nombre = "Cabo de anclaje", Peso = 3, Calorias = 6 },
            new Elemento { Nombre = "Magnesio", Peso = 9, Calorias = 8 },
            new Elemento { Nombre = "Guantes", Peso = 7, Calorias = 9 }
        };



        private List<Elemento> EncontrarConjuntoOptimo(int minCalorias, int maxPeso)
        {
            int elementosDisponibles = elementos.Count;
            int[,] resultadosSeleccion = new int[elementosDisponibles + 1, maxPeso + 1];

            for (int i = 1; i <= elementosDisponibles; i++)
            {
                for (int j = 0; j <= maxPeso; j++)
                {
                    if (elementos[i - 1].Peso > j)
                    {
                        resultadosSeleccion[i, j] = resultadosSeleccion[i - 1, j];
                    }
                    else
                    {
                        resultadosSeleccion[i, j] = Math.Max(resultadosSeleccion[i - 1, j], resultadosSeleccion[i - 1, j - elementos[i - 1].Peso] + elementos[i - 1].Calorias);
                    }
                }
            }

            // Encontrar el valor máximo de calorías que cumple con el mínimo requerido.
            int maxCalorias = resultadosSeleccion[elementosDisponibles, maxPeso];
            if (maxCalorias < minCalorias)
            {
                
                return new List<Elemento>();
            }

            List<Elemento> conjuntoOptimo = new List<Elemento>();
            int pesoRestante = maxPeso;
            for (int i = elementosDisponibles; i > 0 && pesoRestante > 0; i--)
            {
                if (resultadosSeleccion[i, pesoRestante] != resultadosSeleccion[i - 1, pesoRestante])
                {
                    conjuntoOptimo.Add(elementos[i - 1]);
                    pesoRestante -= elementos[i - 1].Peso;
                }
            }

            return conjuntoOptimo;
        }




    }
}