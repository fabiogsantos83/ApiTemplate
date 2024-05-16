using MediatR;

namespace ApiTemplate.Application.Commands
{
    public class UserAddCommand: IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
