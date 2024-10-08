import { Grid, GridColumn } from "semantic-ui-react";
import ActivityList from "./ActivityList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import ActivityFilters from "./ActivityFilters";

export default observer(function ActivityDashboard() {
  const { activityStore } = useStore();
  const { loadActivities, activityRegister } = activityStore;

  useEffect(() => {
    if (activityRegister.size <= 0) loadActivities();
  }, [loadActivities, activityRegister]);

  if (activityStore.loadInitial)
    return <LoadingComponent content="Loading activities..." />;

  return (
    <Grid>
      <GridColumn width="10">
        <ActivityList />
      </GridColumn>
      <GridColumn width={6}>
        <ActivityFilters />
      </GridColumn>
    </Grid>
  );
});
