using MediatR;

namespace ApiTemplate.Application.Commands
{
    public class UserListCommand : IRequest<IList<UserListCommandRespose>>
    {
    }
}
