using Application.IServices;
using Domain.Entities.Concrete;
using Domain.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin")]
    [HttpGet("GetAllOffers")]
    public IActionResult GetAllOffers()
    {
        
        var offers = _offerService.GetAllOffers();
        return Ok(offers);
    }

    [Authorize(Roles="Admin")]
    [HttpGet("OfferById")]
    public IActionResult GetOfferById(int offerId)
    {
        var offer = _offerService.GetOfferById(offerId);
        if (offer == null)
            return NotFound();

        return Ok(offer);
    }
    [Authorize(Roles = "Admin,Player")]
    [HttpGet("OffersByPlayerId")]
    public IActionResult GetOffersByPlayerId(string playerId)
    {
        var offers = _offerService.GetOffersByPlayerId(playerId);
        return Ok(offers);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("OffersByManagerId")]
    public IActionResult GetOffersByManagerId(int managerId)
    {
        var offers=_offerService.GetOffersByManagerId(managerId);
        return Ok(offers);
    }
    [Authorize(Roles="Manager,Admin")]
    [HttpPost("CreateOffer")]
    public async Task<IActionResult> CreateOffer([FromBody] OfferDetailDto offerDetailDto)
    {
        await _offerService.CreateOfferAsync(offerDetailDto);
        return Ok("Offer created successfully");
    }

    [Authorize(Roles = "Player,Admin")]
    [HttpPost("{offerId}/accept")]
    public async Task<IActionResult> AcceptOffer(int offerId)
    {
        try
        {
           await _offerService.AcceptOfferAsync(offerId);
            return Ok("Offer accepted and contract created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = "Player,Admin")]
    [HttpPost("{offerId}/reject")]
    public async Task<IActionResult> RejectOffer(int offerId)
    {
        try
        {
           await _offerService.RejectOfferAsync(offerId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = "Admin,Manager")]
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

    [Authorize(Roles = "Admin,Manager")]
    [HttpDelete("{offerId}")]
    public async Task<IActionResult> DeleteOffer(int offerId)
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

