using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;
using VectorMath;
using Xunit;

namespace VectorMath.Tests
{
	public class Vector3AvxTests
	{

		private const int arrLength = 501;

		Vec3[] vec1 = new Vec3[arrLength];
		Vec3[] vec2 = new Vec3[arrLength];
		Vec3[] result = new Vec3[arrLength];
		Vec3[] actual = new Vec3[arrLength];
		float[] inputF = new float[arrLength];


		private void initializeArrays()
		{
			vec1 = new Vec3[arrLength];
			vec2 = new Vec3[arrLength];
			result = new Vec3[arrLength];
			actual = new Vec3[arrLength];
			inputF = new float[arrLength];


			for (int i = 0; i < arrLength; i++)
			{
				vec1[i] = new Vec3(i, i + 1, i + 2);
				vec2[i] = new Vec3(i + 3, i + 4, i + 5);
				inputF[i] = i;
			}
		}


		[Fact]
		public void Add()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();

			Vector3Fallbacks.Add(vec1, vec2, result);
			Vector3Avx.Add(vec1, vec2, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void Subtract()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();

			Vector3Fallbacks.Subtract(vec1, vec2, result);
			Vector3Avx.Subtract(vec1, vec2, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void Multiply()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();

			Vector3Fallbacks.Multiply(vec1, vec2, result);
			Vector3Avx.Multiply(vec1, vec2, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void MultiplyScalar()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();
			const float scalar = 5f;

			Vector3Fallbacks.MultiplyAllScalar(vec1, scalar, result);
			Vector3Avx.MultiplyScalar(vec1, scalar, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void MultiplyScalars()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();

			Vector3Fallbacks.MultiplyScalar(vec1, inputF, result);
			Vector3Avx.MultiplyScalars(vec1, inputF, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void Normalize()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();

			Vector3Fallbacks.Normalize(vec1, result);
			Vector3Avx.Normalize(vec1, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void NormalizeAvx2()
		{
			Assert.True(Avx2.IsSupported);
			initializeArrays();

			Vector3Fallbacks.Normalize(vec1, result);
			Vector3Avx.NormalizeAvx2(vec1, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}


		[Fact]
		public void NormalizeZero()
		{
			Assert.True(Avx.IsSupported);

			vec1 = new Vec3[8] { new Vec3(0, 0, 0), new Vec3(1, 1, 1), new Vec3(2, 3, 4), new Vec3(2, 3, 4), new Vec3(0, 0, 0), new Vec3(0, 0, 0), new Vec3(6, 5, 2), new Vec3(0, 0, 0) };
			vec2 = new Vec3[8] { new Vec3(1, 1, 1), new Vec3(0, 0, 0), new Vec3(0, 0, 0), Vec3.backward, Vec3.forward, Vec3.left, Vec3.down, Vec3.up };
			result = new Vec3[8];
			actual = new Vec3[8];


			Vector3Fallbacks.Normalize(vec1, result);
			Vector3Avx.NormalizeAvx2(vec1, actual);

			for (int i = 0; i < 8; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}

			Vector3Fallbacks.Normalize(vec2, result);
			Vector3Avx.Normalize(vec2, actual);

			for (int i = 0; i < 8; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void Cross()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();

			Vector3Fallbacks.Cross(vec1, vec2, result);
			Vector3Avx.Cross(vec1, vec2, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}

		[Fact]
		public void Reflect()
		{
			Assert.True(Avx.IsSupported);
			initializeArrays();

			Vector3Fallbacks.Reflect(vec1, vec2, result);
			Vector3Avx.Reflect(vec1, vec2, actual);

			for (int i = 0; i < arrLength; i++)
			{
				Assert.True(result[i].ApproximatelyEquals(actual[i]),
					$"index: {i}, result: {result[i]}, actual: {actual[i]}");
			}
		}
	}
}
