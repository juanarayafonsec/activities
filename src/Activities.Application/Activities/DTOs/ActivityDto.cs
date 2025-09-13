using Activities.Application.Profiles.DTOs;

namespace Activities.Application.Activities.DTOs;
public record ActivityDto(Guid id, string title, DateTime date, string description, 
    string category, bool isCancelled, string hostDisplayName, string hostId, string city,
    string venue, double latitude, double longitude, ICollection<UserProfileDto> attendees);