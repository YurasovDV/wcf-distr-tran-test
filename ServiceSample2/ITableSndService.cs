using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceSample2
{
    [ServiceContract]
    public interface ITableSndService
    {
        [OperationContract, TransactionFlow(TransactionFlowOption.Mandatory)]
        [FaultContract(typeof(string))]
        void Insert(int value);

        [OperationContract, TransactionFlow(TransactionFlowOption.Mandatory)]
        [FaultContract(typeof(string))]
        void InsertWithFail(int value);
    }
}
