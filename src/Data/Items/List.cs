using System.Collections.Generic;

namespace OeuilDeSauron.Data.Items
{
    /// <summary>
    /// Represents a group of items.
    /// </summary>
    public class List : Entity, IAggregateRoot
    {
        /// <summary>
        /// Gets list id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets list name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets whether the list is editable or not.
        /// </summary>
        public bool Editable { get; private set; }

        private List<Item> _items;
        /// <summary>
        /// Gets list items.
        /// </summary>
        public virtual IReadOnlyCollection<Item> Items => _items.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class.
        /// </summary>
        public List(int id, string name, bool editable = true)
        {
            Id = id;
            Name = name;
            Editable = editable;

            _items = new List<Item>();
        }

        /// <summary>
        /// Adds a collection of items to the current list.
        /// </summary>
        /// <param name="items">Items to add.</param>
        public void AddItems(IList<Item> items) =>
            _items.AddRange(items);
    }
}
