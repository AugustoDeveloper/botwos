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
        static public string ToCapitalIfExists(this string stateOrCity)
        {
            if (!string.IsNullOrWhiteSpace(stateOrCity) && StatesCapitalsFromBrazil.ContainsKey(stateOrCity.Trim().ToUpper()))
            {   
                return StatesCapitalsFromBrazil[stateOrCity.Trim().ToUpper()];
            }

            return stateOrCity;
        }
    }
}