using BlogPlatform.Application.Common;
using BlogPlatform.Application.DTOs.User;
using BlogPlatform.Application.Interfaces.Repo;
using BlogPlatform.Application.Query.User;
using MediatR;

namespace BlogPlatform.Application.Handler.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResult<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResult<UserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUsersAsync(request, cancellationToken);

        }
    }
}
