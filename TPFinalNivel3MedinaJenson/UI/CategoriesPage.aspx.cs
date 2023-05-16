using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;
using Negocio;

namespace UI
{
    public partial class CategoriesPage : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }
        public int Id { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["categoriaId"] != null)
                Id = int.Parse(Request.QueryString["categoriaId"]);
            if (!IsPostBack)
            {
                ArticuloDatos datos = new ArticuloDatos();
                //if (Request.QueryString["categoriaId"] != null)
                //{
                    ListaArticulos = datos.Listar(null, Id);
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
                //}
                ddlCampo.Items.Add("Nombre");
                ddlCampo.Items.Add("Marca");
                ddlCampo.Items.Add("Precio");
            }
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = ddlCampo.SelectedValue.ToString();
            if (opcion == "Precio")
            {
                ddlCriterio.Items.Clear();
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
                ddlCriterio.Items.Add("igual a");
            }
            else
            {
                ddlCriterio.Items.Clear();
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
                ddlCriterio.Items.Add("Contiene");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                if (!ValidarFiltro())
                    return;
                string campo = ddlCampo.SelectedItem.ToString();
                string criterio = ddlCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text.ToString();
                repRep.DataSource = datos.Filtrar(campo, criterio, filtro, Id);
                repRep.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private bool ValidarFiltro()
        {
            if (ddlCampo.SelectedIndex < 0)
                return false;
            if (ddlCriterio.SelectedIndex < 0)
                return false;

            if (string.IsNullOrEmpty(txtFiltro.Text))
                return false;

            if(ddlCampo.SelectedValue.ToString() == "Precio")
            {
                if (!(SoloNumeros(txtFiltro.Text)))
                    return false;
            }

            return true;
        }

        private bool SoloNumeros(string text)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }
    }
}