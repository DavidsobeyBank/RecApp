using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecruitmentApplication.Controllers
{
    public class SharedFunctionsController : Controller
    {
        // GET: SharedFunctions
        public ActionResult Index()
        {
            return View();
        }

        public void SendMail(String to, String subject, String body)
        {
            MailMessage objecto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();

            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"].ToString());
            client.Host = ConfigurationManager.AppSettings["host"].ToString();
            client.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["timeout"].ToString());

            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            objecto_mail.From = new MailAddress("noreply@recapp.net");
            objecto_mail.Subject = subject;
            objecto_mail.Body = body + "\r\n\r\nFor your reference, here is the link to RecApp:\r\n"
                                           + "recapp.azurewebsites.net"
                                           + "\r\n\r\nRegards\r\n RecApp Admin";

            objecto_mail.To.Add(new MailAddress(to));

            client.Send(objecto_mail);
        }

        //This method stops the runtime error that asp.net throws because we cannot nest the Gridview inside a <form> tag. Just leave it here doing nothing.
        //NO TOUCHY!
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    //base.VerifyRenderingInServerForm(control);
        //}
        //Writes to an excel file
        private void writeToExcelFile(GridView gridView, string reportName)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string filename = reportName + DateTime.Now + ".xlsx";
            StringWriter writer = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xlsx";
            Response.AddHeader("content-disposition", "attachment;filename= " + filename);
            gridView.GridLines = GridLines.Both;
            gridView.HeaderStyle.Font.Bold = true;
            gridView.RenderControl(htmlWriter);
            string renderedView = writer.ToString();

            Response.Write(renderedView);
            Response.End();

        }
    }
}