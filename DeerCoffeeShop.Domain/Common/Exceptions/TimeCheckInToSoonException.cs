using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Domain.Common.Exceptions;

public class TimeCheckInToSoonException(string message) : Exception(message)
{
}
