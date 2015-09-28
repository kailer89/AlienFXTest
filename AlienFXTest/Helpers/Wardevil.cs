using AlienFXTest.Extensions;
using LightFX;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace AlienFXTest.Helpers
{
	public class Wardevil : IDisposable
	{
		private LightFXController _lightFX;

		private LFX_Result _result;

		public int OverrideWaitTime = 0;

		public Color CurrentSolidColor;

		public Color CurrentFirstColor;

		public Color CurrentSecondColor;

		private bool disposing = false;

		public Wardevil()
		{
			try
			{
				this._lightFX = new LightFXController();
				LFX_Result lFXResult = this._lightFX.LFX_Initialize();
				if (lFXResult != LFX_Result.LFX_Success)
				{
					if (lFXResult == LFX_Result.LFX_Error_NoDevs)
					{
						throw new Exception("There is not AlienFX device available.");
					}
					throw new Exception("There was an error initializing the AlienFX device.");
				}
				this._lightFX.LFX_Reset();
			}
			catch (Exception exception)
			{
				Trace.WriteLine(exception.ToString());
			}
		}

		public void ChangeToGradientColorMorph(Color colorFrom, Color colorTo, int TimingInBetwenChanges = 0)
		{
			this.Reset();
			this.CurrentFirstColor = colorFrom;
			this.CurrentSecondColor = colorTo;
			this._lightFX.LFX_ActionColorEx(LFX_Position.LFX_All, LFX_ActionEnum.Morph, colorFrom.ColorToLFXColor(), colorTo.ColorToLFXColor());
			if (TimingInBetwenChanges > 0)
			{
				this._lightFX.LFX_SetTiming(1000);
			}
			this._lightFX.LFX_Update();
			this.Wait();
		}

		public void ChangeToMorphColorBlackBackground(Color ColorToChangeTo, int TimingInBetwenPulses = 0)
		{
			this.Reset();
			this.CurrentSolidColor = ColorToChangeTo;
			this._lightFX.LFX_ActionColor(LFX_Position.LFX_All, LFX_ActionEnum.Morph, ColorToChangeTo.ColorToLFXColor());
			if (TimingInBetwenPulses > 0)
			{
				this._lightFX.LFX_SetTiming(1000);
			}
			this._lightFX.LFX_Update();
			this.Wait();
		}

		public void ChangeToPulsatingColorBlackBackground(Color ColorToChangeTo, int TimingInBetwenPulses = 0)
		{
			this.Reset();
			this.CurrentSolidColor = ColorToChangeTo;
			this._lightFX.LFX_ActionColor(LFX_Position.LFX_All, LFX_ActionEnum.Pulse, ColorToChangeTo.ColorToLFXColor());
			if (TimingInBetwenPulses > 0)
			{
				this._lightFX.LFX_SetTiming(1000);
			}
			this._lightFX.LFX_Update();
			this.Wait();
		}

		public void ChangeToSolidColor(Color ColorToChangeTo)
		{
			this.Reset();
			this.CurrentSolidColor = ColorToChangeTo;
			this._lightFX.LFX_Light(LFX_Position.LFX_All, ColorToChangeTo.ColorToLFXColor());
			this.Wait();
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._lightFX != null)
				{
					this._lightFX.LFX_Release();
				}
			}
		}

		public void Reset()
		{
			this._lightFX.LFX_Reset();
		}

		private void Wait()
		{
			this._lightFX.LFX_Update();
			Thread.Sleep((this.OverrideWaitTime > 0 ? this.OverrideWaitTime : 1000));
		}
	}
}