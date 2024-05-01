using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OeuilDeSauron.Data.Items
{
    /// <summary>
    /// An item is a reference that can be
    /// used in other entities.
    /// </summary>
    public class Item : Entity
    {
        /// <summary>
        /// Gets item id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets item name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets item code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Items extra data. Used for zip codes in cities.
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Gets or sets item extra data. Used only when mapping.
        /// </summary>
        public string Extra { get; set; }

        [NotMapped]
        /// <summary>
        /// Gets or sets item parents, if any.
        /// </summary>
        public virtual ICollection<ItemRelation> Parents { get; set; }

        [NotMapped]
        /// <summary>
        /// Gets or sets item Children, if any.
        /// </summary>
        public virtual ICollection<ItemRelation> Children { get; set; }

        /// <summary>
        /// Gets item parent list, if any.
        /// </summary>
        public int ListId { get; private set; }

        /// <summary>
        /// Gets or sets item list.
        /// </summary>
        public virtual List List { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item(int id, string name, string code, string data, int listId) : this()
        {
            Id = id;
            Name = name;
            Code = code;
            Data = data;
            ListId = listId;
        }

        internal Item()
        {
            Parents = new Collection<ItemRelation>();
            Children = new Collection<ItemRelation>();
        }

        /// <summary>
        /// Sets item name.
        /// </summary>
        /// <param name="name">New name.</param>
        public void SetName(string name) =>
            Name = name;

        /// <summary>
        /// Sets item list id.
        /// </summary>
        /// <param name="listId">List id.</param>
        public void SetListId(int listId) =>
            ListId = listId;

        public void Update(Item updated)
        {
            Name = updated.Name;
            ListId = updated.ListId;
        }

        /// <summary>
        /// Sets item parent.
        /// </summary>
        /// <param name="parents">Parents.</param>
        public void SetParents(List<ItemRelation> parents)
        {
            Parents = parents;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is Item item &&
            EqualityComparer<string>.Default.Equals(Code, item.Code) &&
            EqualityComparer<string>.Default.Equals(Name, item.Name) &&
            EqualityComparer<string>.Default.Equals(Data, item.Data) &&
            EqualityComparer<int>.Default.Equals(ListId, item.ListId);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            EqualityComparer<string>.Default.GetHashCode(Code) ^
            EqualityComparer<string>.Default.GetHashCode(Name) ^
            EqualityComparer<string>.Default.GetHashCode(Data) ^
            EqualityComparer<int>.Default.GetHashCode(ListId);
    }
}
