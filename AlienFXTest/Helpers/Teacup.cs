using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlienFXTest.Helpers
{
	public class Teacup : IDisposable
	{
		private Computer _pc;

		private CancellationTokenSource _tokenSource;

		private CancellationToken _token;

		public int OverrideCounterRefreshInterval = 0;

		private Dictionary<string, ISensor> _Sensors = new Dictionary<string, ISensor>();

		private bool disposing = false;

		public Teacup(bool EnableGPU = false, bool EnableBatery = false)
		{
			Teacup teacup = this;
			Computer computer = new Computer()
			{
				CPUEnabled = true,
				GPUEnabled = true,
				RAMEnabled = true,
				HDDEnabled = true,
				MainboardEnabled = true,
				FanControllerEnabled = true
			};
			this._pc = computer;
			this._pc.Open();
			this._tokenSource = new CancellationTokenSource();
			this._token = this._tokenSource.Token;
			Task.Factory.StartNew(() => {
				int i;
				bool flag;
				bool flag1;
				bool flag2;
				bool flag3;
				Wardevil wardevil = new Wardevil();
				try
				{
					while (true)
					{
						if (teacup._token.IsCancellationRequested)
						{
							break;
						}
						Thread.Sleep((teacup.OverrideCounterRefreshInterval > 0 ? teacup.OverrideCounterRefreshInterval : 5000));
						if (EnableGPU)
						{
							var breakAll = false;

                            foreach (var hw in teacup._pc.Hardware)
                            {
                                hw.Update();
                                foreach (var shw in hw.SubHardware)
                                {
                                    shw.Update();
                                }
                                foreach (var sensor in hw.Sensors)
                                {
                                    float? value = sensor.Value;
                                    if (value.HasValue)
                                    {
                                        if ((sensor.SensorType != SensorType.Temperature ? false : hw.HardwareType == HardwareType.GpuNvidia))
                                        {
                                            if (value > 0 && value <= 35)
                                            {
                                                if (wardevil.CurrentSolidColor != Color.Red)
                                                {
                                                    wardevil.ChangeToSolidColor(Color.Red);
                                                    breakAll = true;
                                                    break;
                                                }
                                            }
                                            else if (value > 35 && value <= 50)
                                            {
                                                if (wardevil.CurrentSolidColor != Color.Orange)
                                                {
                                                    wardevil.ChangeToSolidColor(Color.Orange);
                                                    breakAll = true;
                                                    break;
                                                }
                                            }
                                            else if (value > 50 && value <= 75)
                                            {
                                                wardevil.ChangeToSolidColor(Color.Yellow);
                                                breakAll = true;
                                                break;
                                            }
                                            else if (value > 75 && value <= 100)
                                            {
                                                wardevil.ChangeToSolidColor(Color.Aqua);
                                                breakAll = true;
                                                break;
                                            }

                                        }
                                    }
                                }
                                if (breakAll) break;
                            }
						}
						if (EnableBatery)
						{
							float batteryLifePercent = SystemInformation.PowerStatus.BatteryLifePercent * 100f;
							if (!(batteryLifePercent <= 0f ? true : batteryLifePercent > 35f))
							{
								if (wardevil.CurrentSolidColor != Color.Red)
								{
									wardevil.ChangeToSolidColor(Color.Red);
								}
							}
							else if (!(batteryLifePercent <= 25f ? true : batteryLifePercent > 50f))
							{
								if (wardevil.CurrentSolidColor != Color.Orange)
								{
									wardevil.ChangeToSolidColor(Color.Orange);
								}
							}
							else if (!(batteryLifePercent <= 50f ? true : batteryLifePercent > 75f))
							{
								if (wardevil.CurrentSolidColor != Color.Yellow)
								{
									wardevil.ChangeToSolidColor(Color.Yellow);
								}
							}
							else if ((batteryLifePercent <= 75f ? false : batteryLifePercent <= 100f))
							{
								if (wardevil.CurrentSolidColor != Color.Green)
								{
									wardevil.ChangeToSolidColor(Color.Green);
								}
							}
						}
					}
					wardevil.Dispose();
				}
				finally
				{
					if (wardevil != null)
					{
						((IDisposable)wardevil).Dispose();
					}
				}
			}, this._token);
		}

		public void Cancel()
		{
			this._tokenSource.Cancel();
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
				if (this._pc != null)
				{
					this._pc.Close();
				}
				this._tokenSource.Cancel();
			}
		}
	}
}