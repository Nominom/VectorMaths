using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace VectorMath
{
	public static unsafe class Mat4x4Avx {
		private static int* CreatePermuter() {
			var ptr = Marshal.AllocHGlobal(sizeof(int) * 8);
			int* iP = (int*)ptr.ToPointer();
			iP[0] = 0;
			iP[1] = 2;
			iP[2] = 4;
			iP[3] = 6;
			iP[4] = 1;
			iP[5] = 3;
			iP[6] = 5;
			iP[7] = 7;
			return iP;
		}
		private static int* tranpose_permuter = CreatePermuter();
		private static ReadOnlySpan<int> pmuter => new[] {0, 2, 4, 6, 1, 3, 5, 7};


		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static unsafe Mat4x4 TransposeAvx2(Mat4x4 source) {
			Mat4x4 result = new Mat4x4();

			Mat4x4* srcP = &source;
			Mat4x4* resP = &result;

			float* src = (float*)srcP;
			float* res = (float*)resP;

			var avx1 = Avx.LoadVector256(src);
			var avx2 = Avx.LoadVector256(&src[8]);

			Vector256<int> permuter = Vector256.Create(0, 2, 4, 6, 1, 3, 5, 7);

			var tmp1 = Avx2.PermuteVar8x32(avx1, permuter);
			var tmp2 = Avx2.PermuteVar8x32(avx2, permuter);

			var res1 = Avx.Shuffle(tmp1, tmp2, 0b10_00_10_00);
			var res2 = Avx.Shuffle(tmp1, tmp2, 0b11_01_11_01);

			Avx.Store(&res[0], res1);
			Avx.Store(&res[8], res2);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static unsafe Mat4x4 TransposeLoadArray(Mat4x4 source)
		{
			Mat4x4 result = new Mat4x4();

			Mat4x4* srcP = &source;
			Mat4x4* resP = &result;

			float* src = (float*)srcP;
			float* res = (float*)resP;

			var avx1 = Avx.LoadVector256(src);
			var avx2 = Avx.LoadVector256(&src[8]);

			Vector256<int> permuter = Avx.LoadVector256(tranpose_permuter);

			var tmp1 = Avx2.PermuteVar8x32(avx1, permuter);
			var tmp2 = Avx2.PermuteVar8x32(avx2, permuter);

			var res1 = Avx.Shuffle(tmp1, tmp2, 0b10_00_10_00);
			var res2 = Avx.Shuffle(tmp1, tmp2, 0b11_01_11_01);

			Avx.Store(&res[0], res1);
			Avx.Store(&res[8], res2);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static unsafe Mat4x4 TransposeReadOnlySpan(Mat4x4 source)
		{
			Mat4x4 result = new Mat4x4();

			Mat4x4* srcP = &source;
			Mat4x4* resP = &result;

			float* src = (float*)srcP;
			float* res = (float*)resP;

			var avx1 = Avx.LoadVector256(src);
			var avx2 = Avx.LoadVector256(&src[8]);

			Vector256<int> permuter;
			fixed (int* p = &pmuter.GetPinnableReference()) {
				permuter = Avx.LoadVector256(p);
			}

			var tmp1 = Avx2.PermuteVar8x32(avx1, permuter);
			var tmp2 = Avx2.PermuteVar8x32(avx2, permuter);

			var res1 = Avx.Shuffle(tmp1, tmp2, 0b10_00_10_00);
			var res2 = Avx.Shuffle(tmp1, tmp2, 0b11_01_11_01);

			Avx.Store(&res[0], res1);
			Avx.Store(&res[8], res2);

			return result;
		}
	}
}
