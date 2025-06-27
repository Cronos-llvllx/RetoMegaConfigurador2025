using megaapi.Interfaces;
using megaapi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace megaapi.Repositories
{
    public class ContractRepository : IContractRepository
    {
        // SIMULACIÓN DE LA BASE DE DATOS EN MEMORIA
        private static List<Contract> _contracts = new List<Contract>();
        private static List<ContractPackage> _contractPackages = new List<ContractPackage>();
        private static int _nextContractId = 1; // Para simular el IDENTITY
        private static int _nextPackageContractId = 1;

        // El constructor ya no necesita el ApplicationDbContext
        public ContractRepository()
        {
            // Opcional: inicializa algunos datos de prueba
            if (!_contracts.Any())
            {
                // Agrega datos para pruebas iniciales
                _contracts.Add(new Contract { IdContract = _nextContractId++,  StartTime = new DateTime(2024, 1, 1), PriceContract = 599.99m });
            }
        }
        // Implementación de los métodos usando las listas
        public void AddContract(Contract contract)
        {
            // Simula el IDENTITY de la base de datos
            contract.IdContract = _nextContractId++;
            _contracts.Add(contract);
        }

        public void AddContractPackage(ContractPackage contractPackage)
        {
            _contractPackages.Add(contractPackage);
        }

        public Contract? GetContractById(int id)
        {
            // Usa LINQ para buscar en la lista
            return _contracts.FirstOrDefault(c => c.IdContract == id);
        }

        public IEnumerable<ContractPackage> GetPackagesByContractId(int contractId)
        {
            // Filtra y devuelve los datos de la lista
            return _contractPackages.Where(cp => cp.IdContract == contractId && cp.RemovedTime == null);
        }

        public void UpdatePackageRemovedTime(int contractId, int packageId, DateTime removedTime)
        {
            var contractPackage = _contractPackages.FirstOrDefault(cp => cp.IdContract == contractId && cp.IdPackage == packageId);
            if (contractPackage != null)
            {
                contractPackage.RemovedTime = removedTime;
            }
        }

        public void SaveChanges()
        {
            // No hace nada en esta versión, ya que los cambios se guardan en la lista.
            // Es un método "no-op" (no-operation) pero lo mantenemos para el contrato de la interfaz.
        }
    }
}