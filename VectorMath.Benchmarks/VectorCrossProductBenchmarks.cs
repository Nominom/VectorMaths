﻿using BenchmarkDotNet.Attributes;
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
	public class VectorCrossProductBenchmarks
	{
		private VectorMath.Vec3[] vec1;
		private VectorMath.Vec3[] vec2;

		private System.Numerics.Vector3[] systemVec1;
		private System.Numerics.Vector3[] systemVec2;

		[Params(10, 100, 1000, 100_000)]
		public int numVectors;

		[GlobalSetup]
		public void Setup()
		{
			vec1 = new VectorMath.Vec3[numVectors];
			vec2 = new VectorMath.Vec3[numVectors];

			systemVec1 = new System.Numerics.Vector3[numVectors];
			systemVec2 = new System.Numerics.Vector3[numVectors];

			for (int i = 0; i < numVectors; i++)
			{
				vec1[i] = new VectorMath.Vec3(i, i, i);
				vec2[i] = new VectorMath.Vec3(i, i, i);

				systemVec1[i] = new System.Numerics.Vector3(i, i, i);
				systemVec2[i] = new System.Numerics.Vector3(i, i, i);
			}
		}


		[Benchmark(Baseline = true)]
		public float BaselineCrossProduct()
		{
			for (int i = 0; i < numVectors; i++)
			{
				vec1[i] = vec1[i].Cross(vec2[i]);
			}


			return vec1[0].x;
		}

		[Benchmark]
		public float SystemNumericsCrossProduct()
		{
			for (int i = 0; i < numVectors; i++)
			{
				systemVec1[i] = System.Numerics.Vector3.Cross(systemVec1[i], systemVec2[i]);
			}

			return systemVec1[0].X;
		}

		[Benchmark]
		public float SseCrossProduct()
		{
			VectorMath.Vector3Sse.Cross(vec1, vec2, vec1);

			return vec1[0].x;
		}

		[Benchmark]
		public float AvxCrossProduct()
		{
			VectorMath.Vector3Avx.Cross(vec1, vec2, vec1);

			return vec1[0].x;
		}

	}
}
