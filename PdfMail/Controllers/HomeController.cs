using System;
using System.IO;
using System.Net.Mail;
using System.Web.Mvc;
using PdfMail.Models;

namespace PdfMail.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Export(MailData data)
		{
			//create pdf
			var pdfBinary = Convert.FromBase64String(data.Attachment);
			var dir = Server.MapPath("~/DataDump");

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			var fileName = dir + "\\PDFnMail-" + DateTime.Now.ToString("yyyyMMdd-HHMMss") + ".pdf";

			// write content to the pdf
			using (var fs = new FileStream(fileName, FileMode.Create))
			using (var writer = new BinaryWriter(fs))
			{
				writer.Write(pdfBinary, 0, pdfBinary.Length);
				writer.Close();
			}

			//Send mail
			var status = SendMail(fileName, data.To);

			//Delete file from file system
			System.IO.File.Delete(fileName);

			//Return result to client
			return Json(status ? new { result = "success" } : new { result = "failed" });
		}

		private static bool SendMail(string filePath, string recipient)
		{
			using (var client = new SmtpClient())
			using (var mail = new MailMessage())
			{
				mail.To.Add(recipient);
				mail.Subject = "Hi";
				mail.Body = "PFA";
				var attachment = new Attachment(filePath);
				mail.Attachments.Add(attachment);
				try
				{
					client.Send(mail);
					return true;
				}
				catch (Exception)
				{
					return false;
				}

			}

		}
	}
}