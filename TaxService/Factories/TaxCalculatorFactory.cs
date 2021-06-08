using System;
using TaxService.Interfaces;
using TaxService.Services;
using TaxService.Services.Calculators;
using TaxService.Types;

namespace TaxService.Factories
{
    public class TaxCalculatorFactory
    {
        public static ITaxCalculatorService CreateTaxCalculatorService(CalculatorType type)
        {
            return type switch
            {
                CalculatorType.TaxJar => new TaxJarCalculatorService(),
                CalculatorType.Test => new TestCalculatorService(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}