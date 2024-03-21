namespace Domain.Model.Enums
{
    using System.ComponentModel;

    public enum Status
    {
        [Description("APROVADO")]
        Aprovado = 0,

        [Description("APROVADO_VALOR_A_MENOR")]
        AprovadoValorAMenor = 1,

        [Description("APROVADO_VALOR_A_MAIOR")]
        AprovadoValorAMaior = 2,

        [Description("APROVADO_QTD_A_MENOR")]
        AprovadoQtdAMenor = 3,

        [Description("APROVADO_QTD_A_MAIOR")]
        AprovadoQtdAMaior = 4,

        [Description("REPROVADO")]
        Reprovado = 5,

        [Description("CODIGO_PEDIDO_INVALIDO")]
        CodigoPedidoInvalido = 6,
    }
}
