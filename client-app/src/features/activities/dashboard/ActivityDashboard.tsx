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
  editMode: boolean;
  openForm: (id: string) => void;
  closeForm: () => void;
  createOrEdit: (activity : Activity) => void;
  deleteActivity: (id: string) => void;
}

export default function ActivityDashboard({
  activities,
  selectedActivity,
  editMode,
  selectActivity,
  cancelSelectActivity,
  openForm,
  closeForm,
  createOrEdit,
  deleteActivity
}: Props) {
  return (
    <Grid>
      <GridColumn width="10">
        <ActivityList
          activities={activities}
          selectActivity={selectActivity}
          deleteActivity={deleteActivity}
        ></ActivityList>
      </GridColumn>
      <GridColumn width={6}>
        {selectedActivity && !editMode && (
          <ActicityDetails
            activity={selectedActivity}
            cancelSelectActivity={cancelSelectActivity}
            openForm={openForm}
          />
        )}
        {editMode && (
          <ActivityForm closeForm={closeForm} activity={selectedActivity} createOrEdit={createOrEdit}/>
        )}
      </GridColumn>
    </Grid>
  );
}
