using ApiTemplate.Application.Commands;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace ApiTemplate.Application.Handlers
{
    public class UserAddHandler : IRequestHandler<UserAddCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserAddHandler(
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ValidationException(request.ValidationResult.Errors);
            }
            
            var userEntity = _mapper.Map<UserEntity>(request);
            userEntity.Id = Guid.NewGuid().ToString();

            _unitOfWork.BeginTransaction();

            try
            {
                await _userRepository.Add(userEntity);

                _unitOfWork.Commit();
            }
            catch 
            {
                _unitOfWork.Rollback();
                throw;
            }
       
            return userEntity.Id;
        }
    }
}
