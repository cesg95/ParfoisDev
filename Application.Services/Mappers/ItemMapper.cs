namespace Application.Services.Mappers
{
    public static class ItemMapper
    {
        public static IEnumerable<Dto.Item> ToDto(this IEnumerable<Domain.Model.Item> items)
        {
            if (items == null)
            {
                return new List<Dto.Item>();
            }

            return items.Select(ToDto);
        }

        public static Dto.Item ToDto(this Domain.Model.Item item)
        {
            if (item == null)
            {
                return null;
            }

            return new Dto.Item
            {
                Descricao = item.Descricao,
                PrecoUnitario = item.PrecoUnitario,
                Qtd = item.Qtd,
            };
        }

        public static IEnumerable<Domain.Model.Item> ToModel(this IEnumerable<Dto.Item> items)
        {
            if (items == null)
            {
                return new List<Domain.Model.Item>();
            }

            return items.Select(ToModel);
        }

        public static Domain.Model.Item ToModel(this Dto.Item item)
        {
            if (item == null)
            {
                return null;
            }

            return new Domain.Model.Item
            {
                Descricao = item.Descricao,
                PrecoUnitario = item.PrecoUnitario,
                Qtd = item.Qtd,
            };
        }
    }
}
