using AlienFXTest.Helpers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.ServiceProcess;

namespace AlienFXTest
{
	public class Service1 : ServiceBase
	{
		private Teacup cup;

		private IContainer components = null;

		public Service1()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.ServiceName = "Service1";
		}

		protected override void OnStart(string[] args)
		{
			DateTime now = DateTime.Now;
			bool flag = false;
			bool flag1 = false;
			if ((int)args.Length <= 0)
			{
				this.cup = new Teacup(false, false);
			}
			else
			{
				if (((int)args.Length <= 0 ? false : args[0].ToString().ToUpper() == "enablegpu".ToUpper()))
				{
					flag = true;
				}
				if (((int)args.Length <= 0 ? false : args[0].ToString().ToUpper() == "enablebattery".ToUpper()))
				{
					flag1 = true;
				}
				if (((int)args.Length <= 1 ? false : args[1].ToString().ToUpper() == "enablegpu".ToUpper()))
				{
					flag = true;
				}
				if (((int)args.Length <= 1 ? false : args[1].ToString().ToUpper() == "enablebattery".ToUpper()))
				{
					flag1 = true;
				}
				this.cup = new Teacup(flag, flag1);
			}
		}

		protected override void OnStop()
		{
			Wardevil wardevil = new Wardevil();
			try
			{
				wardevil.ChangeToSolidColor(Color.Aquamarine);
			}
			finally
			{
				if (wardevil != null)
				{
					((IDisposable)wardevil).Dispose();
				}
			}
			if (this.cup != null)
			{
				this.cup.Dispose();
			}
		}
	}
}