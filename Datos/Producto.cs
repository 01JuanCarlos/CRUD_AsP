using POOII_CL2_TorresMirandaJuanCarlos.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace POOII_CL2_TorresMirandaJuanCarlos.Datos
{
    public class Producto
    {
        public List<ProductoModel> Listar()
        {
            var oLista = new List<ProductoModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new ProductoModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = dr["nombre"].ToString(),
                            idtipo= Convert.ToInt32(dr["idtipo"]),
                            precio = Convert.ToDecimal(dr["precio"]),
                            fecha = Convert.ToDateTime(dr["fecha"]),
                            foto = dr["foto"].ToString()

                        });
                    }
                }

            }
            return oLista;
        }


       


        public bool Agregar(ProductoModel oproducto , IFormFile foto)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    
                    string rutaFoto = "~/imagenes/" + foto.FileName;

                    SqlCommand cmd = new SqlCommand("sp_Agregar", conexion);
                    cmd.Parameters.AddWithValue("nombre", oproducto.nombre);
                    cmd.Parameters.AddWithValue("idtipo", oproducto.idtipo);
                    cmd.Parameters.AddWithValue("precio", oproducto.precio);
                    cmd.Parameters.AddWithValue("fecha", oproducto.fecha);
                    cmd.Parameters.AddWithValue("foto", rutaFoto);
                    string rutaImagenes = Environment.CurrentDirectory + "/wwwroot/imagenes/" + foto.FileName;
                    FileStream flujo = new FileStream(rutaImagenes, FileMode.Create);
                    foto.CopyTo(flujo);
                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;


        }


        public ProductoModel Obtener(int IdProducto)
        {
            var oProducto = new ProductoModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("id", IdProducto);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oProducto.id = Convert.ToInt32(dr["id"]);
                        oProducto.nombre = dr["nombre"].ToString();
                        oProducto.idtipo = Convert.ToInt32(dr["idtipo"]);
                        oProducto.precio = Convert.ToDecimal(dr["precio"]);
                        oProducto.fecha = Convert.ToDateTime(dr["fecha"]);
                        oProducto.foto = dr["foto"].ToString();
                    }
                }

            }
            return oProducto;
        }


        public List<ProductoModel> spConsultarFecha(string fecha)
        {
            List<ProductoModel> lst = new List<ProductoModel>();
            var cn = new Conexion();
            SqlConnection con = new SqlConnection(cn.getCadenaSQL());
            SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE fecha like @fecha", con);

            string parAño = "%" + fecha + "%";
            cmd.Parameters.AddWithValue("@fecha", parAño);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductoModel producto = new ProductoModel();
                producto.id = dr.GetInt32(0);
                producto.nombre = dr.GetString(1);
                producto.idtipo = dr.GetInt32(2);
                producto.precio = dr.GetDecimal(3);
                producto.fecha = dr.GetDateTime(4);

                lst.Add(producto);
            }

            return lst;
        }




        //public ProductoModel BuscarFecha(DateTime fecha)
        //{
        //    var oProducto = new ProductoModel();
        //    var cn = new Conexion();
        //    using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //    {
        //        conexion.Open();
        //        SqlCommand cmd = new SqlCommand("sp_ObtenerFecha", conexion);
        //        cmd.Parameters.AddWithValue("fecha", fecha);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        using (var dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                oProducto.id = Convert.ToInt32(dr["id"]);
        //                oProducto.nombre = dr["nombre"].ToString();
        //                oProducto.idtipo = Convert.ToInt32(dr["idtipo"]);
        //                oProducto.precio = Convert.ToDouble(dr["precio"]);
        //                oProducto.fecha = Convert.ToDateTime(dr["fecha"]);
        //            }
        //        }

        //    }
        //    return oProducto;
        //}




        public bool Eliminar(int IdProducto)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("id", IdProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;


        }













        public bool Actualizar(ProductoModel oProducto)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                   
                    SqlCommand cmd = new SqlCommand("Actualizar", conexion);
                    cmd.Parameters.AddWithValue("id", oProducto.id);
                    cmd.Parameters.AddWithValue("nombre", oProducto.nombre);
                    cmd.Parameters.AddWithValue("idtipo", oProducto.idtipo);
                    cmd.Parameters.AddWithValue("precio", oProducto.precio);
                    cmd.Parameters.AddWithValue("fecha", oProducto.fecha);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;


        }
    }
}
