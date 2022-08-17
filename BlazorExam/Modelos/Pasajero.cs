using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Pasajero
    {
        [Required(ErrorMessage = "El codigo es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La clave es obligatorio")]
        public string Clave { get; set; }
        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; }
        public bool EstaActivo { get; set; }
        [Required(ErrorMessage = "El origen es obligatorio")]
        public string Origen { get; set; }
        [Required(ErrorMessage = "El destino es obligatorio")]
        public string Destino { get; set; }
        public string Avion { get; set; }
        public decimal CantidadPasajeros { get; set; }
        public string Piloto { get; set; }
        public string Horarios { get; set; }
        public DateTime Fecha { get; set; }

    }
}
