using System.Collections.Generic;

namespace OeuilDeSauron.Domain.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int ListId { get; set; }

        public ListModel List { get; set; }

        public IList<ItemRelationModel> Children { get; set; }

        public IList<ItemRelationModel> Parents { get; set; }
    }
}
