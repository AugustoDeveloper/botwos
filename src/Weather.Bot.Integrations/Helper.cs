using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Bot.Integrations
{
    public static class Helper
    {

        public static IReadOnlyDictionary<string, string> CapitalsFromBrazil = new Dictionary<string, string>
        {
            {"AC", "Rio Branco"},
            {"AL", "Maceio"},
            {"AP", "Macapa"},
            {"AM", "Manaus"},
            {"BA", "Salvador"},
            {"CE", "Fortaleza"},
            {"DF", "Brasilia"},
            {"ES", "Vitoria"},
            {"GO", "Goiania"},
            {"MA", "Sao Luis"},
            {"MT", "Cuiaba"},
            {"MS", "Campo Grande"},
            {"MG", "Belo Horizonte"},
            {"PA", "Belem"},
            {"PB", "Joao Pessoa"},
            {"PR", "Curitiba"},
            {"PE", "Recife"},
            {"PI", "Teresina"},
            {"RJ", "Rio de Janeiro"},
            {"RN", "Natal"},
            {"RS", "Porto Alegre"},
            {"RO", "Porto Velho"},
            {"RR", "Boa Vista"},
            {"SC", "Florianopolis "},
            {"SP", "São Paulo"},
            {"SE", "Aracaju"},
            {"TO", "Palmas"},
        };

    }
}
