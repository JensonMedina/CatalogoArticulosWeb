using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class ArticuloDatos
    {
        public List<Articulo> Listar()
        {
            AccesoDatos Datos = new AccesoDatos();
            List<Articulo> Lista = new List<Articulo>();
            try
            {
                string Consulta = "select A.Id, A.Codigo, A.Nombre, A.Descripcion, C.Descripcion as Categoria, M.Descripcion as Marca, A.Precio, A.IdCategoria, A.IdMarca, A.ImagenUrl Imagen from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id And A.IdCategoria = C.Id";
                Datos.SetConsulta(Consulta);
                Datos.EjecutarLectura();
                while (Datos.lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)Datos.lector["Id"];
                    aux.Codigo = (string)Datos.lector["Codigo"];
                    aux.Nombre = (string)Datos.lector["Nombre"];
                    aux.Descripcion = (string)Datos.lector["Descripcion"];
                    if (!(Datos.lector["Imagen"] is DBNull))
                        aux.ImagenUrl = (string)Datos.lector["Imagen"];
                    aux.Precio = (Decimal)Datos.lector["Precio"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)Datos.lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)Datos.lector["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)Datos.lector["IdMarca"];
                    aux.Marca.Descripcion = (string)Datos.lector["Marca"];
                    Lista.Add(aux);
                }
                return Lista;
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
        public void AgregarArticulo(Articulo Nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                string Consulta = "insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)";
                Datos.SetConsulta(Consulta);
                Datos.SetParametros("@Codigo", Nuevo.Codigo);
                Datos.SetParametros("@Nombre", Nuevo.Nombre);
                Datos.SetParametros("@Descripcion", Nuevo.Descripcion);
                Datos.SetParametros("@IdMarca", Nuevo.Marca.Id);
                Datos.SetParametros("@IdCategoria", Nuevo.Categoria.Id);
                Datos.SetParametros("@ImagenUrl", Nuevo.ImagenUrl);
                Datos.SetParametros("@Precio", Nuevo.Precio);
                Datos.EjecutarAccion();
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
        public void ModificarArticulo(Articulo Modificado)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                string Consulta = "update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id";
                Datos.SetConsulta(Consulta);
                Datos.SetParametros("@Codigo", Modificado.Codigo);
                Datos.SetParametros("@Nombre", Modificado.Nombre);
                Datos.SetParametros("@Descripcion", Modificado.Descripcion);
                Datos.SetParametros("@IdCategoria", Modificado.Categoria.Id);
                Datos.SetParametros("@IdMarca", Modificado.Marca.Id);
                Datos.SetParametros("@ImagenUrl", Modificado.ImagenUrl);
                Datos.SetParametros("@Precio", Modificado.Precio);
                Datos.SetParametros("@Id", Modificado.Id);
                Datos.EjecutarAccion();
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
        public void EliminarArticulo(int Id)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                string Consulta = "delete from ARTICULOS where Id = @Id";
                Datos.SetConsulta(Consulta);
                Datos.SetParametros("@Id", Id);
                Datos.EjecutarAccion();
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
        public List<Articulo>Filtrar(string Campo, string Criterio, string Filtro)
        {
            AccesoDatos Datos = new AccesoDatos();
            List<Articulo> Lista = new List<Articulo>();
            try
            {
                string Consulta = "select A.Id, A.Codigo, A.Nombre, A.Descripcion, C.Descripcion as Categoria, M.Descripcion as Marca, A.Precio, A.IdCategoria, A.IdMarca, A.ImagenUrl Imagen from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id And A.IdCategoria = C.Id And ";
                if(Campo == "Precio")
                {
                    switch (Criterio)
                    {
                        case "Mayor a":
                            Consulta += "Precio > " + Filtro;
                            break;
                        case "Menor a":
                            Consulta += "precio < " + Filtro;
                            break;
                        default:
                            Consulta += "Precio = " + Filtro;
                            break;
                    }
                }
                else if(Campo == "Nombre")
                {
                    switch (Criterio)
                    {
                        case "Comienza con":
                            Consulta += "Nombre like '" + Filtro + "%' ";
                            break;
                        case "Termina con":
                            Consulta += "Nombre like '%" + Filtro + "'";
                            break;
                        default:
                            Consulta += "Nombre like '%" + Filtro + "%'";
                            break;
                    }
                }
                else if(Campo == "Marca")
                {
                    switch (Criterio)
                    {
                        case "Comienza con":
                            Consulta += "M.Descripcion like '" + Filtro + "%' ";
                            break;
                        case "Termina con":
                            Consulta += "M.Descripcion like '%" + Filtro + "'";
                            break;
                        default:
                            Consulta += "M.Descripcion like '%" + Filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (Criterio)
                    {
                        case "Comienza con":
                            Consulta += "C.Descripcion like '" + Filtro + "%' ";
                            break;
                        case "Termina con":
                            Consulta += "C.Descripcion like '%" + Filtro + "'";
                            break;
                        default:
                            Consulta += "C.Descripcion like '%" + Filtro + "%'";
                            break;
                    }
                }
                Datos.SetConsulta(Consulta);
                Datos.EjecutarLectura();
                while (Datos.lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)Datos.lector["Id"];
                    aux.Codigo = (string)Datos.lector["Codigo"];
                    aux.Nombre = (string)Datos.lector["Nombre"];
                    aux.Descripcion = (string)Datos.lector["Descripcion"];
                    aux.Precio = (Decimal)Datos.lector["Precio"];
                    if (!(Datos.lector["Imagen"] is DBNull))
                        aux.ImagenUrl = (string)Datos.lector["Imagen"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)Datos.lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)Datos.lector["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)Datos.lector["IdMarca"];
                    aux.Marca.Descripcion = (string)Datos.lector["Marca"];
                    Lista.Add(aux);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
