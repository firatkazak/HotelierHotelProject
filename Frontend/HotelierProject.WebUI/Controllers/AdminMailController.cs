using HotelierProject.WebUI.Models.Mail;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace HotelierProject.WebUI.Controllers;

public class AdminMailController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(AdminMailViewModel model)//Mail göndereceğimiz için bir örnek aldık.
    {
        MimeMessage mimeMessage = new MimeMessage();//Bir MimeMessage örneği aldık.

        MailboxAddress mailboxAddressFrom = new MailboxAddress("HotelierAdmin", "stromhorsesweater@gmail.com");
        //Bir MailboxAddress örneği aldık. Bu 2 parametre istiyor. Gönderenin adı ve Gönderenin Mail adresi.
        mimeMessage.From.Add(mailboxAddressFrom);//mime Message'in gönderen kısmına 1 üst satırda yarattığımız nesneyi veriyoruz.

        MailboxAddress mailboxAddressTo = new MailboxAddress("User", model.ReceiverMail);
        //Bir MailboxAddress örneği aldık. Bu 2 parametre istiyor. Alıcının adı ve Alıcının Mail adresi.
        mimeMessage.To.Add(mailboxAddressTo);//mime Message'in alıcı kısmına 1 üst satırda yarattığımız nesneyi veriyoruz.

        var bodyBuilder = new BodyBuilder();//
        bodyBuilder.TextBody = model.Body;//Metin kısmı Modelimizin Body kısmından gelecek.
        mimeMessage.Body = bodyBuilder.ToMessageBody();//Yukarıda atama yaptığımız bodyBuilder nesnemizi mimeMessage'ın Body'sine ekliyoruz.

        mimeMessage.Subject = model.Subject;//model'den gelen Başlığı, mimeMessage'ın Başlık alanına ekliyoruz.

        SmtpClient client = new SmtpClient();//MailKit'in SmtpClient nesnesinden 1 örnek aldık.
        client.Connect("smtp.gmail.com", 587, false);//Smtp bağlantısı yap. (Bağlanacağı yer,Port Numarası, SSL Gereksin mi(hayır dedik)) 
        client.Authenticate("stromhorsesweater@gmail.com", "brrrnzhjoyjkavze");//Yetkilendirilecek mail ve password key'i(Google'ın verdiği Key).
        client.Send(mimeMessage);//client aracılığı ile mimeMessage'i gönder dedik.
        client.Disconnect(true);//Bağlantıyı sonlandır.

        return View();//View'i döndür.
    }
}

