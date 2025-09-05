import { z } from "zod";
import { requiredString } from "../util/util";

export const activitySchema = z.object({
    title: requiredString("Title"),
    description: requiredString("Description"),
    category: requiredString("Category"),
    date: z.date({ error: "Date is required"}),
    location: z.object({
        city: requiredString("City"),
        venue: z.string().optional(),
        latitude: z.number({error:"Longitude is required"}).min(-90).max(90),
        longitude: z.number({error: "Longitude is required"}).min(-180).max(180),
    })
})

export type ActivitySchema = z.infer<typeof activitySchema>;