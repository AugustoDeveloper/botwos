using System;

namespace Botwos.Infrastructure.Integrations.Exceptions.Weather
{
    public class StateNotFoundException : Exception 
    {
        public StateNotFoundException(string uf) : base($"The state {uf} was not found!") {}
    }
}