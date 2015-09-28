using AlienFXTest.Extensions;
using LightFX;
using System;
using System.Drawing;
using System.Text;
using System.Threading;

namespace AlienFXTest.Examples
{
	public static class ChangeToGreen
	{
		public static void changeToGreen()
		{
			uint num;
			LightFXController lightFXController = new LightFXController();
			LFX_Result lFXResult = lightFXController.LFX_Initialize();
			if (lFXResult == LFX_Result.LFX_Success)
			{
				lightFXController.LFX_Reset();
				Color red = Color.Red;
				LFX_ColorStruct lFXColor = Color.Aqua.ColorToLFXColor();
				LFX_ColorStruct lFXColorStruct = new LFX_ColorStruct(255, red.R, red.G, red.B);
				lightFXController.LFX_Light(LFX_Position.LFX_All, lFXColor);
				StringBuilder stringBuilder = new StringBuilder();
				lightFXController.LFX_GetVersion(out stringBuilder, 1000);
				lightFXController.LFX_Update();
				lightFXController.LFX_GetNumLights(0, out num);
				lightFXController.LFX_ActionColorEx(LFX_Position.LFX_All, LFX_ActionEnum.Pulse, Color.Red.ColorToLFXColor(), Color.White.ColorToLFXColor());
				lightFXController.LFX_Update();
				Thread.Sleep(1000);
				lightFXController.LFX_ActionColor(LFX_Position.LFX_All, LFX_ActionEnum.Pulse, Color.Violet.ColorToLFXColor());
				lightFXController.LFX_Update();
				Thread.Sleep(1000);
				lightFXController.LFX_Release();
			}
			else if (lFXResult == LFX_Result.LFX_Error_NoDevs)
			{
				Console.WriteLine("There is not AlienFX device available.");
			}
			else
			{
				Console.WriteLine("There was an error initializing the AlienFX device.");
			}
		}
	}
}