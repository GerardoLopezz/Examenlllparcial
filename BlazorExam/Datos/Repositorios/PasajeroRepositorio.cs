using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Datos.Repositorios
{
    public class PasajeroRepositorio : IPasajeroRepositorio
    {
        private string CadenaConexion;

        public PasajeroRepositorio(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }
        public Task<bool> Actualizar(Pasajero usuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(Pasajero usuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pasajero>> GetLista()
        {
            throw new NotImplementedException();
        }

        public async Task<Pasajero> GetPorCodigo(string codigo)
        {
            Pasajero user = new Pasajero();
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"SELECT * FROM pasajero WHERE Codigo = @Codigo;";
                user = await conexion.QueryFirstAsync<Pasajero>(sql, new { codigo});
            }
            catch (Exception ex)
            {
            }
            return user;
        }

        public async Task<bool> Nuevo(Pasajero pasajero)
        {
            bool result = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO pasajero (Codigo, Nombre, Clave, Rol, EstaActivo, Origen, Destino, Avion, CantidadPasajeros, Piloto, Horarios, Fecha) 
                             VALUES (@Codigo, @Nombre, @Clave, @Rol, @EstaActivo, @Origen, @Destino, @Avion, @CantidadPasajeros, @Piloto, @Horarios, @Fecha);";
                result = Convert.ToBoolean(await conexion.ExecuteScalarAsync(sql, pasajero));

            }
            catch (Exception)
            {
            }
            return result;

        }
    }
}
