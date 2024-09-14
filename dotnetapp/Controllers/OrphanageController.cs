using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/orphanages")]
   
    public class OrphanageController : ControllerBase
    {
        private readonly OrphanageService _service;
        private readonly DonationService _dservice;

        public OrphanageController(OrphanageService orphanageService,DonationService donationService)
        {
            _service = orphanageService;
            _dservice = donationService;

        }
        
        [HttpGet]
        [Authorize(Roles = "Admin,User")]

        public async Task<ActionResult<IEnumerable<Orphanage>>> GetAllOrphanages()
        {
            var orphanages = await _service.GetAllOrphanages();

            return Ok(orphanages);
            
        }

        [HttpGet("{orphanageId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Orphanage>> GetOrphanageById(int orphanageId)
        {
            var orphanage = _service.GetOrphanageById(orphanageId).Result;

            if (orphanage != null)
            {
                return Ok(orphanage);

            }
            else
            {
                return NotFound("Cannot find any orphanage");
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddOrphanage([FromBody] Orphanage orphanage)
        {
            try
            {
                var result = await _service.AddOrphanage(orphanage);

                if (result != null)
                {
                    return Ok("Orphanage added sucessfully");
                }


                return StatusCode(500, "Failed to add orphanage");

            }
            catch (OrphanageException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{orphanageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateOrphanage(int orphanageId, [FromBody] Orphanage orphanage)
        {
            try
            {
                var result = await _service.UpdateOrphanage(orphanageId, orphanage);
                if (result)
                {
                    return Ok("Orphanage updated sucessfully");
                }
                return NotFound("Cannot find any orphanage");
            }
            catch (OrphanageException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{orphanageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteOrphanage(int orphanageId)
        {
            var res1 = await _dservice.DeleteDonationsByOrphanageId(orphanageId);
            if(!res1) res1 = true;
            try
            {
                var result = await _service.DeleteOrphanage(orphanageId);
                if (result)
                {
                    return Ok(new{message="Orphanage deleted sucessfully"});
                }
                return NotFound(new{message="Cannot find any orphanage"});
            }
            catch (OrphanageException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        

    }
}