import { runInAction } from "mobx";

import agent from "../api/agent";

export default class PhotoStore {
	uploading = false;

	uploadPhoto = async (file: Blob, productid) => {
		this.uploading = true;

		try {
			const response = await agent.Photos.uploadPhoto(file, productid);
			const photo = response.data;
			console.log(photo);
			alert("Upload image successful");
		} catch (error) {
			console.log(error);
			runInAction(() => (this.uploading = false));
		}
	};
}
