using ApiTemplate.Application.Commands;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace ApiTemplate.Application.Handlers
{
    public class UserAddHandler : IRequestHandler<UserAddCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserAddHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ValidationException(request.ValidationResult.Errors);
            }

            var id = Guid.NewGuid();

            _unitOfWork.BeginTransaction();
            try
            {
                await _userRepository.Add(new UserEntity()
                {
                    Id = id.ToString(),
                    Name = request.Name,
                    Email = request.Email,
                });

                _unitOfWork.Commit();
            }
            catch 
            {
                _unitOfWork.Rollback();
                throw;
            }
       
            return id;
        }
    }
}
