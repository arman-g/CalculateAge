namespace example.Extensions
{
    public enum Leaplings
    {
        /// <summary>
        /// Celebrated on Feb 28.
        /// </summary>
        Feb28,
        /// <summary>
        /// Celebrated on March 1.
        /// </summary>
        March1
    }

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Calculate the age of a person based on date of birth.
        /// </summary>
        /// <param name="dob">The date of birth of the person.</param>
        /// <param name="leaplings">Indicates the leap birth celebration day.</param>
        /// <param name="presentDate">The present date. If not set defaults to <see cref="DateTime.Now"/>.</param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime dob,
            Leaplings leaplings = Leaplings.Feb28,
            DateTime? presentDate = null)
        {
            presentDate ??= DateTime.Now.ToPacificDate();
            var age = presentDate.Value.Year - dob.Year;
            if (leaplings == Leaplings.Feb28 &&
                !DateTime.IsLeapYear(presentDate.Value.Year) &&
                DateTime.IsLeapYear(dob.Year) &&
                dob.DayOfYear == 60 &&
                presentDate.Value.DayOfYear == 59)
            {
                return age;
            }

            if (presentDate.Value.DayOfYear < dob.DayOfYear)
            {
                return age - 1;
            }
            return age;
        }
    }
}
