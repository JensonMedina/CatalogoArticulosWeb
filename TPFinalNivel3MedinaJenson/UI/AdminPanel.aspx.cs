using System;
using System.Collections.Generic;
using System.Globalization;
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
        private const string PlaceholderImagePath = "./imagenes/placeholder.png";
        public int? Id { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Add("Id", Id);
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
            try
            {
                Id = (int)dgvArticulos.SelectedDataKey.Value;
                Session["Id"] = Id;
                ArticuloDatos datos = new ArticuloDatos();
                Articulo seleccionado = (datos.Listar(Id))[0];
                txtCodigo.Text = seleccionado.Codigo;
                txtNombre.Text = seleccionado.Nombre;
                txtDescripcion.Text = seleccionado.Descripcion;
                Session.Add("imgActual", seleccionado.ImagenUrl);
                CargarImagen(seleccionado);

                ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();
                ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                txtPrecio.Text = seleccionado.Precio.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
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

        private void CargarArticulo(Articulo articulo)
        {
            articulo.Nombre = txtNombre.Text;
            articulo.Descripcion = txtDescripcion.Text;
            articulo.Codigo = txtCodigo.Text;
            articulo.Categoria = new Categoria();
            articulo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
            articulo.Marca = new Marca();
            articulo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
            articulo.Precio = decimal.Parse(txtPrecio.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-AR"));
            if (txtImagen.PostedFile.FileName != "")
            {
                string identity = DateTime.Now.ToString("yyyyMMddHHmmss");
                string ruta = Server.MapPath("./imagenes/");
                txtImagen.PostedFile.SaveAs(ruta + "Articulo-" + identity + ".jpg");
                articulo.ImagenUrl = "Articulo-" + identity + ".jpg";
            }
            else
            {
                if(Session["Id"] != null)
                {
                    if (Verificaciones.VerificarImagen(Session["imgActual"].ToString()))
                    {
                        articulo.ImagenUrl = Session["imgActual"].ToString();
                    }
                    else if (Session["imgActual"].ToString().ToUpper().Contains("ARTICULO"))
                    {
                        articulo.ImagenUrl = Session["imgActual"].ToString();
                    }
                    else
                        articulo.ImagenUrl = PlaceholderImagePath;
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                if (ValidarCampos())
                {
                    Articulo nuevo = new Articulo();
                    CargarArticulo(nuevo);
                    datos.AgregarArticulo(nuevo);
                    CargarImagen(nuevo);
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                if (ValidarCampos())
                {
                    Articulo seleccionado = new Articulo();
                    CargarArticulo(seleccionado);
                    seleccionado.Id = (int)Session["Id"];
                    datos.ModificarArticulo(seleccionado);
                    CargarImagen(seleccionado);
                    CargarGrilla();
                    dgvArticulos_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                    datos.EliminarArticulo((int)Session["Id"]);
                    CargarGrilla();
                    btnLimpiar_Click(sender, e);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
        private void CargarGrilla()
        {
            ArticuloDatos articulo = new ArticuloDatos();
            try
            {
                List<Articulo> listaArticulos = articulo.Listar();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.DataBind();

                foreach (GridViewRow row in dgvArticulos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        decimal precio = Convert.ToDecimal(row.Cells[5].Text);
                        row.Cells[5].Text = precio.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
        private void CargarImagen(Articulo seleccionado)
        {
            if (string.IsNullOrEmpty(seleccionado.ImagenUrl))
                imgArticulo.ImageUrl = PlaceholderImagePath;
            else if (seleccionado.ImagenUrl.ToString().ToUpper().Contains("ARTICULO"))
                imgArticulo.ImageUrl = "./imagenes/" + seleccionado.ImagenUrl;
            else if (Verificaciones.VerificarImagen(seleccionado.ImagenUrl))
                imgArticulo.ImageUrl = seleccionado.ImagenUrl;
            else
                imgArticulo.ImageUrl = PlaceholderImagePath;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            imgArticulo.ImageUrl = PlaceholderImagePath;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text)
                || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtPrecio.Text))
                return false;
            return true;
        }
    }
}