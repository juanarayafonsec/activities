import { Box, Button, Paper, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";
import { useNavigate, useParams } from "react-router";
import { useForm } from "react-hook-form";
import { useEffect } from "react";
import {
  activitySchema,
  type ActivitySchema,
} from "../../../lib/schemas/activitySchema";
import { zodResolver } from "@hookform/resolvers/zod";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { categoryOptions } from "./categoryOptions";
import DateTimeInput from "../../../app/shared/components/DateTimeInput";

export default function ActivityForm() {
  const {register, reset, control, handleSubmit} = useForm<ActivitySchema>({
    mode: "onTouched",
    resolver: zodResolver(activitySchema),
  });
  const { id } = useParams();
  const { updateActivity, createActivity, activity, isLoadingActivity } =
    useActivities(id);

  useEffect(() => {
    if (activity) {
      reset(activity);
    }
  }, [activity, reset]);

  const navigate = useNavigate();

  const onSubmit = (data: ActivitySchema) => {
    console.log(data);
  };

  if (isLoadingActivity) {
    return <Typography>Loading...</Typography>;
  }

  return (
    <>
      <Paper sx={{ borderRadius: 3, padding: 3 }}>
        <Typography variant="h5" gutterBottom color="primary">
          {activity ? "Edit Activity" : "Create Activity"}
        </Typography>
        <Box
          component="form"
          display="flex"
          flexDirection="column"
          gap={3}
          onSubmit={handleSubmit(onSubmit)}
        >
          <TextInput label="Title" name="title" control={control}/>
          <TextInput label="Description" name="description" control={control} multiline rows={3}/>
          <SelectInput items={categoryOptions} label="Category" name="category" control={control}/>
          <DateTimeInput label="Date" name="date" control={control}/>
          <TextInput label="City" name="city" control={control}/>
          <TextInput label="Venue" name="venue" control={control}/>
          <TextInput label="City" name="city" control={control}/>    
          <Box display="flex" justifyContent="end" gap={3}>
            <Button color="inherit"> Cancel </Button>
            <Button
              color="success"
              variant="contained"
              type="submit"
              disabled={updateActivity.isPending || createActivity.isPending}
            >
              Submit
            </Button>
          </Box>
        </Box>
      </Paper>
    </>
  );
}
