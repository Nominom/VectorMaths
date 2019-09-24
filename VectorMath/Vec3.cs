using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace VectorMath
{
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	public struct Vec3 : IEquatable<Vec3>
	{
		public static readonly Vec3 zero = new Vec3(0, 0, 0);
		public static readonly Vec3 one = new Vec3(1, 1, 1);

		public static readonly Vec3 right = new Vec3(1, 0, 0);
		public static readonly Vec3 up = new Vec3(0, 1, 0);
		public static readonly Vec3 forward = new Vec3(0, 0, 1);

		public static readonly Vec3 left = new Vec3(-1, 0, 0);
		public static readonly Vec3 down = new Vec3(0, -1, 0);
		public static readonly Vec3 backward = new Vec3(0, 0, -1);

		[FieldOffset(0)] public float x;
		[FieldOffset(4)] public float y;
		[FieldOffset(8)] public float z;

		public Vec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator + (in Vec3 left, in Vec3 right)
		{
			return new Vec3(left.x + right.x, left.y + right.y, left.z + right.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator -(in Vec3 left, in Vec3 right)
		{
			return new Vec3(left.x - right.x, left.y - right.y, left.z - right.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator *(in Vec3 left, in Vec3 right)
		{
			return new Vec3(left.x * right.x, left.y * right.y, left.z * right.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator *(in Vec3 left, in float right)
		{
			return new Vec3(left.x * right, left.y * right, left.z * right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator /(in Vec3 left, in Vec3 right)
		{
			return new Vec3(left.x / right.x, left.y / right.y, left.z / right.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vec3 operator /(in Vec3 left, in float right)
		{
			return new Vec3(left.x / right, left.y / right, left.z / right);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float Length()
		{
			return MathF.Sqrt(x * x + y * y + z * z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float Dot(in Vec3 other)
		{
			return (x * other.x + y * other.y + z * other.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Vec3 Cross(in Vec3 other)
		{
			return new Vec3(
				this.y * other.z - this.z * other.y,
				this.z * other.x - this.x * other.z,
				this.x * other.y - this.y * other.x
				);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Vec3 Normalized()
		{
			float length = Length();
			if(length == 0)
			{
				return zero;
			}
			return this / Length();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float length = Length();
			if (length == 0)
			{
				return;
			}

			x = x / length;
			y = y / length;
			z = z / length;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Vec3 Reflect(Vec3 normal)
		{
			//r = d−2(d⋅n)n

			float dot = Dot(normal);
			return this - (normal * dot * 2);
		}



#region EqualityComparison

		public static bool operator ==(Vec3 left, Vec3 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Vec3 left, Vec3 right)
		{
			return !(left == right);
		}

		public override bool Equals(object obj)
		{
			return obj is Vec3 vector && Equals(vector);
		}

		public bool Equals([AllowNull] Vec3 other)
		{
			return x == other.x &&
				   y == other.y &&
				   z == other.z;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(x, y, z);
		}

		public bool ApproximatelyEquals(Vec3 other)
		{
			return	MathF.Abs(x - other.x) <= float.Epsilon &&
					MathF.Abs(y - other.y) <= float.Epsilon &&
					MathF.Abs(z - other.z) <= float.Epsilon;
		}

#endregion

		public override string ToString() {
			return $"{x}, {y}, {z}";
		}
	}
}
