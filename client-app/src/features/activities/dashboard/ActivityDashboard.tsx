import { Grid, GridColumn } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityList from "./ActivityList";
import ActicityDetails from "../details/ActivityDetails";

interface Props {
  activities: Activity[];
}

export default function ActivityDashboard({ activities }: Props) {
  return (
    <Grid>
      <GridColumn width="10">
        <ActivityList activities={activities}></ActivityList>
      </GridColumn>
      <GridColumn width={6}>
        {activities[0] && <ActicityDetails activity={activities[0]} />}
      </GridColumn>
    </Grid>
  );
}
