import axios, { AxiosResponse } from "axios";
import { Activity } from "../models/activity";

axios.defaults.baseURL = "https://localhost:5000/api/v1";

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: async <T>(url: string) => await axios.get<T>(url).then(responseBody),
  post: async <T>(url: string, body: {}) =>
    await axios.post<T>(url, body).then(responseBody),
  put: async <T>(url: string, body: {}) =>
    await axios.put<T>(url, body).then(responseBody),
  delete: async <T>(url: string) =>
    await axios.delete<T>(url).then(responseBody),
};

const Activities = {
  list: () => requests.get<Activity[]>("/activities"),
  details: (id: string) => requests.get<Activity>(`/activities/${id}`),
  create: (activity: Activity) =>
    requests.post<Activity>("/activities", activity),
  update: (activity: Activity) => requests.put<void>("/activities", activity),
  delete: (id: string) => requests.delete<void>(`/activities/${id}`),
};

const agent = {
  Activities,
};

export default agent;
