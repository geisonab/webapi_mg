using Flunt.Notifications;
using Flunt.Validations;

namespace WebAPI_MG.ViewModels
{
    public class ViewModelProdutosEditor : Notifiable , IValidatable
    {
        public int CPD { get; set; }
        public string PRODUTO { get; set; }
        public string REFERENCIA { get; set; }
        public string DESCRICAO { get; set; } 
        public string CLASS { get; set; }    
        public int FAMILIA { get; set; }
        public decimal PRECO { get; set; }

        public void Validate()
        {
            if(FAMILIA == 0)
                AddNotification("FAMILIA","Família inválida");
            AddNotifications(
                new Contract()
                    .HasMinLen(PRODUTO,1,"PRODUTO","O produto deve ser preenchido.")
                    .HasMaxLen(CLASS,2,"CLASS","A classificação deve conter no máximo 2 caracteres.")
            );
        }
    }
}