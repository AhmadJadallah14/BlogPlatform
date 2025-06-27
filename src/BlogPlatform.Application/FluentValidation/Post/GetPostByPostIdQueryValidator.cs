using BlogPlatform.Application.Query.Post;
using FluentValidation;

namespace BlogPlatform.Application.FluentValidation.Post
{
    public class GetPostByPostIdQueryValidator : AbstractValidator<GetPostByPostIdQuery>
    {
        public GetPostByPostIdQueryValidator()
        {
            RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("PostId must be greater than 0.");
        }
    }
}
