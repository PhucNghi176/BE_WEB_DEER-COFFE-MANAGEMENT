﻿using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System.Data;

namespace DeerCoffeeShop.Application.Restaurants.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, string>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRestaurantChainRepository _restaurantChainRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ICurrentUserService currentUserService, IRestaurantChainRepository restaurantChainRepository, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _restaurantChainRepository = restaurantChainRepository;
            _restaurantRepository = restaurantRepository;
            _currentUserService = currentUserService;
            _employeeRepository = employeeRepository;
        }
        public CreateRestaurantCommandHandler() { }
        public async Task<string> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _restaurantRepository.FindAsync(x => x.RestaurantName.Equals(request.RestaurantName) && x.RestaurantChainID.Equals(request.RestaurantChainID), cancellationToken) != null)
                    throw new DuplicateNameException("dulicate restaurant name in this restaurantChain.");
                var resChain = await _restaurantChainRepository.FindAsync(x => x.ID.Equals(request.RestaurantChainID), cancellationToken);
                if (resChain == null)
                    throw new NotFoundException("Not found restaurantChain that had been chosen.");

                var emp = await _employeeRepository.FindAsync(x => x.ID.Equals(request.ManagerID), cancellationToken);
                if (emp.RoleID == 2) //2 là manager đúng hong ta :vv
                    throw new NotFoundException("Not found manager had been chosen.");
                if (await _restaurantRepository.AnyAsync(x => x.ManagerID.Equals(request.ManagerID), cancellationToken))
                    throw new DuplicateNameException("Manager had been chosen is manager of another restaurant.");
                resChain.RestaurantChainTotalBranches += 1;
                resChain.RestaurantChainTotalEmployees += 1;
                this._restaurantChainRepository.Update(resChain);
                await this._restaurantChainRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                var restaurant = new Restaurant
                {

                    IsDeleted = false,
                    ManagerID = request.ManagerID,
                    NguoiTaoID = this._currentUserService.UserId,
                    NgayTao = DateTime.UtcNow,
                    RestaurantAddress = request.RestaurantAddress,
                    RestaurantChainID = request.RestaurantChainID,
                    RestaurantName = request.RestaurantName,
                    TotalEmployees = 1,
                };
                this._restaurantRepository.Add(restaurant);
                await this._restaurantRepository.UnitOfWork.SaveChangesAsync();

                return $"Create new restaurantName: {request.RestaurantName} of restaurantChain: {(await _restaurantChainRepository.FindAsync(x => x.ID.Equals(request.RestaurantChainID), cancellationToken)).RestaurantChainName} successful.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}