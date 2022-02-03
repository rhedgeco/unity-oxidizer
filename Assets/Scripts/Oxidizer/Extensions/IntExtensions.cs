using System.Runtime.CompilerServices;

namespace Oxidizer.Extensions
{
    public static class IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Pow(this int value, uint pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= value;
                value *= value;
                pow >>= 1;
            }

            return ret;
        }
    }
}