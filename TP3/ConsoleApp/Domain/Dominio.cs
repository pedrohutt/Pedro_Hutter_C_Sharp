﻿using System;

namespace Domain11

{
    public class Teams
    {
        public string? Nome { get; private set; }
        public string? Estado { get; private set; }
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
