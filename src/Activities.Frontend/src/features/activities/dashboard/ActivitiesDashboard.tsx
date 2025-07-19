import { Grid2 } from "@mui/material";
import ActivityList from "./ActivityList";
import ActivityDetails from "../details/ActivityDetails";

type Props = {
  activities: Activity[];
  selectedActivity?: Activity;
  selectActivity: (id: string) => void;
  cancelSelectActivity: () => void;
};
export default function ActivitiesDashboard({
  activities,
  cancelSelectActivity,
  selectActivity,
  selectedActivity,
}: Props) {
  return (
    <>
      <Grid2 container spacing={3}>
        <Grid2 size={7}>
          <ActivityList activities={activities} selectActivity={selectActivity}/>
        </Grid2>
        <Grid2 size={5}>
          {selectedActivity && <ActivityDetails activity={selectedActivity} cancelSelectActivity={cancelSelectActivity} />}
        </Grid2>
      </Grid2>
    </>
  );
}
