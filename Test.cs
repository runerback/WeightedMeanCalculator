using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeightedMeanTest
{
	public static class Test
	{
		public static void Do1()
		{
			double total = 50;
			List<int> weights = new List<int>(8) { 2, 2, 2, 2, 2, 2, 2, 1 };

			var results = WeightedMeanCalculator.P2.Calculate(total, weights);

			double total2 = 0;
			foreach (double result in results)
			{
				total2 += result;
			}
			Console.WriteLine("Total: {0}", total2);
		}

		public static void Do2()
		{
			double total = 50;

			var results = WeightedMeanCalculator.P2.Calculate(total, Enumerable.Repeat(1, 15));

			double total2 = 0;
			foreach (double result in results)
			{
				total2 += result;
			}
			Console.WriteLine("Total: {0}", total2);
		}
	}
}
