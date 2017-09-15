using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;
using System.Transactions;

namespace WCFDistrTran
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("press any key?");

            Console.ReadKey();
            try
            {
                EnsureTablesExist();
                 Success();
                //FailSecond();
                Console.WriteLine("end");
            }
            catch (Exception ex)
            {
                Console.WriteLine("catch: " + ex.Message);
            }
        }

        private static void Success()
        {
            using (TransactionScope s = new TransactionScope())
            {
                var c1 = new FirstTable.TableFstServiceClient();
                var c2 = new SecondTable.TableSndServiceClient();

                c1.Insert(100);
                c2.Insert(200);

                s.Complete();
            }
        }

        private static void FailSecond()
        {
            using (TransactionScope s = new TransactionScope())
            {
                try
                {
                    var c1 = new FirstTable.TableFstServiceClient();
                    var c2 = new SecondTable.TableSndServiceClient();

                    c1.Insert(300);
                    c2.InsertWithFail(400);
                }
                catch (Exception)
                {
                    Console.WriteLine("see?");
                    throw;
                }

                s.Complete();
            }
        }

        private static void EnsureTablesExist()
        {
            var srv = new Server((string)References.References.ServerName);
            Database db = new Database(srv, References.References.DatabaseName);
            db.Create();

            using (SqlConnection conn = new SqlConnection(References.References.ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = FirstTable;
                    command.ExecuteNonQuery();

                    command.CommandText = SecondTable;
                    command.ExecuteNonQuery();
                }
            }
        }

        public const string FirstTable = @"IF OBJECT_ID('dbo.Service1', N'U') IS NULL
                                            BEGIN
                                            	CREATE TABLE Service1 
                                            	(
                                            	ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
                                            	amount INT
                                            	)
                                            END";

        public const string SecondTable = @"IF OBJECT_ID('dbo.Service2', N'U') IS NULL
                                            BEGIN
                                            	CREATE TABLE Service2 
                                            	(
                                            	ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
                                            	amount INT
                                            	)
                                            END";


    }
}
