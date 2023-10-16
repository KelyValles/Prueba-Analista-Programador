using Microsoft.AspNetCore.Mvc;
using ProyectoExcursionistas.Models;
using System.Diagnostics;

namespace ProyectoExcursionistas.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int minCalorias, int maxPeso)
        {
            List<Elemento> conjuntoOptimo = EncontrarConjuntoOptimo(minCalorias, maxPeso);

            if (conjuntoOptimo.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron elementos óptimos.";
            }

            return View(new Tuple<int, int, List<Elemento>>(minCalorias, maxPeso, conjuntoOptimo));
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
            new Elemento { Nombre = "Pies de gato", Peso = 2, Calorias = 3, RutaImagen = "/img/Pies_de_gato.jpg"},
            new Elemento { Nombre = "Cuerda", Peso = 5, Calorias = 7, RutaImagen = "/img/cuerda.jpg" },
            new Elemento { Nombre = "Arnes", Peso = 10, Calorias = 5, RutaImagen = "/img/arnes.jpg" },
            new Elemento { Nombre = "Asegurador", Peso = 3, Calorias = 5, RutaImagen = "/img/Asegurador.jpg" },
            new Elemento { Nombre = "Cintas exprés", Peso = 4, Calorias = 3, RutaImagen = "/img/Cintas_expres.jpg" },
            new Elemento { Nombre = "Mosquetones", Peso = 2, Calorias = 2, RutaImagen = "/img/Mosquetones.jpg" },
            new Elemento { Nombre = "Casco", Peso = 14, Calorias = 12, RutaImagen = "/img/casco.jpg" },
            new Elemento { Nombre = "Cabo de anclaje", Peso = 3, Calorias = 6, RutaImagen = "/img/Cabo_de_anclaje.jpg" },
            new Elemento { Nombre = "Magnesio", Peso = 9, Calorias = 8, RutaImagen = "/img/Magnesio.jpg" },
            new Elemento { Nombre = "Guantes", Peso = 7, Calorias = 9, RutaImagen = "/img/guantes.jpg" }
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

            List<Elemento> conjuntoOptimo = new();
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