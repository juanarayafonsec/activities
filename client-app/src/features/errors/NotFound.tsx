import { Link } from "react-router-dom";
import {
  Button,
  Header,
  Icon,
  Segment,
  SegmentInline,
} from "semantic-ui-react";

export default function NotFound() {
  return (
    <Segment placeholder>
      <Header icon>
        <Icon name="search">
          Oops - we've looked everywhere but could not find what you are looking
          for!
        </Icon>
      </Header>
      <SegmentInline>
        <Button as={Link} to="/activities">
          Return to Activities page
        </Button>
      </SegmentInline>
    </Segment>
  );
}