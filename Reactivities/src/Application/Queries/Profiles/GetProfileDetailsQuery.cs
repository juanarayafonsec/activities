using Application.Profiles;

namespace Application.Queries.Profiles;

public class GetProfileDetailsQuery : IRequest<Result<Profile>>
{
    public string Username { get; set; }
}