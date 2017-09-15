using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSample2
{
    public class TableSndService : ITableSndService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Insert(int value)
        {
            using (SqlConnection conn = new SqlConnection(References.References.ConnectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = string.Format("INSERT dbo.Service2(amount) VALUES ({0})", value);
                    command.ExecuteNonQuery();
                }
            }
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void InsertWithFail(int value)
        {
            throw new FaultException(string.Format("normal fault: {0}", value));
        }
    }
}
