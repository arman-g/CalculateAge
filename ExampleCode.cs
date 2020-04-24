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
		presentDate ??= DateTime.Now;
		var age = new Age
		{
			Years = (ushort)(presentDate.Value.Year - dob.Year)
		};

		var decrement =
			!(!DateTime.IsLeapYear(presentDate.Value.Year) &&
			  DateTime.IsLeapYear(dob.Year) &&
			  dob.DayOfYear == 60 &&
			  presentDate.Value.DayOfYear == 59) &&
			presentDate.Value.DayOfYear < dob.DayOfYear;

		if (decrement) { age.Years -= 1; }

		var tempDt = dob.AddYears(age.Years);
		if (decrement) { age.Months = 11; }

		age.Months += (byte)(presentDate.Value.Month - tempDt.Month);
		if (tempDt.AddMonths(age.Months) > presentDate.Value) { age.Months -= 1; }

		tempDt = tempDt.AddMonths(age.Months);
		age.Days = (byte)presentDate.Value.Subtract(tempDt).Days;
		
		return age;
	}
}
