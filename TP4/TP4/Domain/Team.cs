namespace Domain
{
    public class Teams
    {
        public string? Nome { get; set; }
        public byte TitulosMundiais { get; set; }
        public int TitulosBrasileiros { get; set; }
        public DateOnly DataCriacao { get; set; }

        public int tempoAtivo()
        {
            int now = DateTime.Now.Year;
            int dataCriacao = now - DataCriacao.Year;
            return dataCriacao;
        }
    }
}