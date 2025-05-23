using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDBCase.Dtos.SliderDtos;
using MongoDBCase.Models;
using MongoDBCase.Services.CategoryServices;
using MongoDBCase.Services.ProductServices;
using MongoDBCase.Services.SliderServices;

namespace MongoDBCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        private readonly ISliderService _sliderService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, ISliderService sliderService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _sliderService = sliderService;
        }



        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> QuickViewComponent(string productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            ViewBag.CategoryName = category.CategoryName;
            return PartialView(product);
        }
        public async Task<PartialViewResult> Slider()
        {
            List<ResultSliderDTO> values = await _sliderService.GetAllSlidersAsync();
            return PartialView(values);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                TempData["SubscribeError"] = "Lütfen geçerli bir e-posta adresi giriniz.";
                return RedirectToAction("Index");
            }

            // Kullanıcı adı olarak mailin @ öncesini alalım örnek: user@example.com -> user
            var userName = email.Split('@')[0];

            // %20 indirim için rastgele kod üret (basit bir örnek)
            var discountCode = GenerateDiscountCode();

            // Mail gönder
            bool mailSent = await SendDiscountEmail(email, userName, discountCode);

            if (mailSent)
                TempData["SubscribeSuccess"] = "E-posta adresinize %20 indirim kodu gönderildi.";
            else
                TempData["SubscribeError"] = "E-posta gönderilirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";

            return RedirectToAction("Index");
        }

        private string GenerateDiscountCode()
        {
            // Basit 8 karakterli kod üretimi
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new char[8];
            for (int i = 0; i < code.Length; i++)
                code[i] = chars[random.Next(chars.Length)];
            return new string(code);
        }

        private async Task<bool> SendDiscountEmail(string toEmail, string userName, string discountCode)
        {
            try
            {
                var fromAddress = new MailAddress("tesrmemo@gmail.com", "Eticaret Mehmet");
                var toAddress = new MailAddress(toEmail);
                const string fromPassword = "yyfkrstkkn"; // Gmail için uygulama şifresi kullan
                const string subject = "Hoşgeldiniz! %20 İndirim Kodunuz";

                string body = $@"
                            Merhabalar {userName},

                            Sizin için özel %20 indirim kodunuz hazır: {discountCode}

                            Alışverişlerinizde kullanabilirsiniz. Keyifli alışverişler!

                            Saygılarımızla,
                            ";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    await smtp.SendMailAsync(message);
                }

                return true;
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine("SMTP Hatası: " + smtpEx.StatusCode);
                Console.WriteLine("Açıklama: " + smtpEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata: " + ex.Message);
                return false;
            }
        }
    }
}

