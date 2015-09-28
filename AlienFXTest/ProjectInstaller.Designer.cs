using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace AlienFXTest
{
	[RunInstaller(true)]
	public class ProjectInstaller : Installer
	{
		private IContainer components = null;

		private ServiceProcessInstaller serviceProcessInstaller1;

		private ServiceInstaller serviceInstaller1;

		public ProjectInstaller()
		{
			this.InitializeComponent();
			this.serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
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
			this.serviceProcessInstaller1 = new ServiceProcessInstaller();
			this.serviceInstaller1 = new ServiceInstaller();
			this.serviceProcessInstaller1.Password = null;
			this.serviceProcessInstaller1.Username = null;
			this.serviceInstaller1.ServiceName = "Service1";
			InstallerCollection installers = base.Installers;
			Installer[] installerArray = new Installer[] { this.serviceProcessInstaller1, this.serviceInstaller1 };
			installers.AddRange(installerArray);
		}
	}
}