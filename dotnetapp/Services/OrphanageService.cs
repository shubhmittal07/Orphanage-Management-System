using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Exceptions;

namespace dotnetapp.Services
{
    public class OrphanageService
    {
        private readonly ApplicationDbContext _context;
        public OrphanageService(ApplicationDbContext context){
            _context=context;
        }

        public async Task<IEnumerable<Orphanage>> GetAllOrphanages(){
            return await _context.Orphanages.ToListAsync();
        }

        public async Task<Orphanage> GetOrphanageById(int orphanageId)
        {
            return await _context.Orphanages.FindAsync(orphanageId);
        }

        public async Task<bool> AddOrphanage(Orphanage orphanage)
        {
            if(await _context.Orphanages.AnyAsync(o=>o.OrphanageName==orphanage.OrphanageName)){
                throw new OrphanageException("Orphanage with the same name already exists");
            }
            _context.Orphanages.Add(orphanage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrphanage(int orphanageId, Orphanage orphanage){
            var existingOrphanage= await _context.Orphanages.FindAsync(orphanageId);
            if(existingOrphanage==null){
                return false;
            }
            existingOrphanage.OrphanageName=orphanage.OrphanageName;
            existingOrphanage.Description=orphanage.Description;
            existingOrphanage.Founder=orphanage.Founder;
            existingOrphanage.EstablishmentDate=orphanage.EstablishmentDate;
            existingOrphanage.Status=orphanage.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrphanage(int orphanageId){
            var orphanage=await _context.Orphanages.FindAsync(orphanageId);
            _context.Orphanages.Remove(orphanage);
            await _context.SaveChangesAsync();
            return true;
        }  
    }
}