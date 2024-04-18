using System.Collections.Generic;

namespace siwar.Data.Items
{
    public class ItemRelation
    {
        public int ParentId { get; private set; }

        public Item Parent { get; set; }

        public int ChildId { get; private set; }

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
