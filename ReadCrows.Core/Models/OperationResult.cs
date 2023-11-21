

namespace ReadCrows.Core.Models
{
    public class OperationResult
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
       
    }
}
