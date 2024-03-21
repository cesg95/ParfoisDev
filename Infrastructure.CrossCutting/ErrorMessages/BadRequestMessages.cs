namespace Infrastructure.CrossCutting.ErrorMessages
{
    using System.ComponentModel;

    public enum BadRequestMessages
    {
        [Description("Invalid identifier.")]
        InvalidIdentifier = 0,

        [Description("Invalid request.")]
        InvalidRequest = 1,

        [Description("Identifier must be an integer.")]
        IdentifierMustBeAnInteger = 2,

        [Description("Identifier must be equal to request parameter.")]
        IdentifierMustBeEqualToRequestParameter = 3,

        [Description("Request must have at least one item.")]
        RequestMustHaveAtLeastOneItem = 4,

        [Description("Item must have a description.")]
        ItemMustHaveDescription = 5,

        [Description("PrecoUnitario cannot be negative.")]
        PrecoUnitarioCannotBeNegative = 6,

        [Description("Qtd cannot be negative.")]
        QtdCannotBeNegative = 7,

        [Description("Invalid status.")]
        InvalidStatus = 8,

        [Description("ItensAprovados cannot be negative.")]
        ItensAprovadosCannotBeNegative = 9,

        [Description("ValorAprovado cannot be negative.")]
        ValorAprovadoCannotBeNegative = 10,
    }
}
