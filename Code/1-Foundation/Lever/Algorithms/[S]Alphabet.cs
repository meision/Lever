using System;

namespace Meision.Algorithms
{
    public static class Alphabet
    {
        private const string AlphaTemplate = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericTemplate = "0123456789";
        private const string AlphanumericTemplate = AlphaTemplate + NumericTemplate;

        public static string GenerateNumeric(int size)
        {
            ThrowHelper.ArgumentMustNotNegative((size < 0), nameof(size));

            Random random = new Random();
            char[] buffer = new char[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = NumericTemplate[random.Next(NumericTemplate.Length)];
            }
            return new string(buffer);
        }

        public static string GenerateAlpha(int size)
        {
            ThrowHelper.ArgumentMustNotNegative((size < 0), nameof(size));

            Random random = new Random();
            char[] buffer = new char[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = AlphaTemplate[random.Next(AlphaTemplate.Length)];
            }
            return new string(buffer);
        }

        public static string GenerateAlphanumeric(int size)
        {
            ThrowHelper.ArgumentMustNotNegative((size < 0), nameof(size));

            Random random = new Random();
            char[] buffer = new char[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = AlphanumericTemplate[random.Next(AlphanumericTemplate.Length)];
            }
            return new string(buffer);
        }

    }
}
