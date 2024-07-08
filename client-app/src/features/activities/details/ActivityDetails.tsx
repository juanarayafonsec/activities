import {
    Button,
    ButtonGroup,
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardMeta,
  Image,
} from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";

interface Props {
  activity: Activity;
}
export default function ActicityDetails({ activity }: Props) {
  return (
    <Card>
      <Image src={`/assets/categoryImages/${activity.category}.jpg`} />
      <CardContent>
        <CardHeader>{activity.title}</CardHeader>
        <CardMeta>
          <span >{activity.date}</span>
        </CardMeta>
        <CardDescription>
          {activity.description}
        </CardDescription>
      </CardContent>
      <CardContent extra>
        <ButtonGroup widths={2}>
            <Button basic color="blue" content="Edit"/>
            <Button basic color="grey" content="Cancel"/>
        </ButtonGroup>
      </CardContent>
    </Card>
  );
}
