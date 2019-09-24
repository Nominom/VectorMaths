using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VectorMath.Tests
{
	public class Vector3Tests
	{

		[Fact]
		public void ApproximatelyEquals()
		{
			Vec3 vec1 = new Vec3(1, 1, 1);
			Vec3 vec2 = new Vec3(10 / 10, 10 / 10, 10 / 10);
			Vec3 vec3 = new Vec3(1 + float.Epsilon, 1 + float.Epsilon, 1 + float.Epsilon);

			Assert.True(vec1.ApproximatelyEquals(vec2));
			Assert.True(vec2.ApproximatelyEquals(vec3));

			Assert.False(vec1.ApproximatelyEquals(Vec3.forward));
			Assert.False(vec2.ApproximatelyEquals(Vec3.forward));
		}


		[Fact]
		public void Add()
		{
			Vec3 vec1 = new Vec3(1, 2, 3);
			Vec3 vec2 = new Vec3(-4, 5, -6);

			Vec3 result = new Vec3(-3, 7, -3);

			Assert.True(result.ApproximatelyEquals(vec1 +  vec2));
		}

		[Fact]
		public void Subtract()
		{
			Vec3 vec1 = new Vec3(1, 2, 3);
			Vec3 vec2 = new Vec3(-4, 5, -6);

			Vec3 result = new Vec3(5, -3, 9);

			Assert.True(result.ApproximatelyEquals(vec1 - vec2));

		}

		[Fact]
		public void Multiply()
		{
			Vec3 vec1 = new Vec3(1, 2, 3);
			Vec3 vec2 = new Vec3(-4, 5, -6);

			Vec3 result = new Vec3(-4, 10, -18);

			Assert.True(result.ApproximatelyEquals(vec1 * vec2));
		}

		[Fact]
		public void Divide()
		{
			Vec3 vec1 = new Vec3(4, 6, 8);
			Vec3 vec2 = new Vec3(-2, 3, 4);

			Vec3 result = new Vec3(-2, 2, 2);

			Assert.True(result.ApproximatelyEquals(vec1 / vec2));
		}

		[Fact]
		public void MultiplyScalar()
		{
			Vec3 vec1 = new Vec3(1, 2, 3);
			float scalar = 2;

			Vec3 result = new Vec3(2, 4, 6);

			Assert.True(result.ApproximatelyEquals(vec1 * scalar));
		}

		[Fact]
		public void DivideScalar()
		{
			Vec3 vec1 = new Vec3(2, 4, 6);
			float scalar = 2;

			Vec3 result = new Vec3(1, 2, 3);

			Assert.True(result.ApproximatelyEquals(vec1 / scalar));
		}

		[Fact]
		public void Length()
		{
			Vec3 vec1 = new Vec3(3, 2, 1);

			System.Numerics.Vector3 comp1 = new System.Numerics.Vector3(3, 2, 1);
			float result = comp1.Length();

			Assert.InRange(vec1.Length(), result - float.Epsilon, result + float.Epsilon);
		}

		[Fact]
		public void Normalized()
		{
			Vec3 vec1 = new Vec3(1, 1, 1);

			float length = MathF.Sqrt(3);

			System.Numerics.Vector3 comp = new System.Numerics.Vector3(1, 1, 1);
			System.Numerics.Vector3 norm = System.Numerics.Vector3.Normalize(comp);
			Vec3 result = new Vec3(norm.X, norm.Y, norm.Z);


			Assert.True(vec1.Normalized().ApproximatelyEquals(result));
		}

		[Fact]
		public void Normalize()
		{
			Vec3 vec1 = new Vec3(1, 1, 1);

			float length = MathF.Sqrt(3);

			System.Numerics.Vector3 comp = new System.Numerics.Vector3(1, 1, 1);
			System.Numerics.Vector3 norm = System.Numerics.Vector3.Normalize(comp);
			Vec3 result = new Vec3(norm.X, norm.Y, norm.Z);

			vec1.Normalize();
			Assert.True(vec1.ApproximatelyEquals(result));
		}

		[Fact]
		public void Dot()
		{
			Vec3 vec1 = new Vec3(3, 2, 1);
			Vec3 vec2 = new Vec3(-3, 6, 2);

			System.Numerics.Vector3 comp1 = new System.Numerics.Vector3(3, 2, 1);
			System.Numerics.Vector3 comp2 = new System.Numerics.Vector3(-3, 6, 2);
			float result = System.Numerics.Vector3.Dot(comp1, comp2);

			Assert.InRange(vec1.Dot(vec2), result - float.Epsilon, result + float.Epsilon);
		}

		[Fact]
		public void Cross()
		{
			Vec3 vec1 = Vec3.up;
			Vec3 vec2 = Vec3.right;

			System.Numerics.Vector3 comp1 = System.Numerics.Vector3.UnitY;
			System.Numerics.Vector3 comp2 = System.Numerics.Vector3.UnitX;
			System.Numerics.Vector3 comp = System.Numerics.Vector3.Cross(comp1, comp2);
			Vec3 result = new Vec3(comp.X, comp.Y, comp.Z);

			Assert.True(vec1.Cross(vec2).ApproximatelyEquals(result));
		}

		[Fact]
		public void Reflect()
		{
			Vec3 vec1 = Vec3.up;
			Vec3 vec2 = new Vec3(2, -1, 3).Normalized();

			System.Numerics.Vector3 comp1 = System.Numerics.Vector3.UnitY;
			System.Numerics.Vector3 comp2 = System.Numerics.Vector3.Normalize(new System.Numerics.Vector3(2, -1, 3));
			System.Numerics.Vector3 comp = System.Numerics.Vector3.Reflect(comp1, comp2);
			Vec3 result = new Vec3(comp.X, comp.Y, comp.Z);

			Assert.True(vec1.Reflect(vec2).ApproximatelyEquals(result));
		}
	}
}
