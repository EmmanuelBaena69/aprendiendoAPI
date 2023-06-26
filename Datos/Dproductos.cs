using Microsoft.AspNetCore.Mvc;
using tiendaAPI.modelo;
using tiendaAPI.Conexion;
using System.Data.SqlClient;
using System.Data;

namespace tiendaAPI.Datos
{
    public class Dproductos
    {
        ConexionDB cn = new ConexionDB();
        public async Task<List<Mproducts>> MostrarProductos()
        {
            var list = new List<Mproducts>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("showProducts", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mproductos = new Mproducts();
                            mproductos.id = (int)item["id"];
                            mproductos.descripcion = (string)item["descripcion"];
                            mproductos.precio = (decimal)item["precio"];
                            list.Add(mproductos);
                        }
                    }
                }
            }
            return list;
        }
        public async Task inserProducts(Mproducts parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (SqlCommand cmd = new SqlCommand("insertProducts", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }
        public async Task editarProducts(Mproducts parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (SqlCommand cmd = new SqlCommand("editProducts", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parametros.id);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }
    }
}
