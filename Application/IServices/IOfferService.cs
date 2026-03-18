using Domain.Entities.Concrete;

namespace Application.IServices;

public interface IOfferService
{
    List<Offer> GetAllOffers();
    Offer? GetOfferById(string offerId);
    List<Offer> GetOffersByPlayerId(string playerId);
    List<Offer> GetOffersByTeamId(string teamId);
    Task  CreateOfferAsync(Offer offer);
    Task  AcceptOfferAsync(string offerId);
    Task  RejectOfferAsync(string offerId);
    Task  UpdateOfferAsync(Offer offer);
    Task  DeleteOfferAsync(string offerId);
}