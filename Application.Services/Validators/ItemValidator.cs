namespace Application.Services.Validators
{
    using Application.Dto;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    public static class ItemValidator
    {
        public static void Validate(this Item item)
        {
            if (item == null)
            {
                throw new BadRequestException(BadRequestMessages.InvalidItem.ToMessage());
            }

            if (string.IsNullOrWhiteSpace(item.Descricao))
            {
                throw new BadRequestException(BadRequestMessages.ItemMustHaveDescription.ToMessage());
            }

            if (item.PrecoUnitario < 0)
            {
                throw new BadRequestException(BadRequestMessages.PrecoUnitarioCannotBeNegative.ToMessage());
            }

            if (item.Qtd < 0)
            {
                throw new BadRequestException(BadRequestMessages.QtdCannotBeNegative.ToMessage());
            }
        }
    }
}
