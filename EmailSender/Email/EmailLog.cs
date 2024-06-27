using InfoDev.IOffice.ICommon.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InfoDev.IOffice.Models.Email
{
    public class EmailLog 
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public  string EmailFrom { get; set; }
        [Required]
        public string EmailTo { get; set; }
        [StringLength(1000)]
        public string Cc { get; set; }
        [StringLength(1000)]
        public string BCc { get; set; }
        [Required]
        public string Content { get; set; }
        public string Subject { get; set; }
        [Required]
        public int RetryCount { get; set; }
        [Required]
        public int BranchId { get; set; }
        public int MenuId { get; set; }
      
        public EmailStatus EmailStatus { get; set; }
        public string Remarks { get; set; }

        //[ForeignKey("BranchId")]
        //public Branch Branch { get; set; }

     

        //[ForeignKey("MenuId")]
        //public SyncApplicationMenu Menu { get; set; }

        public List<EmailAttachmentDoc> EmailAttachmentDoc { get; set; }
    }
}
