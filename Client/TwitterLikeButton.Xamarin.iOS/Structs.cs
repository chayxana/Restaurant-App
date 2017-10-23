using System;
using System.Runtime.InteropServices;

namespace TwitterLikeButton.Xamarin.iOS
{
	static class CFunctions
	{
		// extern CGFloat TTTimingFunctionElasticOut (CGFloat, CGFloat, CGFloat, CGFloat, CGFloat, CGFloat);
		[DllImport("__Internal")]
		static extern nfloat TTTimingFunctionElasticOut(nfloat a1, nfloat a2, nfloat a3, nfloat a4, nfloat a5, nfloat a6);

		// extern CGFloat TTTTweenTimingFunctionElasticIn (CGFloat, CGFloat, CGFloat, CGFloat, CGFloat, CGFloat);
		[DllImport("__Internal")]
		static extern nfloat TTTTweenTimingFunctionElasticIn(nfloat a1, nfloat a2, nfloat a3, nfloat a4, nfloat a5, nfloat a6);
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct DotRadius
	{
		public nfloat first;

		public nfloat second;
	}
}

