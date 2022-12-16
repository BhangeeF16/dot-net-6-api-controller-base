using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                model.Enum.Clear();

                var names = Enum.GetNames(context.Type).ToList();

                names.ForEach(name => model.Enum.Add(new OpenApiString($"{GetEnumIntegerValue(name, context)} = {name}")));


                // the missing piece that will make sure that the new schema will not replace the mock value with a wrong value 
                // this is the default behavior - the first possible enum value as a default "example" value
                model.Example = new OpenApiInteger(GetEnumIntegerValue(names.First(), context));
            }
        }
        private int GetEnumIntegerValue(string name, SchemaFilterContext context) => Convert.ToInt32(Enum.Parse(context.Type, name));
    }
}
