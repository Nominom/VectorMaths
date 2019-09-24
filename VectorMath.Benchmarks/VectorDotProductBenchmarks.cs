using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSCoreBenchmarks
{
	[Config(typeof(NormalAsmConfig))]
	public class VectorDotProductBenchmarks
	{
		private VectorMath.Vec3[] vec1;
		private VectorMath.Vec3[] vec2;

		private System.Numerics.Vector3[] systemVec1;
		private System.Numerics.Vector3[] systemVec2;

		private float[] result;

		[Params(10, 1000, 100_000)]
		public int numVectors;

		[GlobalSetup]
		public void Setup()
		{
			vec1 = new VectorMath.Vec3[numVectors];
			vec2 = new VectorMath.Vec3[numVectors];

			systemVec1 = new System.Numerics.Vector3[numVectors];
			systemVec2 = new System.Numerics.Vector3[numVectors];

			result = new float[numVectors];

			for (int i = 0; i < numVectors; i++)
			{
				vec1[i] = new VectorMath.Vec3(i, i, i);
				vec2[i] = new VectorMath.Vec3(i, i, i);

				systemVec1[i] = new System.Numerics.Vector3(i, i, i);
				systemVec2[i] = new System.Numerics.Vector3(i, i, i);
			}
		}

		[Benchmark(Baseline = true)]
		public float BaselineDotProduct()
		{
			for (int i = 0; i < numVectors; i++) {
				result[i] = vec1[i].Dot(vec2[i]);
			}

			return result[0];
		}

		[Benchmark]
		public float SystemNumericsDotProduct()
		{
			for (int i = 0; i < numVectors; i++)
			{
				result[i] = System.Numerics.Vector3.Dot(systemVec1[i], systemVec2[i]);
			}

			return result[0];
		}

		[Benchmark]
		public float SseDotProduct()
		{
			VectorMath.Vector3Sse.Dot(vec1, vec2, result);

			return result[0];
		}

		[Benchmark]
		public float AvxDotProduct()
		{
			VectorMath.Vector3Avx.Dot(vec1, vec2, result);

			return result[0];
		}

	}
}
