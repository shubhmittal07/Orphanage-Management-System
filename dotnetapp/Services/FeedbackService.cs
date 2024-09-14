using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class FeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return await _context.Feedbacks.Include(f=>f.User).ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserId(int userId)
        {
            return await _context.Feedbacks.Where(p=>p.UserId==userId).ToListAsync();
        }

        public async Task<bool> AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            var res = await _context.SaveChangesAsync();
            return res>0;
        }

        public async Task<bool> DeleteFeedback(int feedbackId)
        {
            var res = await _context.Feedbacks.FindAsync(feedbackId);

            if(res == null)
            {
                return false;
            }

            _context.Feedbacks.Remove(res);
            var save = await _context.SaveChangesAsync();
            return true;
        }
    }
}