using Acme.Greenhouse.Nodes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Volo.Abp.Identity;

namespace Acme.Greenhouse.Extensions
{
    public static class ExtensionMethods
    {
        public static IQueryable<Node> IncludeDetails(this IQueryable<Node> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Devices)
                .Include(x => x.Sensors);
        }

    }
}
