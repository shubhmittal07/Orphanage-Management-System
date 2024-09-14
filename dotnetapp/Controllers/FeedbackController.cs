using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using dotnetapp.Services;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;
        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedbacks()
        {
            try
            {
                var req = await _feedbackService.GetAllFeedbacks();
                return Ok(req);
            }catch(Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbackByUserId(int userId)
        {
            try
            {
                var req = await _feedbackService.GetFeedbacksByUserId(userId);
                if(req == null || !req.Any())
                {
                    return NotFound("No Feedbacks Were Found For This User");
                }
                return Ok(req);
            }catch(Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AddFeedback([FromBody] Feedback feedback)
        {
            try
            {
                var req = await _feedbackService.AddFeedback(feedback);
                if(req)
                {
                    return Ok(new{status="Feedback added successfully"});
                }
                return BadRequest("Failed to add feedback");
            }catch(Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete("{feedbackId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> DeleteFeedback(int feedbackId)
        {
            try
            {
                var req = await _feedbackService.DeleteFeedback(feedbackId);
                if(req)
                {
                    return Ok(new{status="Feedback deleted successfully"});
                }
                return NotFound(new {status="Cannot find any feedback"});
            }catch(Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}