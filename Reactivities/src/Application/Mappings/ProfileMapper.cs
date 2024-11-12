using Application.Profiles;

namespace Application.Mappings;

public static class ProfileMapper
{
    public static Profile Map(this AppUser appUser) =>
        new()
        {
            Username = appUser.UserName, DisplayName = appUser.DisplayName, Bio = appUser.Bio,
            Image = appUser.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            Photos = appUser.Photos?.Where(p => !p.IsMain).ToList()
        };
}