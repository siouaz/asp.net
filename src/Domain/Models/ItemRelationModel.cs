using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siwar.Domain.Models
{
    public class ItemRelationModel
    {
        public int ParentId { get; set; }

        public ItemModel Parent { get; set; }

        public int ChildId { get; set; }

        public ItemModel Child { get; set; }

    }
}
