using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InfoDev.IOffice.Models.HumanResources
{
    public class FileUpload
    {
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }


        [Required]
        [StringLength(50)]
        public string DocGuid { get; set; }

        [Required]
        [StringLength(10)]
        public string FileExtention { get; set; }

        [Required]
        [StringLength(50)]
        public string MimeType { get; set; }

        [Required]
        public byte[] File { get; set; }
    }
}
