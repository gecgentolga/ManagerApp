using Application.IServices;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class OfferController : Controller
{
    private IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpGet("GetAllOffers")]
    public IActionResult GetAllOffers()
    {
        
        var offers = _offerService.GetAllOffers();
        return Ok(offers);
    }

    [HttpGet("OfferById")]
    public IActionResult GetOfferById(string offerId)
    {
        var offer = _offerService.GetOfferById(offerId);
        if (offer == null)
            return NotFound();

        return Ok(offer);
    }

    [HttpPost("CreateOffer")]
    public IActionResult CreateOffer([FromBody] Offer offer)
    {
        _offerService.CreateOfferAsync(offer);
        return CreatedAtAction(nameof(GetOfferById), new { offerId = offer.OfferId }, offer);
    }

    [HttpPost("{offerId}/accept")]
    public IActionResult AcceptOffer(string offerId)
    {
        try
        {
            _offerService.AcceptOfferAsync(offerId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{offerId}/reject")]
    public IActionResult RejectOffer(string offerId)
    {
        try
        {
            _offerService.RejectOfferAsync(offerId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("UpdateOffer")]
    public async Task<IActionResult> UpdateOffer([FromBody] Offer offer)
    {
        try
        {
            await _offerService.UpdateOfferAsync(offer);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();

        }
    }

    [HttpDelete("{offerId}")]
    public async Task<IActionResult> DeleteOffer(string offerId)
    {
        try
        {
            await _offerService.DeleteOfferAsync(offerId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();

        }

    }
}

