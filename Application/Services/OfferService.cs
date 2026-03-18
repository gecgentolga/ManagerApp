using System.Transactions;
using Application.IServices;
using Application.DataAccess;
using Domain.Entities.Concrete;

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

    public Offer? GetOfferById(string offerId)
    {
        return _offerDal.Get(o => o.OfferId == offerId);
    }

    public List<Offer> GetOffersByPlayerId(string playerId)
    {
        return _offerDal.GetAll(o => o.PlayerId == playerId);
    }

    public List<Offer> GetOffersByTeamId(string teamId)
    {
        return _offerDal.GetAll(o => o.TeamId == teamId);
    }

    public async Task  CreateOfferAsync(Offer offer)
    {
        // İş kuralı: Yeni teklif varsayılan olarak pending (false) olmalı
        offer.OfferStatus = false;
        _offerDal.Add(offer);
        await _offerDal.SaveAsync();
    }

    public async Task AcceptOfferAsync(string offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");

        // İş kuralı: Teklif kabul edildiğinde yeni bir Contract oluşturulmalı
        var contract = new Contract
        {
            CommissionRate = offer.CommissionRate,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(offer.ContractPeriodMonths)
        };

        _contractDal.Add(contract);
        _offerDal.Delete(offer);
        await _offerDal.SaveAsync();
        
    }

    public async Task  RejectOfferAsync(string offerId)
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

    public async Task DeleteOfferAsync(string offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");

        _offerDal.Delete(offer);
        await _offerDal.SaveAsync();
    }
}