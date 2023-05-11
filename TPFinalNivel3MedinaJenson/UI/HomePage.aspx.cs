using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;
using Negocio;

namespace UI
{
    public partial class HomePage : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloDatos articulo = new ArticuloDatos();
            if (!IsPostBack)
            {
                ListaArticulos = articulo.Listar();
                foreach (Articulo item in ListaArticulos)
                {
                    if (string.IsNullOrEmpty(item.ImagenUrl) || !Verificaciones.VerificarImagen(item.ImagenUrl))
                    {
                        // Establece una URL de imagen de tipo placeholder
                        item.ImagenUrl = "./imagenes/placeholder.png";
                    }
                }
                repRep.DataSource = ListaArticulos;
                repRep.DataBind();
            }
        }
    }
}