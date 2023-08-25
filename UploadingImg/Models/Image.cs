using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UploadingImg.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string NameFile { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public HttpPostedFileBase file { get; set; }
    }
}