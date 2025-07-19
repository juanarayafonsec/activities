import { Box, Container, CssBaseline } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import NavBar from "./NavBar";
import ActivitiesDashboard from "../../features/activities/dashboard/ActivitiesDashboard";


function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    axios
      .get<Activity[]>("https://localhost:5001/api/activities")
      .then((response) => setActivities(response.data));
  }, []);

  return (
    <Box sx={{ backgroundColor: "#eeeeee"}}>
      <CssBaseline />
      <NavBar />
      <Container maxWidth="xl" sx={{ marginTop: 3 }}>
        <ActivitiesDashboard activities={activities} />
      </Container>
    </Box>
  );
}

export default App;
