using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FootballNews.WebApp.Extensions.Validators
{
    public class MinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;
        public MinimumElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            if (value is IList list)
            {
                return list.Count >= _minElements;
            }
            return false;
        }
    }
}