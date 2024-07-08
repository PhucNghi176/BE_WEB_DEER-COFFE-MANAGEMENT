using DeerCoffeeShop.Domain.Common.Exceptions;
using FluentValidation;
using System.Dynamic;

namespace DeerCoffeeShop.Application.Employees.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            _ = RuleFor(x => x).Custom((command, context) =>
            {
                bool isError = false;
                dynamic errorData = new ExpandoObject();
                IDictionary<string, object> errorDictionary = (IDictionary<string, object>)errorData;
                if (string.IsNullOrEmpty(command.Email) || !IsValidEmail(command.Email))
                {
                    errorDictionary["Email"] = "Email must be a valid email address.";
                    isError = true;
                }

                if (string.IsNullOrEmpty(command.FullName))
                {
                    errorDictionary["FullName"] = "FullName must be a valid email address.";
                    isError = true;
                }

                if (string.IsNullOrEmpty(command.DateOfBirth))
                {
                    errorDictionary["DateOfBirth"] = "DateOfBirth must be a valid email address.";
                    isError = true;
                }

                if (string.IsNullOrEmpty(command.PhoneNumber))
                {
                    errorDictionary["PhoneNumber"] = "PhoneNumber must be a valid email address.";
                    isError = true;
                }

                if (string.IsNullOrEmpty(command.Address))
                {
                    errorDictionary["Address"] = "Address must not be empty.";
                    isError = true;
                }
                if (isError)
                {
                    System.Console.WriteLine(errorData);
                    throw new FormException("Error in creating employee", errorDictionary);
                }
            });
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                System.Net.Mail.MailAddress addr = new(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
};


