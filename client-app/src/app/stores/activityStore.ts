import { makeAutoObservable, runInAction } from "mobx";
import { Activity } from "../models/activity";
import agent from "../api/agent";

export default class ActivityStore {
  activityRegister = new Map<string, Activity>();
  selectedActivity: Activity | undefined = undefined;
  editMode = false;
  loading = false;
  loadInitial = false;

  constructor() {
    makeAutoObservable(this);
  }

  get activitiesByDate() {
    return Array.from(this.activityRegister.values()).sort(
      (a, b) => Date.parse(a.date) - Date.parse(b.date)
    );
  }

  loadActivities = async () => {
    try {
      this.setLoadingInitial(true);
      const response = await agent.Activities.list();
      runInAction(() => {
        response.forEach((activity) => {
          this.setActivity(activity);
        });
        this.setLoadingInitial(false);
      });
    } catch (errot) {
      this.setLoadingInitial(false);
    }
  };

  loadActivity = async (id: string) => {
    let activity = this.getActivity(id);
    if (activity) {
      this.selectedActivity = activity;
      return activity;
    } else {
      this.setLoadingInitial(true);
      try {
        activity = await agent.Activities.details(id);
        this.setActivity(activity);
        runInAction(() => (this.selectedActivity = activity));
        this.setLoadingInitial(false);
        return activity;
      } catch (error) {
        console.log(error);
        this.setLoadingInitial(false);
      }
    }
  };

  private setActivity = (activity: Activity) => {
    activity.date = activity.date.split("T")[0];
    this.activityRegister.set(activity.id, activity);
  };

  private getActivity = (id: string) => {
    return this.activityRegister.get(id);
  };

  setLoadingInitial = (state: boolean) => {
    this.loadInitial = state;
  };

  createActivity = async (activity: Activity) => {
    this.loading = true;
    try {
      const response = await agent.Activities.create(activity);
      activity.id = response.id;
      runInAction(() => {
        this.activityRegister.set(activity.id, activity);
        this.selectedActivity = activity;
        this.editMode = false;
        this.loading = false;
      });
    } catch (error) {
      this.loading = false;
      console.log(error);
    }
  };

  updateActivity = async (activity: Activity) => {
    this.loading = true;
    await agent.Activities.update(activity);
    try {
      runInAction(() => {
        this.activityRegister.set(activity.id, activity);
        this.selectedActivity = activity;
        this.editMode = false;
        this.loading = false;
      });
    } catch (error) {
      this.loading = false;
      console.log(error);
    }
  };

  deleteActivity = async (id: string) => {
    this.loading = true;
    try {
      await agent.Activities.delete(id);
      runInAction(() => {
        this.activityRegister.delete(id);
        this.loading = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loading = false;
        console.log(this.loading);
      });
      console.log(error);
    }
  };
}
