import { observer } from "mobx-react-lite";
import { Image, List, ListItem, Popup, PopupContent } from "semantic-ui-react";
import { Profile } from "../../../app/models/profile";
import { Link } from "react-router-dom";
import ProfileCards from "../../profiles/ProfileCards";

interface Props {
  attendees: Profile[];
}

export default observer(function ActivityListItemAttendee({
  attendees,
}: Props) {
  return (
    <List horizontal>
      {attendees.map((attendee) => (
        <Popup
          hoverable
          key={attendee.username}
          trigger={
            <ListItem
              key={attendee.username}
              as={Link}
              to={`/profile/${attendee.username}`}
            >
              <Image
                size="mini"
                circular
                src={attendee.image || "/assets/user.png"}
              />
            </ListItem>
          }
        >
          <PopupContent>
            <ProfileCards profile={attendee} />
          </PopupContent>
        </Popup>
      ))}
    </List>
  );
});
