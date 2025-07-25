import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";

type Props = {
    selectedActivity: Activity;
    cancelSelectActivity: () => void;
    openForm: (id: string) => void;
}

export default function ActivityDetails({ selectedActivity, cancelSelectActivity, openForm }: Props) {
     const {activities} = useActivities();
    const activity = activities?.find(a => a.id === selectedActivity.id);
    if (!activity) return <Typography>...</Typography>;
    return (
        <Card sx={{borderRadius: 3}}>
            <CardMedia component="img"  image={`/images/categoryImages/${activity.category}.jpg`} alt={activity.title}/>
            <CardContent>
                <Typography variant="h5">{activity.title}</Typography>
                <Typography variant="subtitle1" fontWeight="light">{activity.date}</Typography>
                <Typography variant="body1">{activity.description}</Typography>
            </CardContent>
            <CardActions>
             <Button color="primary" onClick={() => openForm(activity.id)}>Edit</Button>
             <Button color="inherit" onClick={cancelSelectActivity}>Cancel</Button>
            </CardActions>
        </Card>
    );
}