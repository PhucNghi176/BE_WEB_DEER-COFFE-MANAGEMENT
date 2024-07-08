using DeerCoffeeShop.Domain.Entities.Base;
using MediatR;
using System.Security.Cryptography;

namespace DeerCoffeeShop.Application.Authentication.Refrestoken.GenerateRefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshToken>
    {
        public Task<RefreshToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            byte[] randome = new Byte[64];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randome);
            }
            string token = Convert.ToBase64String(randome);
            RefreshToken refreshToken = new()
            {
                Token = token,
                Expired = DateTime.Now.AddDays(7)
            };
            return Task.FromResult(refreshToken);
        }
    }
}
