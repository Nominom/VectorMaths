using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace VectorMath
{
	public static class Vector3Fallbacks
	{

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Add(ReadOnlySpan<Vec3> left, ReadOnlySpan<Vec3> right, Span<Vec3> result)
		{
			
			if (left.Length != right.Length || left.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = left.Length;

			for(int i = 0; i < length; i++)
			{
				result[i] = left[i] + right[i];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Subtract(ReadOnlySpan<Vec3> left, ReadOnlySpan<Vec3> right, Span<Vec3> result)
		{

			if (left.Length != right.Length || left.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = left.Length;

			for (int i = 0; i < length; i++)
			{
				result[i] = left[i] - right[i];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Multiply(ReadOnlySpan<Vec3> left, ReadOnlySpan<Vec3> right, Span<Vec3> result)
		{

			if (left.Length != right.Length || left.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = left.Length;

			for (int i = 0; i < length; i++)
			{
				result[i] = left[i] * right[i];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void MultiplyAllScalar(ReadOnlySpan<Vec3> left, float right, Span<Vec3> result)
		{

			if (left.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = left.Length;

			for (int i = 0; i < length; i++)
			{
				result[i] = left[i] * right;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void MultiplyScalar(ReadOnlySpan<Vec3> left, ReadOnlySpan<float> right, Span<Vec3> result)
		{

			if (left.Length != right.Length || left.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = left.Length;

			for (int i = 0; i < length; i++)
			{
				result[i] = left[i] * right[i];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Normalize(ReadOnlySpan<Vec3> src, Span<Vec3> dst)
		{

			if (src.Length != dst.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = src.Length;

			for (int i = 0; i < length; i++) {
				dst[i] = src[i].Normalized();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Dot(ReadOnlySpan<Vec3> left, ReadOnlySpan<Vec3> right, Span<float> result)
		{

			if (left.Length != right.Length || left.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = left.Length;

			for (int i = 0; i < length; i++)
			{
				result[i] = left[i].Dot(right[i]);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Cross(ReadOnlySpan<Vec3> left, ReadOnlySpan<Vec3> right, Span<Vec3> result)
		{

			if (left.Length != right.Length || left.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = left.Length;

			for (int i = 0; i < length; i++)
			{
				result[i] = left[i].Cross(right[i]);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Reflect(ReadOnlySpan<Vec3> dirs, ReadOnlySpan<Vec3> normals, Span<Vec3> result)
		{

			if (dirs.Length != normals.Length || dirs.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = dirs.Length;

			for (int i = 0; i < length; i++)
			{
				result[i] = dirs[i].Reflect(normals[i]);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Length(ReadOnlySpan<Vec3> src, Span<float> result)
		{

			if (src.Length != result.Length)
			{
				throw new ArgumentException("All spans should be the same length");
			}

			int length = src.Length;

			for (int i = 0; i < length; i++) {
				result[i] = src[i].Length();
			}
		}
	}
}
