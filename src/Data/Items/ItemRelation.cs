using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OeuilDeSauron.Data.Items
{
    public class ItemRelation
    {
        public int Id { get; private set; }
        public int ParentId { get; private set; }

        [NotMapped]
        public Item Parent { get; set; }

        public int ChildId { get; private set; }

        [NotMapped]
        public Item Child { get; set; }

        public ItemRelation(int childId, int parentId)
        {
            ChildId = childId;
            ParentId = parentId;
        }
        public override bool Equals(object obj) =>
            obj is ItemRelation item &&
            EqualityComparer<int>.Default.Equals(ChildId, item.ChildId) &&
            EqualityComparer<int>.Default.Equals(ParentId, item.ParentId);

        public override int GetHashCode() =>
            EqualityComparer<int>.Default.GetHashCode(ChildId) ^
            EqualityComparer<int>.Default.GetHashCode(ParentId);
    }
}
