using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Services;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;


namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/donations")]
    public class DonationController : ControllerBase
    {
        public DonationService _donationService;

        public DonationController(DonationService ds)
        {
            _donationService = ds;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donation>>> GetAllDonations()
        {
            try
            {
                var res = await _donationService.GetAllDonations();
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Failed to get alldonations");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AddDonation([FromBody] Donation donation)
        {
            try
            {
                await _donationService.AddDonation(donation);
                return Ok(new { message = "Donation added successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, "Failed to add donation");
            }
        }

        [HttpGet("User/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Donation>>> GetDonationsByUserId(int userId)
        {
            try
            {
                var Uid = await _donationService.GetDonationsByUserId(userId);
                return Ok(Uid);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Failed to get donation by userId");
            }
        }

        [HttpGet("orphanages/{orphanageId}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<Donation>>> GetDonationsByOrphanageId(int OrphanageId)
        {
            try
            {
                var Oid = await _donationService.GetDonationsByOrphanageId(OrphanageId);
                return Ok(Oid);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Failed to get donation by orphanageId");
            }
        }

    }
}