using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class DonationService
    {
     private readonly ApplicationDbContext _context;

    public DonationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Donation>> GetAllDonations()
    {
        return await _context.Donations.Include(d => d.User).Include(d => d.Orphanage).ToListAsync();
    }

    public async Task<IEnumerable<Donation>> GetDonationsByUserId(int userId)
    {       
       return await _context.Donations.Include(d=>d.Orphanage).Where(d=>d.UserId==userId).ToListAsync();
    }

    public async Task<bool> AddDonation(Donation donation)
    {
       _context.Donations.Add(donation);
       await _context.SaveChangesAsync();
       return true;
    }
  
    public async Task<IEnumerable<Donation>> GetDonationsByOrphanageId(int orphanageId)
    {
       return await _context.Donations.Where(d=>d.OrphanageId==orphanageId).Include(d=>d.User).Include(d=>d.Orphanage).ToListAsync();
    }
    
    public async Task<bool> DeleteDonationsByOrphanageId(int orphanageId)
    {
        var donationsByOID = await _context.Donations
            .Where(d => d.OrphanageId == orphanageId)
            .Include(d => d.User)
            .Include(d => d.Orphanage)
            .ToListAsync();
        _context.Donations.RemoveRange(donationsByOID);
        await _context.SaveChangesAsync();
        return true;
    }



    
    
    }
}



