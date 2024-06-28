namespace DeerCoffeeShop.Domain.Common.Exceptions;

public class TimeCheckInToSoonException(string message) : Exception(message)
{
}
