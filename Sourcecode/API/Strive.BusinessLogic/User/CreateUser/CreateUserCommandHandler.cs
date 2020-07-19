using Strive.Domain.User;
using Strive.Library.Configuration.Commands;
using Strive.Library.SeedWork;
using System.Threading;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.User.CreateUser
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDTO>
    {
        //private readonly ICustomerRepository _customerRepository;
        private readonly IUserUniquenessChecker _userUniquenessChecker;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserUniquenessChecker userUniquenessChecker, IUnitOfWork unitOfWork)
        {
            _userUniquenessChecker = userUniquenessChecker;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var customer = Strive.Domain.User.User.CreateRegistered(request.Email, request.Name, this._userUniquenessChecker);

            //await this._customerRepository.AddAsync(customer);

            await this._unitOfWork.CommitAsync(cancellationToken);

            return new UserDTO { UserGuid = customer.Id.Value };
        }

    }
}
