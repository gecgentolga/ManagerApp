
using Domain.Entities.Concrete;

namespace Application.IServices;

public interface IContractService
{
    List<Contract> GetContracts();
    Contract? GetContractById(int contractId);
    List<Contract> GetContractsByPlayerId(string playerId);
    
    List<Contract> GetContractsByManagerId(string managerId);
    Task CreateContractAsync(Contract contract);
    Task UpdateContractAsync(Contract contract);
    Task DeleteContractAsync(int contractId);
}