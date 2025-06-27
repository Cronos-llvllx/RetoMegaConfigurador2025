using Microsoft.AspNetCore.Mvc;
using megaapi.Models;
using megaapi.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace megaapi.Controllers
{
    // Define que esta clase es un controlador de API y la ruta base.
    [ApiController]
    [Route("api/[controller]")] // Esto mapea a /api/Subscribers
    public class SubscribersController : ControllerBase
    {
        // ----------------------------------------------------------------------
        // 1. INYECCIÓN DE DEPENDENCIAS
        //    Aquí declaras los repositorios y servicios que tu controlador necesita.
        // ----------------------------------------------------------------------
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IPromotionService _promotionService;

        // Constructor: Recibe las instancias a través de la inyección de dependencias.
        public SubscribersController(ISubscriberRepository subscriberRepository, IContractRepository contractRepository, IPromotionService promotionService)
        {
            _subscriberRepository = subscriberRepository;
            _contractRepository = contractRepository;
            _promotionService = promotionService;
        }

        // ----------------------------------------------------------------------
        // 2. ENDPOINTS DE LA API (ACCIONES)
        // ----------------------------------------------------------------------

        /// <summary>
        /// Obtiene un suscriptor por su ID.
        /// </summary>
        /// <param name="id">ID del suscriptor.</param>
        /// <returns>El objeto Subscriber si se encuentra.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Subscriber))]
        [ProducesResponseType(404)]
        public IActionResult GetSubscriber(int id)
        {
            var subscriber = _subscriberRepository.GetSubscriberById(id);

            if (subscriber == null)
            {
                return NotFound($"Suscriptor con ID {id} no encontrado.");
            }

            return Ok(subscriber);
        }

        /// <summary>
        /// Da de alta un nuevo suscriptor y crea un contrato para él, aplicando promociones manuales.
        /// </summary>
        /// <param name="altaRequest">Objeto con la información del suscriptor y el contrato.</param>
        /// <returns>El suscriptor creado con el nuevo contrato.</returns>
        [HttpPost("alta")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult RegisterSubscriberWithContract([FromBody] RegisterSubscriberWithContractRequest altaRequest)
        {
            // ----------------------------------------------------------------------
            // 3. LÓGICA DE NEGOCIO EN EL ENDPOINT
            // ----------------------------------------------------------------------

            // 3.1. Validación básica de los datos de entrada
            if (altaRequest?.Subscriber == null || string.IsNullOrEmpty(altaRequest.Subscriber.Name) || string.IsNullOrEmpty(altaRequest.Subscriber.PhoneNumber))
            {
                return BadRequest("El suscriptor y sus datos básicos (nombre y teléfono) son obligatorios.");
            }
            
            // 3.2. Lógica para buscar y validar la promoción de contratación manual.
            // Los nombres de propiedades aquí se basan en tu modelo 'Promotion.cs'.
            decimal contractPrice = altaRequest.BaseContractPrice;
            Promotion promotionContratacion = null;
            if (altaRequest.IdManualPromotion.HasValue)
            {
                // Usa el servicio de promociones para buscar la promoción.
                promotionContratacion = _promotionService.GetPromotionById(altaRequest.IdManualPromotion.Value);
                
                if (promotionContratacion == null)
                {
                    return BadRequest("La promoción de contratación seleccionada no es válida.");
                }

                // Aquí validamos que la promoción sea de tipo 'contratación' (Type = 1) y esté vigente.
                if (promotionContratacion.Type != 1 || promotionContratacion.Validity < DateTime.Now)
                {
                    return BadRequest("La promoción de contratación seleccionada no es del tipo correcto o no está vigente.");
                }
                
                // Aplica la lógica del precio o porcentaje (PriceOrPorcentage).
                // La lógica se basa en la descripción del diccionario de datos.
                if (promotionContratacion.PriceOrPorcentage >= 0 && promotionContratacion.PriceOrPorcentage <= 1)
                {
                    // Es un porcentaje, aplica el descuento.
                    contractPrice = contractPrice * (1 - promotionContratacion.PriceOrPorcentage);
                }
                else
                {
                    // Es un precio fijo, reemplaza el precio base.
                    contractPrice = promotionContratacion.PriceOrPorcentage;
                }
            }

            // 3.3. Crear y guardar el nuevo suscriptor.
            var newSubscriber = altaRequest.Subscriber;
            _subscriberRepository.AddSubscriber(newSubscriber);
            _subscriberRepository.SaveChanges(); // Guarda para obtener el Id de la simulación.

            // 3.4. Crear y guardar el nuevo contrato.
            // Las propiedades aquí se basan en tu modelo 'Contract.cs'.
            var newContract = new Contract
            {
                // Asignamos la llave foránea al suscriptor que acabamos de crear.
                IdSubscriber = newSubscriber.IdSubscriber,
                StartTime = DateTime.Now,
                // Asignamos una fecha de fin de ejemplo.
                EndTime = DateTime.Now.AddYears(1), 
                PriceContract = contractPrice
            };
            
            _contractRepository.AddContract(newContract);
            _contractRepository.SaveChanges();

            // 3.5. Asociar paquetes al contrato.
            if (altaRequest.PackageIds != null && altaRequest.PackageIds.Any())
            {
                foreach (var packageId in altaRequest.PackageIds)
                {
                    var contractPackage = new ContractPackage
                    {
                        // Asignamos las llaves foráneas a la tabla de unión.
                        IdContract = newContract.IdContract,
                        IdPackage = packageId,
                        AddedTime = DateTime.Now
                    };
                    _contractRepository.AddContractPackage(contractPackage);
                }
                _contractRepository.SaveChanges();
            }

            // 3.6. Retornar una respuesta de éxito con el suscriptor creado.
            return CreatedAtAction(nameof(GetSubscriber), new { id = newSubscriber.IdSubscriber }, newSubscriber);
        }

        // Puedes agregar más endpoints aquí, como Put para actualizar o Get para obtener todos los suscriptores.
    }

    // ----------------------------------------------------------------------
    // 4. DATA TRANSFER OBJECT (DTO)
    //    Clase para manejar los datos de entrada de la API.
    // ----------------------------------------------------------------------
    public class RegisterSubscriberWithContractRequest
    {
        public Subscriber Subscriber { get; set; }
        public int? IdManualPromotion { get; set; }
        public decimal BaseContractPrice { get; set; }
        public List<int>? PackageIds { get; set; }
    }
}