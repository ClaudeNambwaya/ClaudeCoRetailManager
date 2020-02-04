﻿using Caliburn.Micro;
using CRMDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMDesktopUI.ViewModels
{
	public class LoginViewModel : Screen
	{
		private string _userName;
		private string _password;
		private readonly IAPIHelper _apiHelper;

		public LoginViewModel(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}

		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
				NotifyOfPropertyChange(() => Password);
			}
		}


		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);

			}
		}

		public bool CanLogIn
		{
			get
			{
				bool output = false;

				if (UserName?.Length > 0 && Password?.Length > 0)
				{
					output = true;
				}

				return output;
			}
		}

		public async Task LogIn()
		{
			try
			{
				_ = await _apiHelper.Authenticate(UserName, Password);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
	