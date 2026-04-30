using Domain.Entities.Concrete;
using Domain.Entities.DTOs;

namespace Application.IServices;

public interface IOfferService
{
    List<Offer> GetAllOffers();
    Offer? GetOfferById(int offerId);
    List<Offer> GetOffersByPlayerId(string playerId);
    List<Offer> GetOffersByManagerId(int managerId);
    Task  CreateOfferAsync(OfferDetailDto offerDetailDto);
    Task  AcceptOfferAsync(int offerId);
    Task  RejectOfferAsync(int offerId);
    Task  UpdateOfferAsync(Offer offer);
    Task  DeleteOfferAsync(int offerId);
}