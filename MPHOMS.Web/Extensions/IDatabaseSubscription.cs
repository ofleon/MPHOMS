using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPHOMS.Web
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
