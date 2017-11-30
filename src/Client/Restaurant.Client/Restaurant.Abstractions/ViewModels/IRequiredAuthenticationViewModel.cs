using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IRequiredAuthenticationViewModel
    {
	    Task<string> GetAccessToken();
	}
}
