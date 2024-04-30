using Domain.Tasks.Enums;
using System.Text.Json.Serialization;

namespace WebApi.Model.Tasks.Get
{
    public class GetTaskReturnViewModel
    {
        [JsonPropertyName("ID")]
        public Guid IdTask { get; set; }
        
        [JsonPropertyName("Titulo")]
        public string Title { get; set; }
        
        [JsonPropertyName("Descricao")]
        public string Description { get; set; }
        
        public string Status { get; set; }
        
        [JsonPropertyName("Data de criação")]
        public string CreationDate { get; set; }

        [JsonPropertyName("Email")]
        public string UserEmail { get; set; }
    }
}
