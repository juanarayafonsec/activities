import NavBar from "./NavBar";
import "semantic-ui-css/semantic.min.css";
import "./styles.css";
import { Container } from "semantic-ui-react";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { useEffect, useState } from "react";
import { Activity } from "../models/activity";
import axios from "axios";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectActivity, setSelectedActivity] = useState<Activity | undefined>(
    undefined
  );
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    axios
      .get<Activity[]>("https://localhost:5000/api/v1/Activities")
      .then((response) => {
        setActivities(response.data);
      });
  }, []);

  function handleSelectActivity(id: string) {
    setSelectedActivity(activities.find((x) => x.id === id));
  }

  function handleCancelSelectActivity() {
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string) {
    id ? handleSelectActivity(id) : handleCancelSelectActivity();
    setEditMode(true);
  }

  function handleFormClose() {
    setEditMode(false);
  }

  function handleCreateOrEditActivity(activity: Activity) {
    setActivities([...activities, activity]);
    activity.id
      ? setActivities([
          ...activities.filter((x) => x.id !== activity.id),
          activity,
        ])
      : setActivities([...activities, activity]);
    setEditMode(false);
    setSelectedActivity(activity);
  }

  function handleDelete(id: string) {
    setActivities([...activities.filter((x) => x.id !== id)]);
  }

  return (
    <>
      <NavBar openForm={handleFormOpen} />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard
          activities={activities}
          selectedActivity={selectActivity}
          selectActivity={handleSelectActivity}
          cancelSelectActivity={handleCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={handleCreateOrEditActivity}
          deleteActivity={handleDelete}
        />
      </Container>
    </>
  );
}

export default App;
