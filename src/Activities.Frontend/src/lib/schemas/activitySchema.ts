import { z } from "zod";

const requiredString = (fieldName: string) => z
    .string({error: `${fieldName} is required`})
    .min(1, { message: `${fieldName} is required`});

export const activitySchema = z.object({
    title: requiredString("Title"),
    description: requiredString("Description"),
    category: requiredString("Category"),
    date: z.date({ error: "Date is required"}),
    location: z.object({
        city: requiredString("City"),
        venue: z.string().optional(),
        latitude: z.number(),
        longitude: z.number()
    })
})

export type ActivitySchema = z.infer<typeof activitySchema>;