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
    public partial class AdminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    CargarGrilla();
                    CargarDdl();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                }
            }
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)dgvArticulos.SelectedDataKey.Value;
            ArticuloDatos datos = new ArticuloDatos();
            Articulo seleccionado = (datos.Listar(id))[0];
            txtCodigo.Text = seleccionado.Codigo;
            txtNombre.Text = seleccionado.Nombre;
            txtDescripcion.Text = seleccionado.Descripcion;
            if(string.IsNullOrEmpty(seleccionado.ImagenUrl))
                imgArticulo.ImageUrl = "./imagenes/placeholder.png";
            else
                imgArticulo.ImageUrl = "~/imagenes/" + seleccionado.ImagenUrl;
            
            ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();
            ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
            txtPrecio.Text = seleccionado.Precio.ToString();
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }
        private void CargarDdl()
        {
            CategoriaDatos categoria = new CategoriaDatos();
            MarcaDatos marca = new MarcaDatos();
            try
            {
                ddlCategoria.DataSource = categoria.Listar();
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataBind();
                ddlMarca.DataSource = marca.Listar();
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                Articulo nuevo = new Articulo();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                if (txtImagen.PostedFile.FileName != "")
                {
                    string identity = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string ruta = Server.MapPath("./imagenes/");
                    txtImagen.PostedFile.SaveAs(ruta + "Articulo-" + identity + ".jpg");
                    nuevo.ImagenUrl = "Articulo-" + identity + ".jpg";
                }
                datos.AgregarArticulo(nuevo);
                imgArticulo.ImageUrl = "./imagenes/" + nuevo.ImagenUrl;
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
                //Session.Add("error", ex.ToString());
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }
        private void CargarGrilla()
        {
            ArticuloDatos articulo = new ArticuloDatos();
            try
            {
                List<Articulo> listaArticulos = articulo.Listar();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}