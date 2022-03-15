namespace Domain
{
    public class Teams
    {
        public string? Nome { get; private set; }
        public string? Estado { get; private set; }
        public int TitulosBrasileiros { get; private set; }
        public int TitulosEstaduais { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public int tempoAtivo()
        {
            int now = DateTime.Now.Year;
            int dataCriacao = now - DataCriacao.Year;
            return dataCriacao;
        }
    }
}
