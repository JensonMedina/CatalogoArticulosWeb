using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Negocio
{
    //public class EmailService
    //{
    //    private MailMessage email;
    //    private SmtpClient server;

    //    public EmailService()
    //    {
    //        server = new SmtpClient();
    //        server.Credentials = new NetworkCredential("5d184d178b006d", "ce7b53563a6839");
    //        server.EnableSsl = true;
    //        server.Port = 2525;
    //        server.Host = "sandbox.smtp.mailtrap.io";
    //    }

    //    public void ArmarCorreo(string emailDestino, string asunto, string cuerpo)
    //    {
    //        email = new MailMessage();
    //        email.From = new MailAddress("noresponder@PokedexWeb.com");
    //        email.To.Add(emailDestino);
    //        email.Subject = asunto;
    //        email.IsBodyHtml = true;
    //        //email.Body = "<h1>Reporte de materias a las que se ha inscripto</h1> <br>Hola, te inscribiste.... bla bla";
    //        email.Body = cuerpo;
    //    }

    //    public void EnviarEmail()
    //    {
    //        try
    //        {
    //            server.Send(email);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
}