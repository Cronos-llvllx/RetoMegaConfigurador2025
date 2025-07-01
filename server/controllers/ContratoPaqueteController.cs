using megaapi.interfaces;
using megaapi.models;
using megaapi.models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace megaapi.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoPaqueteController : ControllerBase
    {
        private readonly IContratoPaquete _repo;

        public ContratoPaqueteController(IContratoPaquete repo)
        {
            _repo = repo;
        }

        // POST: api/ContratoPaquete
        [HttpPost]
        public async Task<IActionResult> AgregarPaquete([FromBody] ContratoPaqueteDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Los datos del paquete no pueden estar vacíos.");
                }

                if (dto.Idcontrato <= 0 || dto.Idpaquete <= 0)
                {
                    return BadRequest("El ID del contrato y del paquete deben ser válidos.");
                }

                // Create the ContratoPaquete entity from DTO
                var contractPackage = new ContratoPaquete
                {
                    Idcontrato = dto.Idcontrato,
                    Idpaquete = dto.Idpaquete,
                    FechaAdicion = DateTime.Now,
                    FechaRetiro = null // Es un paquete activo
                };

                var result = await _repo.CrearAsync(contractPackage);
                
                // Return a clean response
                var response = new ContratoPaqueteResponseDto
                {
                    Idcontrato = result.Idcontrato,
                    Idpaquete = result.Idpaquete,
                    FechaAdicion = result.FechaAdicion,
                    FechaRetiro = result.FechaRetiro
                };
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar el paquete: {ex.Message}");
            }
        }

        // PUT: api/ContratoPaquete/cancel/{contractId}/{packageId}
        [HttpPut("cancel/{contractId}/{packageId}")]
        public async Task<IActionResult> CancelarPaquete(int contractId, int packageId)
        {
            try
            {
                // Buscar el paquete activo (sin fecha de retiro)
                var contractPackage = await _repo.ObtenerPorIdAsync(new int[] { contractId, packageId });

                if (contractPackage == null)
                {
                    return NotFound($"No se encontró el paquete {packageId} en el contrato {contractId}.");
                }

                if (contractPackage.FechaRetiro != null)
                {
                    return BadRequest("Este paquete ya ha sido cancelado.");
                }

                // Marcar como retirado
                contractPackage.FechaRetiro = DateTime.Now;

                // Como no hay ActualizarAsync implementado, eliminamos y recreamos
                // Esto es una solución temporal - idealmente deberías implementar ActualizarAsync
                await _repo.EliminarAsync(contractPackage);
                await _repo.CrearAsync(contractPackage);

                return Ok(new { message = "Paquete cancelado exitosamente", contractPackage });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cancelar el paquete: {ex.Message}");
            }
        }

        // GET: api/ContratoPaquete/contract/{contractId}
        [HttpGet("contract/{contractId}")]
        public async Task<IActionResult> ObtenerPaquetesPorContrato(int contractId)
        {
            try
            {
                var packages = await _repo.ObtenerPorReferencia(contractId, "Idcontrato");
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los paquetes: {ex.Message}");
            }
        }
    }
}
