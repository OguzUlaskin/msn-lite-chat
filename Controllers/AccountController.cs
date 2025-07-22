using MsnLiteChatApp.Data;
using MsnLiteChatApp.Models;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MsnLiteChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] string username, [FromForm] string password, IFormFile profileImage)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                return BadRequest("Username already exists.");
            }

            // 1. Profil fotoğrafı klasörü oluştur (varsa geç)
            var profileFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
            if (!Directory.Exists(profileFolder))
                Directory.CreateDirectory(profileFolder);

            // 2. Dosya ismini belirle: ozil.jpg gibi
            var fileName = username + Path.GetExtension(profileImage.FileName);
            var filePath = Path.Combine(profileFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await profileImage.CopyToAsync(stream);
            }

            // 3. Kullanıcı verisini kaydet
            var user = new User
            {
                Username = username,
                Password = password,
                ProfileImage = fileName // Sadece dosya adı kaydedilir
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Kayıt başarılı.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string username, IFormFile? profileImage)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Kullanıcı adı gerekli");

            var profileFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
            Directory.CreateDirectory(profileFolder);

            string fileName = "default.png"; // Varsayılan fotoğraf

            if (profileImage != null && profileImage.Length > 0)
            {
                fileName = username + Path.GetExtension(profileImage.FileName);
                var filePath = Path.Combine(profileFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }
            }

            // Kullanıcı bilgilerini burada session vb. olarak da kaydedebilirsin (şimdilik sadece dönüş yapıyoruz)
            return Ok("Giriş başarılı");
        }

    }
}
