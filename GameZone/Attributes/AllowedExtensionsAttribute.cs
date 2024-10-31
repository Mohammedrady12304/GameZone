using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute :ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;//كده انا حولت ال value اللي استقبلتها الى IFormFile

            if(file is not null)
            {
                var fileName = file.FileName;
                var extension =Path.GetExtension(fileName);
                if (!_allowedExtensions.Split(",").Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"only this extension are allowed : {_allowedExtensions}");//دي كده error message
                }
                

            }
            return ValidationResult.Success;//كده لو مفيش file اتحط مفيش error هيرجع من هنا ممكن يرجع من هناك عادي من عند ال ViewForm
        }
    }
}
