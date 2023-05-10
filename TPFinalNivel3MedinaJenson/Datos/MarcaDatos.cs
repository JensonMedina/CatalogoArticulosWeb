using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class MarcaDatos
    {
        public List<Marca>Listar()
        {
            AccesoDatos Datos = new AccesoDatos();
            List<Marca> ListaMarcas = new List<Marca>();
            try
            {
                string Consulta = "select Id, Descripcion from MARCAS";
                Datos.SetConsulta(Consulta);
                Datos.EjecutarLectura();
                while (Datos.lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)Datos.lector["Id"];
                    aux.Descripcion = (string)Datos.lector["Descripcion"];
                    ListaMarcas.Add(aux);
                }
                return ListaMarcas;
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
