using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Exceptions
{
    public class OrphanageException : Exception
    {
        public OrphanageException(string message) : base(message) {
            
        }
    }
}