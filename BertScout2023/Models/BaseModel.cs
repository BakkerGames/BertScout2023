using System.Text.Encodings.Web;
using System.Text.Json;

namespace BertScout2023.Models;

public class BaseModel
{
    public override string ToString()
    {
        JsonSerializerOptions WriteOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        return JsonSerializer.Serialize(this, WriteOptions);
    }
}