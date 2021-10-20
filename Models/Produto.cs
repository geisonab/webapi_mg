using Dapper.Contrib.Extensions;

namespace WebAPI_MG.Models
{
    [Table("tbSLN_Mobile_PesquisaPreco_Produtos")]
    public class Produto
    {
        [ExplicitKey]
        public int CPD { get; set; }
        public string PRODUTO { get; set; }
        public string REFERENCIA { get; set; }
        public string DESCRICAO { get; set; }
        public string CLASS { get; set; }
        public int FAMILIA { get; set; }
        public decimal PRECO { get; set; }
    }
}