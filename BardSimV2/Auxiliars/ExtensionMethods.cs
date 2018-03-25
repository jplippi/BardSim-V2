namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static int SecondsToMilli(this decimal m)
        {
            return (int)(m * 1000);
        }

        public static decimal MilliToSeconds(this int i)
        {
            return (decimal)i / 1000;
        }

        public static decimal MilliToSeconds(this long l)
        {
            return (decimal)l / 1000;
        }

        public static decimal MilliToSeconds(this ulong u)
        {
            return (decimal)u / 1000;
        }
    }
}
