using DeerCoffeeShop.Domain.Common.Exceptions;
using FluentValidation;
using System.Dynamic;

namespace DeerCoffeeShop.Application.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x).Custom((command, context) =>
            {
                bool isError = false;
                dynamic errorData = new ExpandoObject();
                var errorDictionary = (IDictionary<string, object>)errorData;
                if (string.IsNullOrEmpty(command.Email))
                {
                    errorDictionary["Email"] = "Email can not be Empty.";
                    isError = true;
                }

                if (!IsValidEmail(command.Email))
                {
                    errorDictionary["Email"] = "Email must be a valid email address.";
                    isError = true;
                }

                if (string.IsNullOrEmpty(command.FullName))
                {
                    errorDictionary["FullName"] = "FullName can not be Empty.";
                    isError = true;
                }

                if (command.DateOfBirth == null)
                {
                    errorDictionary["dateOfBirth"] = "DateOfBirth is required and must be a valid date.";
                    isError = true;
                }

                if (command.DateOfBirth == DateTime.MinValue)
                {
                    errorDictionary["dateOfBirth"] = "DateOfBirth is required and must be a valid date.";
                    isError = true;
                }
                else if (command.DateOfBirth > DateTime.Now)
                {
                    errorDictionary["dateOfBirth"] = "Can not be in the future !";
                    isError = true;
                }
                else if (command.DateOfBirth < DateTime.Now.AddYears(-120))
                {
                    errorDictionary["dateOfBirth"] = "DateOfBirth is not realistic.";
                    isError = true;
                }

                if (string.IsNullOrEmpty(command.PhoneNumber))
                {
                    errorDictionary["PhoneNumber"] = "PhoneNumber can not be empty !.";
                    isError = true;
                }

                if (!(command.PhoneNumber.Length > 9 && command.PhoneNumber.Length < 13))
                {
                    errorDictionary["PhoneNumber"] = "PhoneNumber must in range 10 to 12.";
                    isError = true;
                }

                if (string.IsNullOrEmpty(command.Address))
                {
                    errorDictionary["Address"] = "Address must not be empty.";
                    isError = true;
                }

                if (command.RoleId == null)
                {
                    errorDictionary["RoleId"] = "Role Id is required.";
                    isError = true;
                }

                if (!(command.RoleId > 0 && command.RoleId < 4))
                {
                    errorDictionary["RoleId"] = "Role Id must in range";
                    isError = true;
                }

                if (command.IsActive == null)
                {
                    errorDictionary["IsActive"] = "IsActive must not be empty.";
                    isError = true;
                }

                if (isError)
                {
                    throw new FormException("Error in updating employee", errorDictionary);
                }
            });
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
};


