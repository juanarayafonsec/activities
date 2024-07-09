import { Grid, GridColumn } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityList from "./ActivityList";
import ActicityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";

interface Props {
  activities: Activity[];
  selectedActivity: Activity | undefined;
  selectActivity: (id: string) => void;
  cancelSelectActivity: () => void;
}

export default function ActivityDashboard({
  activities,
  selectedActivity,
  selectActivity,
  cancelSelectActivity,
}: Props) {
  return (
    <Grid>
      <GridColumn width="10">
        <ActivityList
          activities={activities}
          selectActivity={selectActivity}
        ></ActivityList>
      </GridColumn>
      <GridColumn width={6}>
        {selectedActivity && (
          <ActicityDetails
            activity={selectedActivity}
            cancelSelectActivity={cancelSelectActivity}
          />
        )}

        <ActivityForm />
      </GridColumn>
    </Grid>
  );
}
