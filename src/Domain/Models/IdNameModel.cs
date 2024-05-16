namespace OeuilDeSauron.Domain.Models
{
    public class IdNameModel
    {
        public object Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public object Parents { get; set; }

        public IdNameModel(object id, string name)
        {
            Id = id;
            Name = name;
        }

        public IdNameModel(object id, string name, string description): this (id, name) =>
            Description = description;

        public IdNameModel(object id, string name, object parents = default) : this(id, name, default) =>
            Parents = parents;
    }
}
