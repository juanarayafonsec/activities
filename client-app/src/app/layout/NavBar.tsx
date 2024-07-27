import { MenuItem, Button, Menu, Container } from "semantic-ui-react";
import { useStore } from "../stores/store";

export default function NavBar() {
  const { activityStore } = useStore();

  return (
    <Menu inverted fixed="top">
      <Container>
        <MenuItem>
          <img
            src="assets/logo.png"
            alt="logo"
            style={{ marginRight: "10px" }}
          />
          Reactive
        </MenuItem>
        <MenuItem name="Activities"></MenuItem>
        <MenuItem>
          <Button onClick={() => activityStore.openForm()} positive>
            CreateActivity
          </Button>
        </MenuItem>
      </Container>
    </Menu>
  );
}
