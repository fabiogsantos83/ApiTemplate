using ApiTemplate.Application.Queries;
using ApiTemplate.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace ApiTemplate.Application.Handlers
{
    public class UserListHandler : IRequestHandler<UserQueryRequest, IList<UserQueryResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserListHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IList<UserQueryResponse>> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.List();
            
            return _mapper.Map<List<UserQueryResponse>>(users);     
        }
    }
}
