using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace megaapi.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaqueteController : ControllerBase
    {
        private readonly IPaquete _repo;

        public PaqueteController(IPaquete repo)
        {
            _repo = repo;
        }

        // GET: api/Paquete
        [HttpGet]
        public async Task<IActionResult> ObtenerPaquetes()
        {
            return Ok(await _repo.ObtenerTodoAsync());
        }
    }
}