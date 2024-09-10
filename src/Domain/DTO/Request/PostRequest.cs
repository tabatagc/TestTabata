using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.DTO.Request
{
    public class PostRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(30, ErrorMessage = "Title should not have more than 30 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(1200, ErrorMessage = "Content should not have more than 1200 characters.")]
        public string Content { get; set; }
    }
}
