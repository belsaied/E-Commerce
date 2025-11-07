using System.Text.Json;

namespace Shared.ErrorModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        // for Validation in Authentication.
        public IEnumerable<string>? Errors { get; set; }
        public override string ToString()
          => JsonSerializer.Serialize(this);
            
        
    } 
}
