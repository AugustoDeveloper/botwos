using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Bot.Integrations
{
    public static class Helper
    {
        public static IEnumerable<(string UF, string Name)> UfToState()
        {
            yield return ("AC", "Acre");
            yield return ("AL", "Alagoas");
            yield return ("AP", "Amapa");
            yield return ("AM", "Amazonas");
            yield return ("BA", "Bahia");
            yield return ("CE", "Ceara");
            yield return ("DF", "Distrito Federal");
            yield return ("ES", "Espirito Santo");
            yield return ("GO", "Goias");
            yield return ("MA", "Maranhao");
            yield return ("MT", "Mato Grosso");
            yield return ("MS", "Mato Grosso do Sul");
            yield return ("MG", "Minas Gerais");
            yield return ("PA", "Para");
            yield return ("PB", "Paraiba");
            yield return ("PR", "Parana");
            yield return ("PE", "Pernambuco");
            yield return ("PI", "Piaui");
            yield return ("RJ", "Rio de Janeiro");
            yield return ("RN", "Rio Grande do Norte");
            yield return ("RS", "Rio Grande do Sul");
            yield return ("RO", "Rondonia");
            yield return ("RR", "Roraima");
            yield return ("SC", "Santa Catarina");
            yield return ("SP", "São Paulo");
            yield return ("SE", "Sergipe");
            yield return ("TO", "Tocantins");
        }

    }
}
