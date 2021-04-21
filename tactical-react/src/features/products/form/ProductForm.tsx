/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { ChangeEvent, useState } from "react";

import { Button, Segment, Form } from "semantic-ui-react";

import { Product } from "../../../app/models/product";

interface Props {
	product: Product | undefined;
	closeForm: () => void;
	createOrEdit: (product: Product) => void;
	submitting: boolean;
}

export default function ProductForm({
	product: selectedProduct,
	closeForm,
	createOrEdit,
	submitting,
}: Props) {
	const initialState = selectedProduct ?? {
		productId: "",
		productName: "",
		productPrice: "",
		productDescription: "",
		productImageName: "",
		productQuantity: "",
		categoryId: "",
		brandId: "",
		categoryName: "",
		brandName: "",
		createdDate: "",
		updatedDate: "",
		starRating: "",
	};

	const [product, setProduct] = useState(initialState);

	function handleSubmit() {
		createOrEdit(product);
	}

	function handleInputChange(
		event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
	) {
		const { name, value } = event.target;
		setProduct({ ...product, [name]: value });
	}

	return (
		<Segment clearing>
			<Form onSubmit={handleSubmit} autoComplete="off">
				<Form.Input
					placeholder="productName"
					value={product.productName}
					name="productName"
					onChange={handleInputChange}></Form.Input>

				<Form.TextArea
					placeholder="Description"
					value={product.productDescription}
					name="productDescription"
					onChange={handleInputChange}></Form.TextArea>

				<Form.Input
					placeholder="Price"
					value={product.productPrice}
					name="productPrice"
					onChange={handleInputChange}></Form.Input>

				<Form.Input
					placeholder="Category"
					value={product.categoryId}
					name="categoryId"
					onChange={handleInputChange}></Form.Input>

				<Form.Input
					placeholder="Brand"
					value={product.brandId}
					name="brandId"
					onChange={handleInputChange}></Form.Input>

				<Form.Input
					placeholder="Quantity"
					value={product.productQuantity}
					name="productQuantity"
					onChange={handleInputChange}></Form.Input>

				<Button
					loading={submitting}
					floated="right"
					positive
					type="submit"
					content="SUBMIT"
				/>
				<Button
					onClick={closeForm}
					floated="right"
					type="button"
					content="CANCEL"
				/>
			</Form>
		</Segment>
	);
}
