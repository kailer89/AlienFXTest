using AlienFXTest.Helpers;
using System;
using System.Drawing;
using System.ServiceProcess;
using System.Threading;

namespace AlienFXTest
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			Wardevil wardevil = new Wardevil();
			try
			{
				wardevil.ChangeToSolidColor(Color.Red);
			}
			finally
			{
				if (wardevil != null)
				{
					((IDisposable)wardevil).Dispose();
				}
			}
			ServiceBase.Run(new ServiceBase[] { new Service1() });
		}
	}
}