﻿using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE.ETLTools {
    public class AdomdConnectionManager : DbConnectionManager<AdomdConnection, AdomdCommand> { 

        public AdomdConnectionManager() : base() { }

        public AdomdConnectionManager(ConnectionString connectionString) : base(connectionString) { }

        public override void BulkInsert(IDataReader data, IColumnMappingCollection columnMapping, string tableName) {
            throw new NotImplementedException();
        }

    }
}