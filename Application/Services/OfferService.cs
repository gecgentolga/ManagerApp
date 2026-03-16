using Application.IServices;
using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Application.Services;

public class OfferService : IOfferService
{
    IOfferDal _offerDal;

    public OfferService(IOfferDal offerDal)
    {
        _offerDal = offerDal;
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

    public void CreateOffer(Offer offer)
    {
        // İş kuralı: Yeni teklif varsayılan olarak pending (false) olmalı
        offer.OfferStatus = false;
        _offerDal.Add(offer);
    }

    public void AcceptOffer(string offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");
        
        // İş kuralı: Teklif kabul edildiğinde OfferStatus = true
        offer.OfferStatus = true;
        _offerDal.Update(offer);
    }

    public void RejectOffer(string offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");
        
        // İş kuralı: Teklif reddedildiğinde OfferStatus = false
        offer.OfferStatus = false;
        _offerDal.Update(offer);
    }

    public void UpdateOffer(Offer offer)
    {
        var existing = _offerDal.Get(o => o.OfferId == offer.OfferId);
        if (existing == null)
            throw new InvalidOperationException($"Offer {offer.OfferId} not found.");
        
        _offerDal.Update(offer);
    }

    public void DeleteOffer(string offerId)
    {
        var offer = _offerDal.Get(o => o.OfferId == offerId);
        if (offer == null)
            throw new InvalidOperationException($"Offer {offerId} not found.");
        
        _offerDal.Delete(offer);
    }
}