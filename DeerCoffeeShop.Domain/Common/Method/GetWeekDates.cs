namespace DeerCoffeeShop.Domain.Common.Method;

public static class GetWeekDates
{
    public static List<DateOnly> Get(DateOnly date)
    {
        List<DateOnly> weekDates = [];

        // Get the day of the week as an integer (0 = Sunday, 1 = Monday, ..., 6 = Saturday)
        int dayOfWeek = (int)date.DayOfWeek;

        // Calculate the previous Sunday
        DateOnly startOfWeek = date.AddDays(-dayOfWeek);

        // Add each day from the previous Sunday to the next Saturday to the list
        for (int i = 0; i < 7; i++)
        {
            weekDates.Add(startOfWeek.AddDays(i));
        }

        return weekDates;
    }
}
