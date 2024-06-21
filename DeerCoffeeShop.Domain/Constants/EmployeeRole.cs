namespace DeerCoffeeShop.Domain.Constants
{
    public static class EmployeeRole
    {
        public static readonly Dictionary<int, string> EmployeeRoleDictionary = new Dictionary<int, string>
        {
            { 1, "Admin" },
            { 2, "Manager" },
            { 3, "Employee" },
        };
    }
}