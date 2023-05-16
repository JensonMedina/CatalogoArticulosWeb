using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Verificaciones
    {
        public static bool VerificarImagen(string imageUrl)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    // Realiza una petición HTTP a la URL de la imagen
                    if (imageUrl.ToUpper().Contains("HTTPS")) { }
                        client.DownloadData(imageUrl);
                    return true; // La imagen se carga correctamente
                }
            }
            catch (Exception)
            {
                return false; // La imagen no se carga correctamente
            }
        }

    }
}
