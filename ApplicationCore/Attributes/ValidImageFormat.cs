using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidImageFormat : ValidationAttribute
    {
        private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

        public override bool IsValid(object? value)
        {
            IFormFile? file = value as IFormFile;
            if (file == null)
                return true;

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(fileExtension))
            {
                ErrorMessage = "Only JPEG or PNG image format is allowed.";
                return false;
            }

            return true;
        }
    }
}
