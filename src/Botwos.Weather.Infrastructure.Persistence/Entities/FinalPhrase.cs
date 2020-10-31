namespace Botwos.Weather.Infrastructure.Persistence.Entities
{
    public class FinalPhrase : BaseResponsePart
    {
        public double BeginPrecipitationMMRange { get; set; }
        public double EndPrecipitationMMRange { get; set; }
        public int BeginCloudPercentageRange { get; set; }
        public int EndCloudPercentageRange { get; set; }
    }
}

// Chuva fraca: quando a intensidade é inferior a 2,5 milímetros por hora (mm/h);
// Chuva moderada: quando a intensidade é igual ou superior a 2,5 mm/h mas inferior a 10 mm/h;
// Chuva forte: quando a intensidade é igual ou superior a 10 mm/h mas inferior a 50 mm/h;
// Chuva violenta: quando a intensidade é superior a 50 mm/h (geralmente sob a forma de aguaceiros).

// Parcialmente nublado (ou quebrado) é classificada como a cobertura de nuvens de 70 a 80 por cento ou 5 a 7 octas.
// Este valor é inferior a 90 a 100 por cento (8 octas) usados ​​para definir de céu nublado