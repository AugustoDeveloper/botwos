namespace Botwos.Weather.Infrastructure.Persistence.Entities
{
    abstract public class BaseResponsePart
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TextFormat {get;set; }
        public string Language { get; set; }
    }
}