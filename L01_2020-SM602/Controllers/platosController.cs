using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_SM602.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_SM602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public platosController(restauranteContext restauranteContext)
        {
            _restauranteContext= restauranteContext;
        }
    }
}
