using LightFX;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AlienFXTest.Extensions
{
	public static class AlienFX
	{
		public static LFX_ColorStruct ColorToLFXColor(this Color color)
		{
			LFX_ColorStruct lFXColorStruct = new LFX_ColorStruct(255, color.R, color.G, color.B);
			return lFXColorStruct;
		}
	}
}