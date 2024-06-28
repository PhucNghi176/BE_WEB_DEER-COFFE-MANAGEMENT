using DeerCoffeeShop.Application.Utils;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using System.Dynamic;

namespace DeerCoffeeShop.Application.Employees.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFormRepository _formRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IFormRepository formRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _formRepository = formRepository;
        }

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            bool isError = false;
            dynamic errorData = new ExpandoObject();
            Employee? existed = await _employeeRepository.FindAsync(x => x.Email == request.Email, cancellationToken);
            DateTime date = DateTime.Parse(request.DateOfBirth);
            if (existed?.Email != null)
            {
                errorData.Email = "Email already exist !";
                isError = true;
            }

            if (request.PhoneNumber.Length < 10 || request.PhoneNumber.Length > 12)
            {
                errorData.PhoneNumber = "Phone number must in range 10 to 12 !";
                isError = true;
            }

            if (existed?.PhoneNumber != null)
            {
                errorData.PhoneNumber = "Phone already exist !";
            };

            if (isError)
            {
                if (existed != null)
                {
                    if (existed.IsMailed)
                    {
                        throw new FormException("Error in creating employee", errorData);
                    }
                    else if (!existed.IsMailed & request.FullName.ToLower().Equals(existed.FullName.ToLower()))
                    {
                        await MailUtils.SendEmailAsync(request.FullName, request.Email, request.Address, request.PhoneNumber, $"{date.Day}/{date.Month}/{date.Year}", "Đơn Xác Nhận Đăng Ký");
                        return "Check Your Email Again!";
                    }
                }

            }

            Employee emp = new()
            {
                Address = request.Address,
                DateOfBirth = DateTime.Parse(request.DateOfBirth),
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleID = 3,
                IsActive = false,
                IsMailed = true,
            };
            _employeeRepository.Add(emp);

            _ = await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            Employee? enity = await _employeeRepository.FindAsync(x => x.Email == request.Email, cancellationToken);



            await MailUtils.SendEmailAsync(request.FullName, request.Email, request.Address, request.PhoneNumber, $"{date.Day}/{date.Month}/{date.Year}", "Đơn Xác Nhận Đăng Ký");

            Form form = new()
            {
                EmployeeID = enity?.ID,
                FormType = Domain.Enums.FormTypeEnum.JOB_APPLICATION,
                Date = DateTime.Now,
                IsApproved = false
            };

            _formRepository.Add(form);

            return await _formRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Employee Successfully !" : "Create Employee Fail";
        }
    }
};


