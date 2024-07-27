import { makeAutoObservable, runInAction } from "mobx";
import { Activity } from "../models/activity";
import agent from "../api/agent";

export default class ActivityStore {
  activityRegister = new Map<string, Activity>();
  selectedActivity: Activity | undefined = undefined;
  editMode = false;
  loading = false;
  loadInitial = true;

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
      const response = await agent.Activities.list();
      runInAction(() => {
        response.forEach((activity) => {
          activity.date = activity.date.split("T")[0];
          this.activityRegister.set(activity.id, activity);
        });
        this.setLoadingInitial(false);
      });
    } catch (errot) {
      this.setLoadingInitial(false);
    }
  };

  setLoadingInitial = (state: boolean) => {
    this.loadInitial = state;
  };

  selectActivity = (id: string) => {
    this.selectedActivity = this.activityRegister.get(id);
  };

  cancelSelectedActivity = () => {
    this.selectedActivity = undefined;
  };

  openForm = (id?: string) => {
    id ? this.selectActivity(id) : this.cancelSelectedActivity();
    this.editMode = true;
  };

  closeForm = () => {
    this.editMode = false;
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
        if (this.selectedActivity?.id === id) this.cancelSelectedActivity();
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
