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
  
  useEffect(() => {
    axios.get<Activity[]>("https://localhost:5000/api/v1/Activities").then(response => {
      setActivities(response.data)
    });
  });

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard activities={activities}/>
      </Container>
    </>
  );
}

export default App;
