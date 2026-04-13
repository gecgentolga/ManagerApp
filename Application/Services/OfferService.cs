using System.Transactions;
using Application.IServices;
using Application.DataAccess;
using Domain.Entities.Concrete;
using Domain.Entities.DTOs;

namespace Application.Services;

public class OfferService : IOfferService
{
    IOfferDal _offerDal;
    IContractDal _contractDal;

    public OfferService(IOfferDal offerDal, IContractDal contractDal)
    {
        _offerDal = offerDal;
        _contractDal = contractDal;
    }

    public List<Offer> GetAllOffers()
    {
        return _offerDal.GetAll();
    }

    public Offer? GetOfferById(int offerId)
    {
        return _offerDal.Get(o => o.OfferId == offerId);
    }

    public List<Offer> GetOffersByPlayerId(string playerId)
    {
        return _offerDal.GetAll(o => o.PlayerId == playerId);
    }

    public List<Offer> GetOffersByManagerId(int managerId)
    {
        return _offerDal.GetAll(o => o.ManagerId == managerId);
    }

    public async Task  CreateOfferAsync(OfferDetailDto offerDto)
    {
        var offer = new Offer
        {
            PlayerId = offerDto.PlayerId,
            ManagerId = offerDto.ManagerId,
            CommissionRate = offerDto.CommissionRate,
            ContractPeriodMonths = offerDto.ContractPeriodMonths,
            OfferStatus = true // Yeni teklifler varsayılan olarak aktif kabul edilir
        };
        _offerDal.Add(offer);
        await _offerDal.SaveAsync();
    }

    public async Task AcceptOfferAsync(int offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");

        // İş kuralı: Teklif kabul edildiğinde yeni bir Contract oluşturulmalı
        var contract = new Contract
        {
            CommissionRate = offer.CommissionRate,
            PlayerId = offer.PlayerId,
            ManagerId = offer.ManagerId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(offer.ContractPeriodMonths)
        };

        _contractDal.Add(contract);
        _offerDal.Delete(offer);
        await _offerDal.SaveAsync();
        
    }

    public async Task  RejectOfferAsync(int offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");

        // İş kuralı: Teklif reddedildiğinde OfferStatus = false
        offer.OfferStatus = false;
        _offerDal.Update(offer);
        await _offerDal.SaveAsync();
    }

    public async Task UpdateOfferAsync(Offer offer)
    {
        var existing = _offerDal.Get(o => o.OfferId == offer.OfferId);
        if (existing == null)
            throw new InvalidOperationException($"Offer {offer.OfferId} not found.");

        _offerDal.Update(offer);
        await _offerDal.SaveAsync();
    }

    public async Task DeleteOfferAsync(int offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");

        _offerDal.Delete(offer);
        await _offerDal.SaveAsync();
    }
}