using ApiTemplate.Application.Commands;
using ApiTemplate.Domain.Interfaces;
using MediatR;

namespace ApiTemplate.Application.Handlers
{
    public class UserListHandler : IRequestHandler<UserListCommand, IList<UserListCommandRespose>>
    {

        private readonly IUserRepository _userRepository;

        public UserListHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IList<UserListCommandRespose>> Handle(UserListCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.List();
            var response = new List<UserListCommandRespose>();

            users.ToList().ForEach(user =>
            {
                response.Add(new UserListCommandRespose()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                });
            });

            return response;
        }
    }
}
