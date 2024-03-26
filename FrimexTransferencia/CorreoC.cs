using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace FrimexTransferencia
{
    class CorreoC
    {
        List<string> _archivos = new List<string>();
        List<string> _destinatarios = new List<string>();
        public string CORREOCONTRASEÑA { set; get; }
        public string CORREODIRECCION { set; get; }
        public int PUERTOSMTP { set; get; }
        public string SMTPMAIL { set; get; }
        public string REMITENTE { set; get; }
        public List<string> DESTINATARIOS { set { _destinatarios = value; } get { return _destinatarios; } }
        public string ASUNTO { set; get; }
        public string MENSAJE { set; get; }
        public List<string> ARCHIVOS { set { _archivos = value; } get { return _archivos; } }
        public bool EnviarCorreo(out string msg)
        {
            string msg_local = "";
            bool _exito = false;

            string DESTINATARIO = "";
            foreach (string destinatario in DESTINATARIOS)
                DESTINATARIO = destinatario + ";";

            if (DESTINATARIO.Trim().Equals("") || MENSAJE.Trim().Equals("") || ASUNTO.Trim().Equals(""))
            {
                msg_local = "La direccion de correo del destinatario, asunto y mensaje son obligatorios";
                _exito = false;
            }
            try
            {
                MailMessage Email = new System.Net.Mail.MailMessage();
                foreach (string destinatario in DESTINATARIOS)
                    Email.To.Add(destinatario);
                Email.Body = MENSAJE;
                Email.Subject = ASUNTO;
                if (ARCHIVOS.Count>0 )
                    foreach (string archivo in ARCHIVOS)
                        if (System.IO.File.Exists(@archivo))
                            Email.Attachments.Add(new Attachment(@archivo));
                Email.IsBodyHtml = true;
                Email.From = new MailAddress(REMITENTE, "Sistema Frimex");
                //System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                // SmtpClient smtpMail = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient smtpMail = new SmtpClient(SMTPMAIL, PUERTOSMTP);
                smtpMail.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(CORREODIRECCION, CORREOCONTRASEÑA);
                smtpMail.Credentials = credentials;
                smtpMail.EnableSsl = true;                
                smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;                
                smtpMail.Send(Email);
                smtpMail.Dispose();
                _exito = true;
            }
            catch (Exception ex)
            {
                msg_local = "Ocurrio un error: " + ex.Message;
                _exito = false;
            }
            msg = msg_local;
            return _exito;
        }      
    }
}
