using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MsnLiteChatApp.Data;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MsnLiteChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _context.Messages
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }
    }
}
