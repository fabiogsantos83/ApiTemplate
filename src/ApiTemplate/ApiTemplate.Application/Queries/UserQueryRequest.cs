using MediatR;

namespace ApiTemplate.Application.Queries
{
    public class UserQueryRequest : IRequest<IList<UserQueryResponse>>
    {
    }
}
