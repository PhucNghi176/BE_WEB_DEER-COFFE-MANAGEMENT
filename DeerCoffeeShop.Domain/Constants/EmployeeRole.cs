namespace DeerCoffeeShop.Domain.Constants
{
    public static class EmployeeRole
    {
        public static readonly Dictionary<int, string> EmployeeRoleDictionary = new()
        {
            { 1, "Admin" },
            { 2, "Manager" },
            { 3, "Employee" },
        };
    }
}