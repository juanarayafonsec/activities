import { MenuItem, Button, Menu, Container } from "semantic-ui-react";

interface Props {
  openForm: () => void;
}

export default function NavBar({ openForm }: Props) {
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
          <Button onClick={openForm} positive>CreateActivity</Button>
        </MenuItem>
      </Container>
    </Menu>
  );
}
