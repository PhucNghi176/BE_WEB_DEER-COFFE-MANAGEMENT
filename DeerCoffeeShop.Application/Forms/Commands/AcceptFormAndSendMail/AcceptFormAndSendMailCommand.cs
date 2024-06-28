using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Application.Utils;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;

namespace DeerCoffeeShop.Application.Forms.Commands.AcceptFormAndSendMail;

public record AcceptFormAndSendMailCommand(string FormID, string RestaurantID, DateTime Date) : IRequest<string>, ICommand
{
    public string FormID { get; set; } = FormID;
    public string RestaurantID { get; set; } = RestaurantID;
    public DateTime Date { get; set; } = Date;

}
internal class AcceptFormAndSendMailCommandHandler : IRequestHandler<AcceptFormAndSendMailCommand, string>
{
    private readonly IFormRepository _formRepository;
    private readonly IRestaurantRepository _restaurantRepository;

    public AcceptFormAndSendMailCommandHandler(IFormRepository formRepository, IRestaurantRepository restaurantRepository)
    {
        _formRepository = formRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<string> Handle(AcceptFormAndSendMailCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Form? from = await _formRepository.FindAsync(x => x.ID == request.FormID, cancellationToken);
        Domain.Entities.Restaurant? restaurant = await _restaurantRepository.FindAsync(x => x.ID == request.RestaurantID, cancellationToken);
        if (from == null)
        {
            return "Form not found";
        }
        if (restaurant == null)
        {
            return "Invalid Restaurant";
        }
        from.IsApproved = true;
        from.FormType = Domain.Enums.FormTypeEnum.ACCEPPTED;
        from.Date = DateTime.Now;
        await MailUtils.SendEmailAsync(from.Employee.FullName, from.Employee.Email, "Thư mời phỏng vấn", request.Date, restaurant.RestaurantAddress);
        _formRepository.Update(from);
        _ = await _formRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        //send mail

        return "Check Your Email!";
    }
}
