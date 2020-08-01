using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SailingActors.Api
{
    public interface ICartIdGenerator{
        public string New();
    }
    public class CartIdGenerator : ICartIdGenerator
    {
        public string New()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            //From = https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[r.Next(s.Length)]).ToArray());
        }
    }
}
