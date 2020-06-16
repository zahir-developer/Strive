using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {

        private readonly IHubContext<ChatHub> hubContext;

        public SignalRController(IHubContext<ChatHub> _hubContext)
        {
            hubContext = _hubContext;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<string> CheckIn(string count)
        {
            await hubContext.Clients.All.SendAsync("UpdateCarCheckIn", count);
            //await hubContext.Clients.All.SendAsync("Notify", $"Home page loaded at: {DateTime.Now}");
            return "Check In count updated: " + count;
        }
    }
}
