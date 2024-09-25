import { MenuItem, Button, Menu, Container } from "semantic-ui-react";
import { NavLink } from "react-router-dom";

export default function NavBar() {
  return (
    <Menu inverted fixed="top">
      <Container>
        <MenuItem as={NavLink} to="/" header>
          <img
            src="assets/logo.png"
            alt="logo"
            style={{ marginRight: "10px" }}
          />
          Reactive
        </MenuItem>
        <MenuItem name="Activities" as={NavLink} to="/activities"></MenuItem>
        <MenuItem name="Errors" as={NavLink} to="/errors"></MenuItem>
        <MenuItem>
          <Button as={NavLink} to="createActivity" positive>
            CreateActivity
          </Button>
        </MenuItem>
      </Container>
    </Menu>
  );
}
