﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Data.SqlClient;
using System.Net.Http;

using DataAccess.Models;

namespace UsageStatAPI
{

    public static class UsageStatsDataGatherer
    {

        static string ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=PkmnDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

        public static void CreateNewStatTable(string tableName)
        {

            SqlConnection conn = new SqlConnection(ConnectionString);
            string cmdString = $"CREATE TABLE dbo.{tableName}(" +
                               $"ID int PRIMARY KEY IDENTITY(1,1) NOT NULL," +
                               $"PkmnID int NOT NULL," +
                               $"RawCount int NOT NULL," +
                               $"Abilities nvarchar(300) NOT NULL," +
                               $"Items nvarchar(700) NOT NULL," +
                               $"Spreads nvarchar(650) NOT NULL," +
                               $"Moves nvarchar(900) NOT NULL," +
                               $"Teammates nvarchar(550) NOT NULL," +
                               $"ChecksAndCounters nvarchar(550) NOT NULL" +
                               $")";


            if (!TableExists(conn, "dbo", tableName))
            {


                SqlCommand cmd = new SqlCommand(cmdString, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table was created successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
            else
            {
                Console.WriteLine("Table already existed");
            }

        }

        private static bool TableExists(SqlConnection conn, string database, string name)
        {

            string strCmd = null;
            SqlCommand cmd = null;

            try
            {
                strCmd = "select case when exists((select '['+SCHEMA_NAME(schema_id)+'].['+name+']' As name FROM [" + database + "].sys.tables WHERE name = '" + name + "')) then 1 else 0 end";
                cmd = new SqlCommand(strCmd, conn);

                return (int)cmd.ExecuteScalar() == 1;
            }
            catch
            {
                return false;
            }

        }

        public static void GatherData(string tableName, SqlConnection conn = null)
        {

            string query = $"INSERT INTO dbo.{tableName}(Name, Viability, RawCount) VALUES(@Name, @Viability, @RawCount)";
            if(conn == null)
            {
                conn = new SqlConnection(ConnectionString);
            }

            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", "Test0");
                cmd.Parameters.AddWithValue("@Viability", 10);
                cmd.Parameters.AddWithValue("@RawCount", 90);
                cmd.ExecuteNonQuery();

            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                conn.Close();

            }

            Console.WriteLine("Data Gathered and added to table:  " + tableName + "  successfully.");

        }

        public static string GetUsageStatText(string url)
        {

            HttpClient client = new HttpClient();
            string result = client.GetStringAsync(url).Result;

            return result;

        }

        private static string TrimStatLine(string s, char[] chars)
        {

            foreach(char c in chars)
            {
                s = s.Replace(c.ToString(), String.Empty);
            }
            s = s.Trim();

            return s;

        }

        private static void InsertPokemonStat(PokemonStatModel stat, string tableName, SqlConnection conn = null)
        {

            string query = $"INSERT INTO dbo.{tableName}(PkmnId, RawCount, Abilities, Items, Spreads, Moves, Teammates, ChecksAndCounters)";
            query += " VALUES(@PkmnID, @RawCount, @Abilities, @Items, @Spreads, @Moves, @Teammates, @ChecksAndCounters)";

            if(conn == null)
            {
                conn = new SqlConnection(ConnectionString);
            }

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PkmnID", stat.PkmnID);
                cmd.Parameters.AddWithValue("@RawCount", stat.RawCount);
                cmd.Parameters.AddWithValue("@Abilities", stat.Abilities);
                cmd.Parameters.AddWithValue("@Items", stat.Items);
                cmd.Parameters.AddWithValue("@Spreads", stat.Spreads);
                cmd.Parameters.AddWithValue("@Moves", stat.Moves);
                cmd.Parameters.AddWithValue("@Teammates", stat.Teammates);
                cmd.Parameters.AddWithValue("@ChecksAndCounters", stat.ChecksAndCounters);
                cmd.ExecuteNonQuery();

            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public static void ParseUsageStats(string url, string tableName)
        {

            CreateNewStatTable(tableName);
            SqlConnection conn = new SqlConnection(ConnectionString);

            string text = GetUsageStatText(url);

            using (StringReader reader = new StringReader(text))
            {

                int sectionIndex = 0;
                char[] removechars = new char[] {'|'};

                string name = string.Empty;
                PokemonStatModel stat = new PokemonStatModel();
                stat.ID = 0;
                stat.PkmnID = 0;

                string line = reader.ReadLine();
                while (line != null)
                {

                    //Check for new section line
                    if (line.Contains("+"))
                    {
                        sectionIndex++;
                        line = reader.ReadLine();

                        //Next Section, submit current one
                        if(sectionIndex >= 9)
                        {



                            sectionIndex = 0;
                            stat.Abilities = stat.Abilities.Trim(',');
                            stat.Items = stat.Items.Trim(',');
                            stat.Moves = stat.Moves.Trim(',');
                            stat.Spreads = stat.Spreads.Trim(',');
                            stat.Teammates = stat.Teammates.Trim(',');
                            stat.ChecksAndCounters = stat.ChecksAndCounters.Trim(',');

                            //Insert PokemonStat into DB
                            //stat.Print();
                            InsertPokemonStat(stat, tableName, conn);

                            stat.Abilities = "";
                            stat.Items = "";
                            stat.Moves = "";
                            stat.Spreads = "";
                            stat.Teammates = "";
                            stat.ChecksAndCounters = "";

                        }

                        continue;
                    }

                    

                    switch (sectionIndex)
                    {
                        //Parse NAME
                        case 1:
                            name = line.Trim();
                            break;
                        //Parse meta data
                        case 2:
                            if(line.Contains("Raw count:"))
                            {
                                string[] splitString = line.Split(':');
                                stat.RawCount = Int32.Parse(splitString[1].Replace("|", String.Empty));
                            }
                            break;
                            //Parse ABILITIES
                        case 3:
                            if (line.Contains("Abilities"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            stat.Abilities += TrimStatLine(line, removechars) + ",";

                            break;
                        //Parse ITEMS
                        case 4:

                            if (line.Contains("Items"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            stat.Items += TrimStatLine(line, removechars) + ",";

                            break;
                        //Parse SPREADS
                        case 5:

                            if (line.Contains("Spreads"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            stat.Spreads += TrimStatLine(line, removechars) + ",";

                            break;
                        //Parse MOVES
                        case 6:
                            if (line.Contains("Moves"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            stat.Moves += TrimStatLine(line, removechars) + ",";

                            break;
                        //Parse TEAMMATES
                        case 7:

                            if (line.Contains("Teammates"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            stat.Teammates += TrimStatLine(line, removechars) + ",";

                            break;
                        //PARSE CHECKS AND COUNTERS
                        case 8:

                            if (line.Contains("Checks and Counters") || line.Contains("/"))
                            {
                                line = reader.ReadLine();
                                continue;
                            }

                            int rmvIndex = line.IndexOf('(');
                            line = line.Remove(rmvIndex);
                            stat.ChecksAndCounters += TrimStatLine(line, removechars) + ",";

                            break;
                        
                    }

                    line = reader.ReadLine();

                }

            }

        }

    }
}