import { makeAutoObservable, runInAction } from "mobx";
import { Photo, Profile } from "../models/profile";
import { store } from "./store";
import agent from "../api/agent";

export default class ProfileStore {
  profile: Profile | null = null;
  loadingProfile = false;
  uploading = false;
  loading = false;

  constructor() {
    makeAutoObservable(this);
  }

  get isCurrentUser() {
    if (store.userStore.user && this.profile) {
      return store.userStore.user.username === this.profile.username;
    }
    return false;
  }

  loadProfile = async (username: string) => {
    this.loadingProfile = true;

    try {
      const profile = await agent.Profiles.get(username);

      runInAction(() => {
        this.profile = profile;
        this.loadingProfile = false;
        console.log(profile.photos?.length);

        profile.photos?.forEach(element => {
          console.log(element.id);
        });
      });
    } catch (error) {
      console.log(error);
      runInAction(() => (this.loadingProfile = false));
    }
  };

  uploadPhoto = async (file: Blob) => {
    this.uploading = true;
    try {
      const response = await agent.Profiles.uploadPhoto(file);
      const photo = response.data;
      runInAction(() => {
        if (this.profile) {
          this.profile.photos?.push(photo);
          if (photo.isMain && store.userStore.user) {
            store.userStore.setImage(photo.url);
            this.profile.image = photo.url;
          }
        }
        this.uploading = false;
      });
    } catch (error) {
      runInAction(() => (this.uploading = false));
    }
  };

  setMainPhoto = async (photo: Photo) => {
    this.loading = true;
    try {
      await agent.Profiles.setMainPhoto(photo.id);
      store.userStore.setImage(photo.url);
      runInAction(() => {
        if (this.profile && this.profile.photos) {
          const currentMain = this.profile.photos.find((p) => p.isMain);
          if (currentMain) currentMain.isMain = false;

           const newMain = this.profile.photos.find((p) => p.id === photo.id);
           if (newMain) newMain.isMain = true;

           this.profile.image = photo.url; // Update profile image
           store.userStore.setImage(photo.url); // Sync with user store
        }
        this.loading = false;
      });
    } catch (error) {
      runInAction(() => (this.loading = false));
    }
  };

  deletePhoto = async (photo: Photo) => {
    this.loading = true;
    
    try {
      await agent.Profiles.deletePhoto(photo.id);
      runInAction(() => {
        if(this.profile) {
          this.profile.photos = this.profile.photos?.filter(p => p.id !== photo.id);
          this.loading = false;
        }
      })
    } catch (error) {
      runInAction(() => this.loading = false);
    }
  }
}
