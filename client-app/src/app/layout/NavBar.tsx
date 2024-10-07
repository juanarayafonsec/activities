import {
  MenuItem,
  Button,
  Menu,
  Container,
  Image,
  Dropdown,
  DropdownItem,
  DropdownMenu,
} from "semantic-ui-react";
import { Link, NavLink } from "react-router-dom";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

export default observer(function NavBar() {
  const {
    userStore: { user, logout },
  } = useStore();
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
        <MenuItem>
          <Image
            src={user?.image || "/assets/user.png"}
            avatar
            spaced="right"
          />
          <Dropdown pointing="top left" text={user?.displayName}>
            <DropdownMenu>
              <DropdownItem
                as={Link}
                to={`/profile/${user?.username}`}
                text="My Profile"
                icon="user"
              />
              <DropdownItem onClick={logout} text="Logout" icon="power" />
            </DropdownMenu>
          </Dropdown>
        </MenuItem>
      </Container>
    </Menu>
  );
});
