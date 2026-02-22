using System.Diagnostics;

namespace Atlas.Exceptions
{
    public class DomainException(string message, Exception? innerException = null)
        : Exception(message, innerException)
    {
        [StackTraceHidden]
        public static void ThrowIfNull(object? argument, string message)
        {
            if (argument is null)
            {
                throw new DomainException(message);
            }
        }

        [StackTraceHidden]
        public static void ThrowIfNullOrEmpty(string? argument, string message)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new DomainException(message);
            }
        }

        [StackTraceHidden]
        public static void ThrowIfNullOrWhiteSpace(string? argument, string message)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new DomainException(message);
            }
        }

        [StackTraceHidden]
        public static void ThrowIfGreaterThan(int argument, int max, string message)
        {
            if (argument > max)
            {
                throw new DomainException(message);
            }
        }

        [StackTraceHidden]
        public static void ThrowIfOutOfRange(
            int argument,
            int min,
            int max,
            string minMessage,
            string maxMessage
        )
        {
            if (argument < min)
            {
                throw new DomainException(minMessage);
            }

            if (argument > max)
            {
                throw new DomainException(maxMessage);
            }
        }
    }
}
