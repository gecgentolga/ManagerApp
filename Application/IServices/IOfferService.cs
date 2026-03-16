using Domain.Entities.Concrete;

namespace Application.IServices;

public interface IOfferService
{
    List<Offer> GetAllOffers();
    Offer? GetOfferById(string offerId);
    List<Offer> GetOffersByPlayerId(string playerId);
    List<Offer> GetOffersByTeamId(string teamId);
    void CreateOffer(Offer offer);
    void AcceptOffer(string offerId);
    void RejectOffer(string offerId);
    void UpdateOffer(Offer offer);
    void DeleteOffer(string offerId);
}