namespace NexusGym.Domain
{
    public class PlanoCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int PlanoId { get; set; }
        public Plano Plano { get; set; }
        public Cliente Cliente { get; set; }

        public bool Ativo { get; set; } = true;
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; } 


        public void Atualizar(int planoId)
        {
            this.PlanoId = planoId;
        }

        public void Inativar()
        {
            this.Ativo = false;
        }
    }
}
