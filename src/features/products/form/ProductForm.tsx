/* eslint-disable @typescript-eslint/no-unused-vars */
import { observer } from "mobx-react-lite";
import React, { ChangeEvent, useState } from "react";

import { Button, Segment, Form } from "semantic-ui-react";

import { Product } from "../../../app/models/product";
import { useStore } from "../../../app/stores/store";

export default observer(function ProductForm() {
	const { productStore } = useStore();

	const {
		selectedProduct,
		closeForm,
		createProduct,
		updateProduct,
		loading,
	} = productStore;

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
		product.productId ? updateProduct(product) : createProduct(product);
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
					loading={loading}
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
});
