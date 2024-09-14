using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Donation
    {
        public int DonationId {get;set;}
        public int? OrphanageId {get;set;}
        // [JsonIgnore]
        public Orphanage? Orphanage {get;set;}
        public decimal Amount {get;set;}
        public DateTime DonationDate {get;set;}
        public int UserId {get;set;}
        // [JsonIgnore]
        public User? User {get;set;}
    }
}