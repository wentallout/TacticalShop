import { Button, Form, Grid, Header } from "semantic-ui-react";
import PhotoWidgetDropzone from "./PhotoWidgetDropzone";
import { useState, useEffect, ChangeEvent } from "react";
import PhotoWidgetCropper from "./PhotoWidgetCropper";
import { useStore } from "../../app/stores/store";

export default function PhotoForm() {
	const [files, setFiles] = useState<any>([]);
	const [cropper, setCropper] = useState<Cropper>();

	const [photo, setPhoto] = useState({
		productid: 0,
	});

	const {
		photoStore: { uploadPhoto },
	} = useStore();

	function onCrop() {
		if (cropper) {
			cropper.getCroppedCanvas().toBlob((blob) => handlePhotoUpload(blob!));
		}
	}

	function handlePhotoUpload(file: Blob) {
		if (photo.productid !== 0) {
			uploadPhoto(file, photo.productid);
		}
	}

	useEffect(() => {
		return () =>
			files.forEach((file: any) => URL.revokeObjectURL(file.preview));
	}, [files]);

	function handleInputChange(
		event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
	) {
		const { name, value } = event.target;
		setPhoto({ ...photo, [name]: value });
	}

	return (
		<Grid>
			<Grid.Column width={4}>
				<Header color="teal" content="STEP 1 - ADD PHOTO" />

				<PhotoWidgetDropzone setFiles={setFiles} />
			</Grid.Column>

			<Grid.Column width={1} />
			<Grid.Column width={4}>
				<Header color="teal" content="STEP 2 - Resize Image" />
				{files && files.length > 0 && (
					<PhotoWidgetCropper
						setCropper={setCropper}
						imagePreview={files[0].preview}
					/>
				)}
			</Grid.Column>
			<Grid.Column width={1} />
			<Grid.Column width={4}>
				<Header color="teal" content="STEP 3 - Preview and upload" />
				{files && files.length > 0 && (
					<>
						<div
							className="img-preview"
							style={{ minHeight: 200, overflow: "hidden" }}></div>

						<Form onSubmit={onCrop}>
							<Form.Input
								placeholder="ProductId"
								value={photo.productid}
								name="productid"
								onChange={handleInputChange}></Form.Input>
							<Button.Group widths={2}>
								<Button type="submit" positive icon="check" />
								<Button onClick={() => setFiles([])} icon="close" />
							</Button.Group>
						</Form>
					</>
				)}
			</Grid.Column>
		</Grid>
	);
}
