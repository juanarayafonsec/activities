import {
  Button,
  Form,
  FormInput,
  FormTextArea,
  Segment,
} from "semantic-ui-react";

export default function ActivityForm() {
  return (
    <Segment>
      <Form clearing>
        <FormInput placeholder="Title" />
        <FormTextArea placeholder="Description" />
        <FormInput placeholder="Category" />
        <FormInput placeholder="Date" />
        <FormInput placeholder="City" />
        <FormInput placeholder="Venue" />
        <Button floated="right" positive type="submit" content="Submit" />
        <Button type="button" content="Cancel" />
      </Form>
    </Segment>
  );
}
