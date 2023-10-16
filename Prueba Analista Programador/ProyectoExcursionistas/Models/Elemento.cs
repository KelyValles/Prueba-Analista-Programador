using System;
using System.ComponentModel.DataAnnotations;

public class Elemento
{
        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Peso { get; set; }

        [Required]
        public int Calorias { get; set; }

        [Required]
        public string RutaImagen { get; set; }

}
