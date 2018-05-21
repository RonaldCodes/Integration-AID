namespace Agent
{
    public static class Extensions
    {
        public static string CleanVehicleReg(this string s)
        {
            if (s == null) return "";
            return s.Replace(".", "").Trim();
        }
    }
}
