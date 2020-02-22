using System;
using System.IO;
using System.Net.Mime;

namespace FootballNews.Core
{
    public static class GuardExtensions
    {
        public static void ThrowIfEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException($"Required input {argumentName} was empty.", argumentName);
            }
        }

        public static void ThrowIfNull(object argument, string argumentName)
        {
            if (argument is null)
            {
                throw new ArgumentNullException($"{argumentName}", "Required parameter {argumentName} was null.");
            }
        }

        public static void ThrowIfBiggerThan(int argument, int maxValue, string argumentName)
        {
            if (argument > maxValue)
            {
                throw new ArgumentOutOfRangeException(argumentName, argument, $"Value of passed argument ({argument})was bigger than max value {maxValue}");
            }
        }
        
    }
}