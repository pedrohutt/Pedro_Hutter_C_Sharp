namespace Domain
{
    public class Team
    {
        public Team(int id, string nome, byte titulosM, short titulosBR, DateOnly dataC)
        {
            Id = id;
            Nome = nome;
            TitulosMundiais = titulosM;
            TitulosBrasileiros = titulosBR;
            DataCriacao = dataC;
        }

        public int Id { get; set; } 
        public string? Nome { get; set; }
        public byte TitulosMundiais { get; set; }
        public short TitulosBrasileiros { get; set; }
        public DateOnly DataCriacao { get; set; }

        public int tempoAtivo()
        {
            int now = DateTime.Now.Year;
            int dataCriacao = now - DataCriacao.Year;
            return dataCriacao;
        }
        public string ShowTeamInfo()
        {
            string teamInfo = $"\n Nome do Time: {Nome}" +
                              $"\n Titulos Brasileiros {TitulosBrasileiros} - Titulos Mundiais: {TitulosMundiais}" +
                              $"\n O Clube já possui {tempoAtivo()} anos de história!\n" +
                              $"\n GUID - {Id}\n";
            return teamInfo;
        }
        public override string ToString()
        {
            return $"{Id}|{Nome}|{TitulosMundiais}|{TitulosBrasileiros}|{DataCriacao}";
        }

        public string ToCSV()
        {
            return $"{Id}|{Nome}|{TitulosMundiais}|{TitulosBrasileiros}|{DataCriacao}";
        }

        public DateOnly GetDataRegister()
        {
            return DataCriacao;
        }
    }
}