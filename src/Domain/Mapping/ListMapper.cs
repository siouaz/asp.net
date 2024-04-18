using AutoMapper;

using siwar.Data.Items;
using siwar.Domain.Models;

namespace siwar.Domain.Mapping
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
