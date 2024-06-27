using InfoDev.IOffice.Models.HumanResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InfoDev.IOffice.Models.Email
{
    public class EmailAttachmentDoc : FileUpload
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmailLogId { get; set; }

        [Required]
        [Column(TypeName = "Date"), DataType(DataType.Date)]
        public DateTime DocCaptureDate { get; set; }

        [Required]
        public int DocCaptureUserId { get; set; }

        [ForeignKey("EmailLogId")]
        public EmailLog EmailLog { get; set; }

        //[ForeignKey("DocCaptureUserId")]
        //public SyncApplicationUser ApplicationUser { get; set; }
    }
}
