using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using MPHOMS.Web.Repository;
using MPHOMS.Web.Models.Delivery;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;
using MPHOMS.Web.Hubs;

namespace MPHOMS.Web.Hubs
{
    public class SignalOrderDatabaseSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;
        private readonly IHubContext<SignalOrder> _hubContext;
        private SqlTableDependency<OrderNotificationStore1> _tableDependency;

        public SignalOrderDatabaseSubscription(IHubContext<SignalOrder> hubContext)
        {
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            _tableDependency = new SqlTableDependency<OrderNotificationStore1>(connectionString, "OrderNotificationStore1", "dbo", null, null, null, DmlTriggerType.Insert);
            _tableDependency.OnChanged += this.TableChanged;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            Console.WriteLine("Waiting for receiving notifications...");
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void TableChanged(object sender, RecordChangedEventArgs<OrderNotificationStore1> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                var changedEntity = e.Entity;
                int OrderId = changedEntity.OrderId;
                _hubContext.Clients.All.SendAsync("refreshOrdersinsert", OrderId);
            }
        }

        #region IDisposable

        ~SignalOrderDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
