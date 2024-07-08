import { MenuItem, Button, Menu, Container } from "semantic-ui-react";

export default function NavBar() {
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
          <Button positive>CreateActivity</Button>
        </MenuItem>
      </Container>
    </Menu>
  );
}
