import { Tab, TabPane } from "semantic-ui-react";
import ProfilePhotos from "./ProfilePhotos";
import { Profile } from "../../app/models/profile";

interface Props {
  profile: Profile;
}

export default function ProfileContent({profile}: Props) {
  const panes = [
    { menuItem: "About", render: () => <TabPane>About Content</TabPane> },
    { menuItem: "Photos", render: () => <ProfilePhotos profile={profile}/> },
    { menuItem: "Events", render: () => <TabPane>Events Content</TabPane> },
    {
      menuItem: "Followers",
      render: () => <TabPane>Followers Content</TabPane>,
    },
    {
      menuItem: "Following",
      render: () => <TabPane>Following Content</TabPane>,
    },
  ];

  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="right"
      panes={panes}
    />
  );
}
