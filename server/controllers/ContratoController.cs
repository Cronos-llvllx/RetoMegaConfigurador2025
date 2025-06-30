using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace megaapi.controllers
{
    [ApiController]
    // La ruta se genera automáticamente a partir del nombre del controlador:
    // "Contrato"Controller -> /api/Contrato
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly IContrato _repo;

        public ContratoController(IContrato repo)
        {
            _repo = repo;
        }

        // GET: api/Contrato
        [HttpGet]
        public async Task<IActionResult> ObtenerContratos()
        {
            // Llama al método del repositorio  ya  implementado
            return Ok(await _repo.ObtenerTodoAsync());
        }

        // GET: api/Contrato/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerContratoPorId(int id)
        {
            // método del repositorio ya implementado
            var contrato = await _repo.ObtenerPorIdAsync(id);

            if (contrato == null)
            {
                return NotFound($"No se encontró un contrato con el identificador {id}");
            }

            return Ok(contrato);
        }
    }
}