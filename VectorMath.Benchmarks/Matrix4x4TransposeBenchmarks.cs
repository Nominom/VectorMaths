using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using Core.ECS.Numerics;
using ECSCoreBenchmarks;
using VectorMath;
using Matrix4x4 = System.Numerics.Matrix4x4;

namespace VectorMath.Benchmarks
{
	[Config(typeof(NormalAsmConfig))]
	public class Matrix4x4TransposeBenchmarks
	{

		private System.Numerics.Matrix4x4 numericsMat1;

		private VectorMath.Mat4x4 mat1;

		[GlobalSetup]
		public void Setup()
		{
			numericsMat1 = Matrix4x4.CreateFromYawPitchRoll(10, 3, 3);

			mat1 = new VectorMath.Mat4x4(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
		}

		[Benchmark(Baseline = true)]
		public VectorMath.Mat4x4 TransposeNormal() {
			return VectorMath.Mat4x4.Transpose(mat1);
		}

		[Benchmark]
		public VectorMath.Mat4x4 TransposeRef()
		{
			return VectorMath.Mat4x4.TransposeRef(mat1);
		}

		[Benchmark]
		public Matrix4x4 NumericsTranspose()
		{
			return Matrix4x4.Transpose(numericsMat1);
		}

		[Benchmark]
		public VectorMath.Mat4x4 SseTranspose()
		{
			return Mat4x4Sse.Transpose(mat1);
		}

		[Benchmark]
		public VectorMath.Mat4x4 SseTransposeRef()
		{
			return Mat4x4Sse.TransposeRef(mat1);
		}

		[Benchmark]
		public VectorMath.Mat4x4 SseTransposeStore()
		{
			return Mat4x4Sse.TransposeStore(mat1);
		}

		[Benchmark]
		public VectorMath.Mat4x4 AvxTranspose()
		{
			return Mat4x4Avx.TransposeAvx2(mat1);
		}

		[Benchmark]
		public VectorMath.Mat4x4 AvxTransposeLoadArray()
		{
			return Mat4x4Avx.TransposeLoadArray(mat1);
		}

		[Benchmark]
		public VectorMath.Mat4x4 AvxTransposeReadOnlySpan()
		{
			return Mat4x4Avx.TransposeReadOnlySpan(mat1);
		}
	}
}
