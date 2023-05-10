using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class CategoriaDatos
    {
        public List<Categoria>Listar()
        {
            AccesoDatos Datos = new AccesoDatos();
            List<Categoria> ListaCategorias = new List<Categoria>();
            try
            {
                string Consulta = "select Id, Descripcion from CATEGORIAS";
                Datos.SetConsulta(Consulta);
                Datos.EjecutarLectura();
                while (Datos.lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)Datos.lector["Id"];
                    aux.Descripcion = (string)Datos.lector["Descripcion"];
                    ListaCategorias.Add(aux);
                }

                return ListaCategorias;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }
    }
}
