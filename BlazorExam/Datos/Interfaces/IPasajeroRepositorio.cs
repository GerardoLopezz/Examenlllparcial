using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IPasajeroRepositorio
    {
        Task<bool> Nuevo(Pasajero pasajero);
        Task<bool> Actualizar(Pasajero pasajero);
        Task<bool> Eliminar(Pasajero pasajero);
        Task<IEnumerable<Pasajero>> GetLista();
        Task<Pasajero> GetPorCodigo(string codigo);
    }
}
