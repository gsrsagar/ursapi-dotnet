using System.ComponentModel.DataAnnotations;

namespace urs_api.Models
{
   
    public class Attachments
    {

        [Key]
        public long Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FileExt { get; set; }
        [Required]
        public byte[] FileContents { get; set; }
        [Required]
        public int? FileSizeBytes { get; set; }
        [Required]
        public int? ContentLength { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]
        public string OriginalFilePath { get; set; }
        [Required]
        public string BrowserInfo { get; set; }
        [Required]
        public string LastUploadedBy { get; set; }
        [Required]
        public DateTime? LastUploadedDate { get; set; }
        [Required]
        public string LastDownloadedBy { get; set; }
        [Required]
        public DateTime? LastDownloadedDate { get; set; }
        [Required]
        public string LastUpdateBy { get; set; }
        [Required]
        public DateTime? LastUpdateDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime? CreatedDate { get; set; }
    }
}
