using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.IRepositories;
using Lunchers.Models.Repositories;
using Lunchers.Models.ViewModels.Handelaar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private IHandelaarRepository _handelaarRepository;
        private IReservatieRepository _reservatieRepository;
        private ILunchRespository _lunchRespository;

        public AdminController(IHandelaarRepository handelaarRepository, IReservatieRepository reservatieRepository, ILunchRespository lunchRespository)
        {
            _handelaarRepository = handelaarRepository;
            _reservatieRepository = reservatieRepository;
            _lunchRespository = lunchRespository;
        }

        [HttpPost]
        public IActionResult KeurHandelaarGoed([FromForm]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    handelaar.Login.Geactiveerd = true;
                    _handelaarRepository.SaveChanges();

                    //mail service
                    var message = new MailMessage();
                    message.From = new MailAddress("lunchersteam@gmail.com");
                    //message.To.Add(handelaar.Email);
                    message.To.Add("brent_schets@hotmail.be");
                    message.ReplyToList.Add("lunchersteam@gmail.com");
                    message.Subject = "Uw aanvraag om handelaar te worden werd goedgekeurd";
                    message.Body = string.Format("Beste {0} \n\nUw aanvraag om handelaar te worden, werd zonet goedgekeurd.\nU kan nu aanmelden met de door u gekozen gebruikersnaam en wachtwoord.\nEn u kan onmiddelijk een lunch toevoegen als u dat wilt.\n\nMet vriedelijke groeten,\nHet Lunchers team ",
                    handelaar.HandelsNaam);

                    //smpt server
                    var SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("lunchersteam@gmail.com", "reallyStrongPwd123");
                    SmtpServer.EnableSsl = true;

                    //message sent
                    SmtpServer.Send(message);

                    return Ok(new { bericht = "De handelaar werd succesvol goedgekeurd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpPost]
        public IActionResult KeurHandelaarAf([FromForm]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    handelaar.Login.Geactiveerd = false;
                    _handelaarRepository.Delete(handelaar.GebruikerId);
                    _handelaarRepository.SaveChanges();

                    //mail service
                    var message = new MailMessage();
                    message.From = new MailAddress("lunchersteam@gmail.com");
                    //message.To.Add(handelaar.Email);
                    message.To.Add("brent_schets@hotmail.be");
                    message.ReplyToList.Add("lunchersteam@gmail.com");
                    message.Subject = "Uw aanvraag om handelaar te worden werd afgekeurd";
                    message.Body = string.Format("Beste {0} \n\nUw aanvraag om handelaar te worden, werd zonet afgekeurd.\nAls u toch nog handelaar wenst te worden zal u contact moeten opnemen met de administrator van lunchers.ml\n\nMet vriedelijke groeten,\nHet Lunchers team ",
                    handelaar.HandelsNaam);

                    //smpt server
                    var SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("lunchersteam@gmail.com", "reallyStrongPwd123");
                    SmtpServer.EnableSsl = true;

                    //message sent
                    SmtpServer.Send(message);

                    return Ok(new { bericht = "De handelaar werd succesvol afgekeurd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpPost]
        public IActionResult VerwijderHandelaar([FromForm]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    _handelaarRepository.Delete(handelaar.GebruikerId);
                    _handelaarRepository.SaveChanges();

                    //mail service
                    var message = new MailMessage();
                    message.From = new MailAddress("lunchersteam@gmail.com");
                    //message.To.Add(handelaar.Email);
                    message.To.Add("brent_schets@hotmail.be");
                    message.ReplyToList.Add("lunchersteam@gmail.com");
                    message.Subject = "U werd zonet verwijderd van Lunchers";
                    message.Body = string.Format("Beste {0} \n\nU werd zonet verwijder van de website lunchers.ml.\nU kan nog altijd contact opnemen met de administrator van lunchers.ml.\n\nMet vriedelijke groeten,\nHet Lunchers team ",
                    handelaar.HandelsNaam);

                    //smpt server
                    var SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("lunchersteam@gmail.com", "reallyStrongPwd123");
                    SmtpServer.EnableSsl = true;

                    //message sent
                    SmtpServer.Send(message);

                    return Ok(new { bericht = "De handelaar werd succesvol verwijderd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpGet]
        public IActionResult KrijgAantallen()
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                var aantallen = new
                {
                    AantalHandelaars = _handelaarRepository.GetAll().Count(),
                    AantalLunches = _lunchRespository.GetAll().Count(),
                    AantalReservaties = _reservatieRepository.GetAll().Count()
                };

                return Ok(aantallen);
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }
    }
}