using System;
using TaxService.Interfaces;
using TaxService.Models.Enum;
using TaxService.Services.Calculators;

namespace TaxService.Factories
{
    public static class TaxCalculatorFactory
    {
        //TODO: Move this to a config class
        private const string ApiKey = "5da2f821eee4035db4771edab942a4cc";
        private const string ApiUrl = "https://api.taxjar.com/v2";
        public static ITaxCalculatorService Create(CalculatorType type)
        {
            return type switch
            {
                CalculatorType.TaxJar => new TaxJarCalculatorService(ApiKey, ApiUrl),
                CalculatorType.Test => new TestCalculatorService(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}