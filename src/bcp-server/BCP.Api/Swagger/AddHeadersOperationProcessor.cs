using NJsonSchema;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace BCP.Api.Swagger
{
    public class AddHeadersOperationProcessor : IOperationProcessor
    {
        public AddHeadersOperationProcessor()
        {
        }

        public bool Process(OperationProcessorContext context)
        {
            return true;
        }
    }
}
