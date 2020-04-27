using System;

namespace age.calculation.example
{
    [DebuggerDisplay(
    nameof(Years) + " = {" + nameof(Years) + "}, " +
    nameof(Months) + " = {" + nameof(Months) + "}, " +
    nameof(Days) + " = {" + nameof(Days) + "}")]
    public class Age
    {
	public ushort Years { get; set; }
	public byte Months { get; set; }
	public byte Days { get; set; }

	public override string ToString()
	{
	return $"{Years}yr., {Months}mos., {Days}d";
	}
    }

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Indicates whether this date is in leap year.
        /// </summary>
        /// <param name="value">This <see cref="DateTime"/> instance.</param>
        /// <returns>A boolean value indicating whether this date instance is in leap your.</returns>
        public static bool IsInLeapYear(this DateTime value)
        {
            return DateTime.IsLeapYear(value.Year);
        }

        /// <summary>
        /// Calculate the age of a person based on the date of birth.
        /// </summary>
        /// <param name="dob">The date of birth of the person.</param>
        /// <param name="presentDate">The present date. Defaults to <see cref="DateTime.Now"/>, if not specified.</param>
        /// <remarks>This function supports leaper DOBs (Feb 29).</remarks>
        /// <returns>An <see cref="Age"/> object containing years, months and days information.</returns>
        public static Age CalculateAge(
            this DateTime dob,
            DateTime? presentDate = null)
        {
            presentDate ??= DateTime.Now.ToPacificDate();
            if (dob > presentDate) throw new ArgumentOutOfRangeException(
                nameof(dob),
                "DOB must be less or equal to the present date.");

            const byte maxMonths = 12;
            const byte feb28 = 59;

            // indicates whether dob year has extra day compare to present date.
            // Note, leap years have 366 days vs to 365 for regular years.
            var extraDay = dob.IsInLeapYear() &&
                           !presentDate.Value.IsInLeapYear() &&
                           dob.DayOfYear > feb28 ? 1 : 0;

            // indicates whether dob has been celebrated in the present year.
            var hasDobOccur = presentDate.Value.DayOfYear >= dob.DayOfYear - extraDay;

            // calculate the years of age
            var age = new Age
            {
                Years = (ushort)(presentDate.Value.Year - dob.Year - (hasDobOccur ? 0 : 1))
            };

            // calculate the months of age
            if (hasDobOccur)
            {
                age.Months = (byte)(presentDate.Value.Month - dob.Month);
                if (age.Months > 0 & presentDate.Value.Day < dob.Day)
                {
                    age.Months -= 1;
                }
            }
            else
            {
                age.Months = (byte)(maxMonths - 1 - Math.Abs(presentDate.Value.Month - dob.Month));
            }

            // calculate the days of age
            var presentMonth = dob.Month + age.Months;
            if (presentMonth > maxMonths)
            {
                presentMonth = Math.Abs(presentMonth - maxMonths);
            }

            if (presentMonth == presentDate.Value.Month)
            {
                age.Days = (byte)(presentDate.Value.Day - (dob.Day - extraDay));
            }
            else
            {
                age.Days = (byte)(
                    DateTime.DaysInMonth(dob.Year + age.Years, presentMonth) -
                    (dob.Day - (hasDobOccur ? extraDay : 0)) +
                    presentDate.Value.Day);
            }

            return age;
        }
    }
}
