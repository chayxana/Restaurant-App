using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
    public class UserInfoViewModel : ReactiveObject, IUserInfoViewModel
	{
		private string _name;

		public string Name
		{
			get => _name;
			set => this.RaiseAndSetIfChanged(ref _name, value);
		}


		private string _lastName;

		public string LastName
		{
			get => _lastName;
			set => this.RaiseAndSetIfChanged(ref _lastName, value);
		}


		private DateTime _birthDate;

		public DateTime BirthDate
		{
			get => _birthDate;
			set => this.RaiseAndSetIfChanged(ref _birthDate, value);
		}

		private string _picture;

		public string Picture
		{
			get => _picture;
			set => this.RaiseAndSetIfChanged(ref _picture, value);
		}
	}
}
