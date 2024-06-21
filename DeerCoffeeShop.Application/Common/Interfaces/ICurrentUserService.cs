namespace DeerCoffeeShop.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserName { get; }
        string? RestaurantID { get; }
        Task<bool> IsInRoleAsync(string role);
        Task<bool> AuthorizeAsync(string policy);
    }
}
