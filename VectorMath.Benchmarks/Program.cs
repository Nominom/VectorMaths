using System;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Running;

namespace VectorMath.Benchmarks
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("SSE supported: " + Sse.IsSupported);
			Console.WriteLine("SSE2 supported: " + Sse2.IsSupported);
			Console.WriteLine("AVX supported: " + Avx.IsSupported);
			Console.WriteLine("AVX2 supported: " + Avx2.IsSupported);

			BenchmarkRunner.Run<Matrix4x4MultiplyBenchmarks>();

		}
	}
}
