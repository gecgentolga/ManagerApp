using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Concrete;

namespace Application.Services;

public class ContractService:IContractService
{
    private IContractDal _contractDal;

    public ContractService(IContractDal contractDal)
    {
        _contractDal = contractDal;
    }
    public List<Contract> GetContracts()
    {
        return _contractDal.GetAll();
    }

    public Contract? GetContractById(int contractId)
    {
        return _contractDal.Get(o=>o.ContractId==contractId);
    }

    public List<Contract> GetContractsByPlayerId(string playerId)
    {
        return _contractDal.GetAll(o => o.PlayerId == playerId);
    }

    public List<Contract> GetContractsByManagerId(string managerId)
    {
        return _contractDal.GetAll(o => o.ManagerId == managerId);
    }
    

    public async Task CreateContractAsync(Contract contract)
    {
        _contractDal.Add(contract);
        await _contractDal.SaveAsync();
    }

    public async Task UpdateContractAsync(Contract contract)
    {
        _contractDal.Update(contract);
        await _contractDal.SaveAsync();
    }

    public async Task DeleteContractAsync(int contractId)
    {
        var contract = _contractDal.Get(o => o.ContractId == contractId);
        if (contract == null)
            throw new InvalidOperationException($"Contract {contractId} not found.");

        _contractDal.Delete(contract);
        await _contractDal.SaveAsync();
    }
}