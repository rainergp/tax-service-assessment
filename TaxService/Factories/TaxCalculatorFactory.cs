using System;
using TaxService.Config;
using TaxService.Interfaces;
using TaxService.Models.Enum;
using TaxService.Services.Calculators;

namespace TaxService.Factories
{
    public static class TaxCalculatorFactory
    {
        public static ITaxCalculatorService Create(CalculatorType type)
        {
            return type switch
            {
                CalculatorType.TaxJar => new TaxJarCalculatorService(TaxJarApiConfig.ApiKey, TaxJarApiConfig.ApiUrl),
                CalculatorType.Test => new TestCalculatorService(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}