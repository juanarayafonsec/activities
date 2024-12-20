import { observer } from "mobx-react-lite";
import {
  Button,
  ButtonGroup,
  Card,
  CardGroup,
  Grid,
  GridColumn,
  Header,
  Image,
  TabPane,
} from "semantic-ui-react";
import { Photo, Profile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";
import { SyntheticEvent, useEffect, useState } from "react";
import PhotoUploadWidget from "../../app/common/imageUpload/PhotoUploadWidget";

interface Props {
  profile: Profile;
}
export default observer(function ProfilePhotos({ profile }: Props) {
  const {
    profileStore: {
      isCurrentUser,
      uploadPhoto,
      uploading,
      loading,
      setMainPhoto,
      deletePhoto,
    },
  } = useStore();
  const [addPhotoMode, setAddPhotoMode] = useState(false);
  const [target, setTarget] = useState("");

  function handlePhotoUpload(file: Blob) {
    uploadPhoto(file).then(() => setAddPhotoMode(false));
  }

  function handleSetMainPhoto(
    photo: Photo,
    e: SyntheticEvent<HTMLButtonElement>
  ) {
    setTarget(e.currentTarget.name);
    setMainPhoto(photo);
  }

  function handleDeletePhoto(
    photo: Photo,
    e: SyntheticEvent<HTMLButtonElement>
  ) {
    setTarget(e.currentTarget.name);
    deletePhoto(photo);
  }

  useEffect(() => {
    console.log(profile.photos?.length);

    profile.photos?.forEach((element) => {
      console.log(element.id);
    });
  });

  return (
    <TabPane>
      <Grid>
        <GridColumn width={16}>
          <Header floated="left" icon="image" content="Photos" />
          {isCurrentUser && (
            <Button
              floated="right"
              basic
              content={addPhotoMode ? "Cancel" : "Add Photo"}
              onClick={() => setAddPhotoMode(!addPhotoMode)}
            />
          )}
        </GridColumn>
        <GridColumn width={16}>
          {addPhotoMode ? (
            <PhotoUploadWidget
              uploadPhoto={handlePhotoUpload}
              loading={uploading}
            />
          ) : (
            <CardGroup itemsPerRow={5}>
              {profile.photos?.map((p) => (
                <Card key={p.id}>
                  <Image src={p.url} />
                  {isCurrentUser && (
                    <ButtonGroup fluid widths={2}>
                      <Button
                        basic
                        color="green"
                        content="Main"
                        name={"main" + p.id}
                        disabled={p.isMain}
                        loading={target === "main" + p.id && loading}
                        onClick={(e) => handleSetMainPhoto(p, e)}
                      />
                      <Button
                        basic
                        color="red"
                        icon="trash"
                        loading={target === "deelte" + p.id && loading}
                        onClick={(e) => handleDeletePhoto(p, e)}
                        disabled={p.isMain}
                        name={"deelte" + p.id}
                      />
                    </ButtonGroup>
                  )}
                </Card>
              ))}
            </CardGroup>
          )}
        </GridColumn>
      </Grid>
    </TabPane>
  );
});
