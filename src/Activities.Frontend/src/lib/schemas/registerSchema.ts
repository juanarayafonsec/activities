import z from "zod";
import { requiredString } from "../util/util";


export const registerSchema = z.object({
  displayName: requiredString("Display Name"),
  email: z.email("Invalid email address"),
  password: requiredString("Password")
});

export type RegisterSchema = z.infer<typeof registerSchema>;