using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class UploadImageViewModel
    {
        [Required(ErrorMessage = "Imagem inválida.")]
        public string Base64Image { get; set; }
    }
}
