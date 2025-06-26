using BlogPlatform.Application.Command.Post;
using BlogPlatform.Application.Common;
using BlogPlatform.Application.Interfaces.Repo;
using MediatR;

namespace BlogPlatform.Application.Handler.Post
{
    public class PublishPostCommandHandler : IRequestHandler<PublishPostCommand, Result<bool>>
    {
        private readonly IPostRepository _postRepository;

        public PublishPostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Result<bool>> Handle(PublishPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);
                if (post == null || post.IsDeleted)
                    return Result<bool>.Failure("Post not found");

                post.SetPublishStatus(request.IsPublished);
                await _postRepository.UpdateAsync(post, cancellationToken);

                return Result<bool>.Success(true,"Post Published Sucssesfuly");
            }
            catch(Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
