using System.Collections.Generic;
using Botwos.Infrastructure.Integrations.Exceptions.Weather;

namespace Botwos.Infrastructure.Integrations.Extensions
{
    static public class StringExtension
    {
        public static IReadOnlyDictionary<string, string> StatesCapitalsFromBrazil = new Dictionary<string, string>
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
        static public string ToCapital(this string uf)
        {
            if (!string.IsNullOrWhiteSpace(uf) && StatesCapitalsFromBrazil.ContainsKey(uf.Trim().ToUpper()))
            {   
                return StatesCapitalsFromBrazil[uf.Trim().ToUpper()];
            }

            throw new StateNotFoundException(uf);
        }
    }
}