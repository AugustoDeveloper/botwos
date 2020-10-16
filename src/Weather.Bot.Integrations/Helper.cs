using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Bot.Integrations
{
    public static class Helper
    {

        public static IReadOnlyDictionary<string, string> StatesFromBrazil = new Dictionary<string, string>
        {
            {"AC", "Acre"},
            {"AL", "Alagoas"},
            {"AP", "Amapa"},
            {"AM", "Amazonas"},
            {"BA", "Bahia"},
            {"CE", "Ceara"},
            {"DF", "Distrito Federal"},
            {"ES", "Espirito Santo"},
            {"GO", "Goias"},
            {"MA", "Maranhao"},
            {"MT", "Mato Grosso"},
            {"MS", "Mato Grosso do Sul"},
            {"MG", "Minas Gerais"},
            {"PA", "Para"},
            {"PB", "Paraiba"},
            {"PR", "Parana"},
            {"PE", "Pernambuco"},
            {"PI", "Piaui"},
            {"RJ", "Rio de Janeiro"},
            {"RN", "Rio Grande do Norte"},
            {"RS", "Rio Grande do Sul"},
            {"RO", "Rondonia"},
            {"RR", "Roraima"},
            {"SC", "Santa Catarina"},
            {"SP", "São Paulo"},
            {"SE", "Sergipe"},
            {"TO", "Tocantins"},
        };

    }
}
