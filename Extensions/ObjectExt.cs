using Grpc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Forms.Grpc.Extensions
{
    public static class ObjectExt
    {
        /// <summary>
        /// Validate EditContext DataAnnotations<br/>
        /// Note: Server-Side should never result to error. The only way for validation error occurs server-side its either (attack) or mistake from client side. Validation form should be shared with server-client.
        /// </summary>
        /// <param name="throwOnError">True = Will throw GRPC InvalidArgument error if fails.</param>
        public static bool ValidateForm(this object context, bool throwOnError = true)
        {
            try
            {
                var ctx = new ValidationContext(context);
                bool rez = Validator.TryValidateObject(context, ctx, null, true);
                if (!rez)
                {
                    if (throwOnError)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        return rez;
                    }

                }
                else
                {
                    return rez;
                }
            }
            catch
            {

                throw new RpcException(new Status(StatusCode.InvalidArgument, "Error cannot validate the form."));
            }

        }
    }
}
