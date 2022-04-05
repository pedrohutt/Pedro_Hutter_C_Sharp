using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Team
    {   
        public Team(int id,string name, short titulosBR, byte titulosM, DateOnly criacao)
        {
            Id = id;
            Name = name;
            TitulosBR = titulosBR;
            TitulosMundiais = titulosM;
            Criacao = criacao;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short TitulosBR { get; set; }
        public byte TitulosMundiais { get; set; }
        public DateOnly Criacao { get; set; }

        public int tempoAtivo()
        {
            int now = DateTime.Now.Year;
            int dataCriacao = now - Criacao.Year;
            return dataCriacao;
        }

        public DateOnly GetDataRegister()
        {
            return Criacao;
        }

        public string GetTeamInfo() => string.Format("Id: {0} Nome: {1} - Tempo Ativo: {2}", Id, Name, tempoAtivo());

        public override string ToString()
        {
            return $"{Id}|{Name}|{TitulosBR}|{TitulosMundiais}|{Criacao}";
        }
    }
}
