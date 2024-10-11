using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pkix;
using System.ComponentModel.Design;
using System.Configuration;
using System.Text.RegularExpressions;


namespace NJUArchery_SQL_Component
{
    internal class Program
    {
        static ILogger? _logger;

        static void Main(string[] args)
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            //Waiting for implement for input pwd via configuration...


            var services = new ServiceCollection();

            services.AddLogging(logger => logger.AddConsole());

            var serviceProvider=services.BuildServiceProvider();

            _logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            var builder=GetConnectionString();
            MySqlConnection mySqlConnection = new MySqlConnection();
            mySqlConnection.ConnectionString = builder.ConnectionString;

            try 
            {
                mySqlConnection.Open();
                _logger.LogInformation("Database connection successed");
            }
            catch(MySqlException e) { _logger.LogError($"Database connection failed, with {e}"); }

        }


        /// <summary>
        /// Mannually set connectionString via console inputs.
        /// </summary>
        /// <returns></returns>
        static MySqlConnectionStringBuilder GetConnectionString()
        {

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

            var host = Tool.ReadLineWithPrompt("Host");
            builder.Add("server", host);

            var port=Tool.ReadLineWithPrompt("Port");
            builder.Add("port", port);

            var userName = Tool.ReadLineWithPrompt("Username");
            builder.Add("uid", userName);

            var pwd = Tool.ReadLineWithPrompt("Password",true);
            builder.Add("pwd", pwd);

            var database = Tool.ReadLineWithPrompt("Database");
            builder.Add("database", database);

            Dictionary<string,string> optional = new Dictionary<string,string>();

            Console.WriteLine("With optional parameters, inputs follow the format key:value, end inputs with Exit.");

            Regex optionReg = new Regex("^([A-Za-z0-9]+)：(^[A-Za-z0-9]+)$");

            while (true)
            {
                var inputStr=Console.ReadLine();

                if (inputStr == "Exit")
                {
                    return builder;
                }

                else if (inputStr != null && optionReg.IsMatch(inputStr))
                {
                    var match = optionReg.Match(inputStr);

                    builder.Add(match.Groups[1].Value, match.Groups[2].Value);
                }
                else { Console.WriteLine("Invalid input..."); }

;            }
        }


        
    }
}
