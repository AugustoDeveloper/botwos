using Botwos.Weather.Infrastructure.Persistence;
using Botwos.Weather.Infrastructure.Persistence.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Botwos.Weather.Bot.Core
{
    static public class ResponseContextExtension
    {
        async static public Task<string> GenerateSuccessResponseFormatMessageAsync(this DbResponsesContext context,
            string language,
            double feelsLikeCelsius,
            double precipitation,
            int cloudPercentage,
            int greetingShortCode,
            int initialPhraseShortCode,
            int finalPhraseShortCode)
        {
            Console.WriteLine($"{nameof(language)} - {language}");
            Console.WriteLine($"{nameof(feelsLikeCelsius)} - {feelsLikeCelsius}");
            Console.WriteLine($"{nameof(precipitation)} - {precipitation}");
            Console.WriteLine($"{nameof(cloudPercentage)} - {cloudPercentage}");
            Console.WriteLine($"{nameof(greetingShortCode)} - {greetingShortCode}");
            Console.WriteLine($"{nameof(initialPhraseShortCode)} - {initialPhraseShortCode}");
            Console.WriteLine($"{nameof(finalPhraseShortCode)} - {finalPhraseShortCode}");

            var greeting = await context.Greetings.FirstOrDefaultAsync(x =>
                x.Kind == GreetingKind.Success &&
                x.ShortCode == $"GRTNG_{greetingShortCode}" &&
                x.Language == language);

            var initialPhrase = await context.InitialPhrases.FirstOrDefaultAsync(x =>
                x.ShortCode == $"PHRS_{initialPhraseShortCode}" &&
                x.Language == language &&
                x.BeginFeelsLikeCelsiusRange <= feelsLikeCelsius &&
                x.EndFeelsLikeCelsiusRange >= feelsLikeCelsius);

            //TODO: We need build the last part of the phrase and add to format below
            //var finalPhrase = await context.FinalPhrases.FirstAsync(x =>
            //    x.ShortCode == $"PRHS_{finalPhraseShortCode}" &&
            //    x.Language == language &&
            //    x.BeginCloudPercentageRange >= cloudPercentage &&
            //    x.EndCloudPercentageRange <= cloudPercentage &&
            //    x.BeginPrecipitationMMRange >= precipitation &&
            //    x.EndPrecipitationMMRange <= precipitation);

            //TODO: We need add all english phrases e and rest of portuguese phrases
            return $"{greeting?.TextFormat ?? "Hello {0}, "}{initialPhrase?.TextFormat ?? "it is {1}°C in {2}."}";
        }

        async static public Task<string> GenerateFailureResponseFormatMessage(this DbResponsesContext context,
            string language,
            int greetingShortCode)
        {
            var greeting = await context.Greetings.FirstAsync(x =>
                x.Kind == GreetingKind.Failure &&
                x.ShortCode == $"GRTNG_{greetingShortCode}" &&
                x.Language == language);

            return $"{greeting.TextFormat}";
        }

    }
}