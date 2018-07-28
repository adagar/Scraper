using System;
using Amazon.Lambda.Core;

[assembly: LambdaSerializerAttribute(
    typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace lambda_example
{
    public class MyClass
    {
        public object MyFunction()
        {
            return new
            {
                message = "Hello from Lambda!",
                time = DateTime.UtcNow
            };
        }
    }
}
