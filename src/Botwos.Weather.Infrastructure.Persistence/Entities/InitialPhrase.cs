namespace Botwos.Weather.Infrastructure.Persistence.Entities
{
    public class InitialPhrase : BaseResponsePart
    {
        public double BeginFeelsLikeCelsiusRange { get; set; }
        public double EndFeelsLikeCelsiusRange { get; set; }
    }
}