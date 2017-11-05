using Storage.Core.Providers.Interfaces;
using System.Web;

namespace Storage.Core.Providers
{
    public class HttpContextStorageProvider : IContextStorageProvider<HttpContext>
    {
        public HttpContext Context => HttpContext.Current;
    }
}
