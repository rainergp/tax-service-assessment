using System;
using TaxService.Factories;
using TaxService.Types;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var taxCalculatorFactory = new TaxCalculatorFactory();
            var taxService = new Services.TaxService(TaxCalculatorFactory.CreateTaxCalculatorService(CalculatorType.TaxJar));

        }
    }
}