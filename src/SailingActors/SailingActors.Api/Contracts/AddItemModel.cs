using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SailingActors.Api.Contracts
{
    public class AddItemModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
