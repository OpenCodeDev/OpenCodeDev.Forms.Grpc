using Grpc.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Forms.Grpc.Extensions
{
    public static class ProtoConversionExt
    {
    /// <summary>
    /// Convert EfCore Table to Client Equivalent. <br/>
    /// Note: System simply convert Entity Framework Table to Grpc Message Object using Json.<br/>
    /// Note 2: I could not find a quick way to achieve json-like conversion with grpc.... if you have any idea or a better and faster way to achieve it feel free to contribute.
    /// </summary>
    /// <typeparam name="T">Target Object</typeparam>
    /// <returns></returns>
        public static T ToProtoMessage<T>(this object obj)
        {
            try
            {
                JsonSerializerSettings f = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj, f));
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Fatal Internal Error, This has been reported for further investigation."));
            }
        }
    }
}
