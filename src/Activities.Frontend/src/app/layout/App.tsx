import { Box, Container, CssBaseline } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import NavBar from "./NavBar";
import ActivitiesDashboard from "../../features/activities/dashboard/ActivitiesDashboard";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectActivity] = useState<Activity | undefined>(undefined );
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    axios
      .get<Activity[]>("https://localhost:5001/api/activities")
      .then((response) => setActivities(response.data));
  }, []);

  const handleSelectActivity = (id: string) => {
    setSelectActivity(activities.find((activity) => activity.id === id));
  };

  const handleCancelSelectActivity = () => {
    setSelectActivity(undefined);
  };

  const handleOpenForm = (id?: string) => {
    if (id) {
      handleSelectActivity(id)
    } else {
      handleCancelSelectActivity()
    }

    setEditMode(true);
  };

  const handleCloseForm = () => {
    setEditMode(false);
  };

  const handleSubmit = (activity: Activity) => {
    if(activity.id) {
      setActivities(activities.map(a => a.id === activity.id ? activity : a));
    } else {
      const newActivity = {...activity, id: activities.length.toString()}
      setSelectActivity(newActivity);
       setActivities([...activities, newActivity])
    } 
    setEditMode(false);
}

const handleDelete = (id: string) => {
  setActivities(activities.filter(activity => activity.id !== id));
  if (selectedActivity?.id === id) {
    setSelectActivity(undefined);
  }
}


  return (
    <Box sx={{ backgroundColor: "#eeeeee" }}>
      <CssBaseline />
      <NavBar openForm={handleOpenForm}/>
      <Container maxWidth="xl" sx={{ marginTop: 3 }}>
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
      </Container>
    </Box>
  );
}

export default App;
