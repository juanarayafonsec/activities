import NavBar from "./NavBar";
import "semantic-ui-css/semantic.min.css";
import "./styles.css";
import { Container } from "semantic-ui-react";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { useEffect, useState } from "react";
import { Activity } from "../models/activity";
import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectActivity, setSelectedActivity] = useState<Activity | undefined>(
    undefined
  );
  const [editMode, setEditMode] = useState(false);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    const fetchActivities = async () => {
      const response = await agent.Activities.list();
      let activities: Activity[] = [];
      response.forEach((activity) => {
        activity.date = activity.date.split("T")[0];
        activities.push(activity);
      });
      setActivities(response);
      setLoading(false);
    };
    fetchActivities().catch();
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

  async function handleCreateOrEditActivity(activity: Activity) {
    setSubmitting(true);
    if (activity.id) {
      await agent.Activities.update(activity);
      setActivities([
        ...activities.filter((x) => x.id !== activity.id),
        activity,
      ]);
    } else {
      const response = await agent.Activities.create(activity);
      activity.id = response.id;
      setActivities([...activities, activity]);
    }
    setEditMode(false);
    setSelectedActivity(activity);
    setSubmitting(false);
  }

  async function handleDelete(id: string) {
    setSubmitting(true);
    await agent.Activities.delete(id);
    setSubmitting(false);
    setActivities([...activities.filter((x) => x.id !== id)]);
  }
  if (loading)
    return <LoadingComponent inverted={loading} content="Loading app" />;
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
          submitting={submitting}
        />
      </Container>
    </>
  );
}

export default App;
