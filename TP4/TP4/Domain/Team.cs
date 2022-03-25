namespace Domain
{
    public class Team
    {
        public string? Nome { get; set; }
        public string? Estado { get; set; }
        public int TitulosBrasileiros { get; set; }
        public int TitulosEstaduais { get; set; }
        public DateTime DataCriacao { get; set; }

        public int tempoAtivo()
        {
            int now = DateTime.Now.Year;
            int dataCriacao = now - DataCriacao.Year;
            return dataCriacao;
        }
    }
}