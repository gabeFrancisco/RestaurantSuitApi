using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace RecantosSystem.Api.Services.Logging
{
	public class LogService
	{
		private readonly ILogger<LogService> _logger;
		private readonly string _dir = $@"{Directory.GetCurrentDirectory()}/Logs";
		private readonly string _file = $"log_{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
		private readonly string _fullPath;
		public LogService(ILogger<LogService> logger)
		{
			_logger = logger;
			_fullPath = Path.Combine(_dir, _file);

			if (!Directory.Exists(_dir))
			{
				Directory.CreateDirectory(_dir);
			}

			if (!File.Exists(_fullPath))
			{
				var created = File.Create(_fullPath);
				created.Close();
			}
		}

		public void LogMessage(string message)
		{
			File.AppendAllText(_fullPath, $"\nInformation: [{DateTime.Now}] = {message}");
		}

		public void LogException(Exception ex)
		{
			File.AppendAllText(_fullPath, $"\nException: [{DateTime.Now}] = {ex}");
		}

        public void LogException(Exception ex, string typeLocation)
		{
			File.AppendAllText(_fullPath, $"\nException on {typeLocation}: [{DateTime.Now}] = {ex}");
		}
	}
}