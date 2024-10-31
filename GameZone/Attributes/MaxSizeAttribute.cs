using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class MaxSizeAttribute :ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
                var fileSize = file.Length;
                if (fileSize > _maxSize) {
                    return new ValidationResult($"the allowed max size is {_maxSize/1024/1024}MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}
