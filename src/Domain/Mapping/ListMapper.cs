using AutoMapper;

using OeuilDeSauron.Data.Items;
using OeuilDeSauron.Domain.Models;

namespace OeuilDeSauron.Domain.Mapping
{
    public class ListMapper : Profile
    {
        public ListMapper()
        {
            // Entity -> Model
            CreateMap<Item, ItemModel>();
            CreateMap<List, ListModel>();
            CreateMap<ItemRelation, ItemRelationModel>();
            // Model -> Entity
            CreateMap<ItemModel, Item>();
            CreateMap<ListModel, List>();
            CreateMap<ItemRelationModel, ItemRelation>();
        }
    }
}
