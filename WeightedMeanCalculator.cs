using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeightedMeanTest
{
	public class WeightedMeanCalculator
	{
		public WeightedMeanCalculator(int precision)
		{
			if (precision < 0 || precision > MAX_PERCISION)
				throw new ArgumentOutOfRangeException("precision");
			this.precision = precision;
		}

		private int precision;
		public int Precision
		{
			get { return this.precision; }
		}

		public const int MAX_PERCISION = 9;

		public IEnumerable<double> Calculate(double total, IEnumerable<int> weights)
		{
			if (total <= 0)
				throw new ArgumentOutOfRangeException("total");
			if (weights == null)
				throw new ArgumentNullException("weights");
			var count = weights.Count();
			if (count == 0)
				throw new ArgumentNullException("weights");

			var weightsValue = weights.Select(weight => new Weight(weight)).ToList();

			int precision = this.precision;

			int expander = Pow10(precision);

			int numerator = (int)Math.Round(total, precision) * expander;
			int denominator = weights.Sum();

			int rem;
			int div = Math.DivRem(numerator, denominator, out rem);

			int leftrem = rem;
			for (int i = 0, j = 0; i < denominator; j++)
			{
				int weight = weightsValue[j].Value;
				double result = Math.Round(((double)((div * weight) + (i < rem ? Math.Min(leftrem, weight) : 0))) / expander, precision);
				Console.WriteLine("\r\nweighted index: {0} \r\nnormal index: \t{1} \r\nweight: \t{2} \r\nremainer: \t{3} \r\nleft remainer: \t{4} \r\nresult: \t{5:R}", i, j, weight, rem, Math.Max(0, leftrem), result);
				i += weight;
				leftrem -= weight;
				yield return result;
			}
		}

		public static WeightedMeanCalculator P2
		{
			get { return new WeightedMeanCalculator(2); }
		}

		private static int Pow10(int width)
		{
			if (width < 0 || width > 10)
				throw new ArgumentOutOfRangeException("width");

			int result = 1;
			for (int i = 0; i < width; i++)
			{
				result = result * 10;
			}

			return result;
		}

		private class Weight
		{
			public Weight(int weight)
			{
				if (weight <= 0)
					throw new ArgumentOutOfRangeException("weight");
				this.value = weight;
			}

			private int value;
			public int Value
			{
				get { return this.value; }
			}
		}
	}
}
