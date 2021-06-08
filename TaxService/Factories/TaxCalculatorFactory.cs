using System;
using TaxService.Interfaces;
using TaxService.Services;
using TaxService.Services.Calculators;
using TaxService.Types;

namespace TaxService.Factories
{
    public static class TaxCalculatorFactory
    {
        public static ITaxCalculatorService Create(CalculatorType type)
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