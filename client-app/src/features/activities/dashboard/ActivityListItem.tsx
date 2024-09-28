import { Link } from "react-router-dom";
import {
  Button,
  Icon,
  Item,
  ItemContent,
  ItemDescription,
  ItemGroup,
  ItemHeader,
  ItemImage,
  Segment,
  SegmentGroup,
} from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { useStore } from "../../../app/stores/store";
import { SyntheticEvent, useState } from "react";
import { format } from "date-fns";

interface Props {
  activity: Activity;
}
export default function ActivityLisyItem({ activity }: Props) {
  const { activityStore } = useStore();
  const { deleteActivity, loading } = activityStore;

  const [target, setTarget] = useState("");

  function handleActivityDelete(
    e: SyntheticEvent<HTMLButtonElement>,
    id: string
  ) {
    setTarget(e.currentTarget.name);
    deleteActivity(id);
  }
  return (
    <SegmentGroup>
      <Segment>
        <ItemGroup>
          <Item>
            <ItemImage size="tiny" circular src="assets/user.png" />
            <ItemContent>
              <ItemHeader as={Link} to={`/activities/${activity.id}`}>
                {activity.title}
              </ItemHeader>
              <ItemDescription>Hpsted by Bob</ItemDescription>
            </ItemContent>
          </Item>
        </ItemGroup>
      </Segment>
      <Segment>
        <span>
          <Icon name="clock" /> {format(activity.date!, 'dd MMM yyyy h:mm aa')}
          <Icon name="marker" /> {activity.venue}
        </span>
      </Segment>
      <Segment secondary>Attendess go here</Segment>
      <Segment clearing>
        <span> {activity.description}</span>
        <Button
          as={Link}
          to={`/activities/${activity.id}`}
          color="teal"
          floated="right"
          content="View"
        />
      </Segment>
    </SegmentGroup>
  );
}
