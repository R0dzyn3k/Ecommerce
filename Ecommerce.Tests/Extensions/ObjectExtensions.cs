using System.Text;
using Newtonsoft.Json;

namespace Ecommerce.Tests.Extensions;

public static class ObjectExtensions
{
    public static StringContent ToJsonContent(this object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return content;
    }
}