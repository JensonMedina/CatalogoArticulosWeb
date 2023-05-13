using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;

namespace UI
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public List<Categoria> ListaCategorias { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoriaDatos Categoria = new CategoriaDatos();
                ListaCategorias = Categoria.Listar();

                try
                {
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                }
            }
        }
    }
}