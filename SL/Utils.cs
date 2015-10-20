using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BarcodeLib;
using BIZ;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;

namespace SL
{
    public class Utils
    {

        public string generarQR(string codigo)
        {
            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(codigo);

            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            string path = AppDomain.CurrentDomain.BaseDirectory + "tmp\\"+ codigo +"QR.png";
            using (var stream = new FileStream(path, FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            return codigo+"QR.png";
        }

        public String generaCodigoBarras(String codigo)
        {
            String nombreArchivo = codigo + ".jpg";
            var fileName = string.Format(AppDomain.CurrentDomain.BaseDirectory + "{0}", @"tmp\" + nombreArchivo);

            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
            {
                IncludeLabel = true,
                Alignment = AlignmentPositions.CENTER,
                Width = 350,
                Height = 55,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
                AlternateLabel = codigo, //aca se podría poder otra leyenda cuando se lee.
            };

            Image img = barcode.Encode(TYPE.Interleaved2of5, codigo);
            img.Save(fileName);

            return nombreArchivo;
        }
        public void sendMail(BIZCorreo pCorreo)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.To.Add(pCorreo.To);
                mail.From = new MailAddress("iidcorpuai@gmail.com");
                mail.Subject = pCorreo.Subject;
                mail.Body = pCorreo.Body;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("iidcorpuai@gmail.com", "socrates321");// Enter senders User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex) {
                //bitacora guardar error mail
            }

        }

    }

}

