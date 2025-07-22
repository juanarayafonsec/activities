import { Box, Container, CssBaseline, Typography } from "@mui/material";
import { useState } from "react";
import NavBar from "./NavBar";
import ActivitiesDashboard from "../../features/activities/dashboard/ActivitiesDashboard";
import { useActivities } from "../../lib/hooks/useActivities";

function App() {
  const [selectedActivity, setSelectActivity] = useState<Activity | undefined>(
    undefined
  );
  const [editMode, setEditMode] = useState(false);
  const { activities, isPending } = useActivities();

  const handleSelectActivity = (id: string) => {
    setSelectActivity(activities!.find((activity) => activity.id === id));
  };

  const handleCancelSelectActivity = () => {
    setSelectActivity(undefined);
  };

  const handleOpenForm = (id?: string) => {
    if (id) {
      handleSelectActivity(id);
    } else {
      handleCancelSelectActivity();
    }

    setEditMode(true);
  };

  const handleCloseForm = () => {
    setEditMode(false);
  };

  const handleSubmit = (activity: Activity) => {
    // if(activity.id) {
    //   setActivities(activities.map(a => a.id === activity.id ? activity : a));
    // } else {
    //   const newActivity = {...activity, id: activities.length.toString()}
    //   setSelectActivity(newActivity);
    //    setActivities([...activities, newActivity])
    // }
    console.log(activity);
    setEditMode(false);
  };

  const handleDelete = (id: string) => {
    console.log(id);
  };

  return (
    <Box sx={{ backgroundColor: "#eeeeee", minHeight: "100vh" }}>
      <CssBaseline />
      <NavBar openForm={handleOpenForm} />
      <Container maxWidth="xl" sx={{ marginTop: 3 }}>
        {!activities || isPending ? (
          <Typography>Loading...</Typography>
        ) : (
          <ActivitiesDashboard
            activities={activities}
            selectActivity={handleSelectActivity}
            cancelSelectActivity={handleCancelSelectActivity}
            selectedActivity={selectedActivity}
            editMode={editMode}
            openForm={handleOpenForm}
            closeForm={handleCloseForm}
            submitForm={handleSubmit}
            deleteActivity={handleDelete}
          />
        )}
      </Container>
    </Box>
  );
}

export default App;
