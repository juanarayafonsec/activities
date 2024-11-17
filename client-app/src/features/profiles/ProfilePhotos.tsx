import { observer } from "mobx-react-lite";
import { Card, CardGroup, Header, Image, TabPane } from "semantic-ui-react";
import { Profile } from "../../app/models/profile";

interface Props {
  profile: Profile;
}
export default observer(function ProfilePhotos({ profile }: Props) {
  return (
    <TabPane>
      <Header icon="image" content="Photos" />
      <CardGroup itemsPerRow={5}>
        {profile.photos?.map((p) => (
          <Card key={p.id}>
            <Image src={p.url} />
          </Card>
        ))}
      </CardGroup>
    </TabPane>
  );
});
