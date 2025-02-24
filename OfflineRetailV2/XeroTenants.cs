using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRetailV2
{
    public class Tenant
    {
        public Tenant(string tenantId, string tenantName)
        {
            Id = tenantId;
            Name = tenantName;
        }

        public string Id { get; }

        public string Name { get; }
    }
}
