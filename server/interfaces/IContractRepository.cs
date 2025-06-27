using megaapi.Models; // Para usar las clases Contrato y ContratoPaquete
using System;
using System.Collections.Generic;

namespace megaapi.Interfaces
{
    public interface IContractRepository
    {
                // Métodos para gestionar Contratos
        Contract? GetContractById(int id);
        void AddContract(Contract contract);

        // Métodos para gestionar la relación ContratoPaquete
        void AddContractPackage(ContractPackage contractPackage);
        IEnumerable<ContractPackage> GetPackagesByContractId(int contractId);
        void UpdatePackageRemovedTime(int contractId, int packageId, DateTime removedTime);

        // Método para guardar los cambios
        void SaveChanges();
    }
}