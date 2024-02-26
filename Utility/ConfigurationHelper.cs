using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MyUtility;

public class ConfigurationHelper
{
	private static ConfigurationHelper _instance;
	private IConfiguration _configuration;

	public static ConfigurationHelper Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ConfigurationHelper();
			}
			return _instance;
		}
	}

	public void Initialize(IConfiguration configuration)
	{
		this._configuration = configuration;
	}
	public string GetConnectionString()
	{
		string consStr = string.Empty;
		consStr = this._configuration.GetConnectionString("DefaultConnection");
		return consStr;
	}
}
