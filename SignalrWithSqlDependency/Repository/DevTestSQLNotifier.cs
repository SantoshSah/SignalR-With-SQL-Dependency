using SignalrWithSqlDependency.Hubs;
using SignalrWithSqlDependency.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SignalrWithSqlDependency.Repository
{
    public class DevTestSQLNotifier
    {
        SignalrWithSqlDependencyEntities db = new SignalrWithSqlDependencyEntities();
        public IEnumerable<DevTestModel> GetData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                //we can also use stored procedure. 
                using (SqlCommand command = new SqlCommand(@"SELECT [ID],[CampaignName],[Date],[Clicks],[Conversions],[Impressions],
                                                            [AffiliateName] FROM [dbo].[DevTest]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new DevTestModel()
                            {
                                ID = x.GetInt32(reader.GetOrdinal("ID")),
                                CampaignName = x.GetString(reader.GetOrdinal("CampaignName")),
                                Date = x.GetDateTime(reader.GetOrdinal("Date")),
                                Clicks = x.GetInt32(reader.GetOrdinal("Clicks")),
                                Conversions = x.GetInt32(reader.GetOrdinal("Conversions")),
                                Impressions = x.GetInt32(reader.GetOrdinal("Impressions")),
                                AffiliateName = x.GetString(reader.GetOrdinal("AffiliateName"))
                            }).ToList();
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            DevTestHub.Show();
        }

    }
}