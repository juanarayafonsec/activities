import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { useLocation } from "react-router";
import type { Activity, ActivityBase } from "../types";
import { useAccount } from "./useAccounts";

export const useActivities = (id?: string) => {
    const queryClient = useQueryClient();
    const {currentUser} = useAccount();
    const location = useLocation();

    const { data: activities, isLoading } = useQuery({
    queryKey: ["activities"],
    queryFn: async () => {
      const response = await agent.get<Activity[]>("activities");
      return response.data;
    },
    enabled: !id && location.pathname === "/activities" && !!currentUser
  });

  const {data: activity, isLoading: isLoadingActivity} = useQuery({
    queryKey: ["activities", id],
    queryFn: async () => {
      const response = await agent.get<Activity>(`activities/${id}`);
      return response.data;
    },
    enabled: !!id && !!currentUser
  });
  
  const createActivity = useMutation({
    mutationFn: async (activity: ActivityBase) => {
       const response = await agent.post<ActivityBase>("activities", activity);
       return response.data;
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ["activities"] });
    }
  })

  const updateActivity = useMutation({
    mutationFn: async (activity: Activity) => {
       await agent.put<Activity>("activities", activity);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ["activities"] });
    }
  })

  const deleteActivity = useMutation({
    mutationFn: async (id: string) => {
       await agent.delete<Activity>(`activities/${id}`);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ["activities"] });
    }
  })

  return {activities, isLoading, updateActivity, createActivity, deleteActivity, activity, isLoadingActivity};
} 