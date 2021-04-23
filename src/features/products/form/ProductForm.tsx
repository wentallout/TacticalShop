import { observer } from "mobx-react-lite";
import { ChangeEvent, useEffect, useState } from "react";

import { Button, Segment, Form } from "semantic-ui-react";

import { useStore } from "../../../app/stores/store";
import { useHistory, useParams } from "react-router";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Link } from "react-router-dom";

export default observer(function ProductForm() {
	const history = useHistory();
	const { productStore } = useStore();

	const {
		createProduct,
		updateProduct,
		loadProduct,
		loading,
		loadingInitial,
	} = productStore;
	const { productid } = useParams<{ productid: string }>();
	const [product, setProduct] = useState({
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
	});

	useEffect(() => {
		if (productid)
			loadProduct(productid).then((product) => setProduct(product!));
	}, [productid, loadProduct]);

	function handleSubmit() {
		if (product.productId.length === 0) {
			createProduct(product).then(() =>
				history.push(`/products/${product.productId}`),
			);
		} else {
			updateProduct(product).then(() =>
				history.push(`/products/${product.productId}`),
			);
		}
	}

	function handleInputChange(
		event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
	) {
		const { name, value } = event.target;
		setProduct({ ...product, [name]: value });
	}

	if (loadingInitial) return <LoadingComponent content="Loading product..." />;
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
					as={Link}
					to="/products"
					floated="right"
					type="button"
					content="CANCEL"
				/>
			</Form>
		</Segment>
	);
});
