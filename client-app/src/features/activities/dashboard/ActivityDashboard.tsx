import { Grid, GridColumn } from "semantic-ui-react";
import ActivityList from "./ActivityList";
import ActicityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import LoadingComponent from "../../../app/layout/LoadingComponent";

export default observer(function ActivityDashboard() {
  const { activityStore } = useStore();
  const { selectedActivity, editMode } = activityStore;

  useEffect(() => {
    activityStore.loadActivities();
  }, [activityStore]);

  if (activityStore.loadInitial)
    return <LoadingComponent content="Loading app" />;

  return (
    <Grid>
      <GridColumn width="10">
        <ActivityList />
      </GridColumn>
      <GridColumn width={6}>
        {selectedActivity && !editMode && <ActicityDetails />}
        {editMode && <ActivityForm />}
      </GridColumn>
    </Grid>
  );
});
