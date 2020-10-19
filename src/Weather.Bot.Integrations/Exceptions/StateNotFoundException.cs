using System;

namespace Weather.Bot.Integrations.Exceptions
{
    public class StateNotFoundException : Exception 
    {
        public StateNotFoundException(string uf) : base($"The state {uf} was not found!") {}
    }
}