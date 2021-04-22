import React from "react";

import { Card, Image, Button } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";

import { useStore } from "../../../app/stores/store";

export default function ProductDetails() {
	const { productStore } = useStore();
	const {
		selectedProduct: product,
		openForm,
		cancelSelectedProduct,
	} = productStore;

	if (!product) return <LoadingComponent />;

	return (
		<Card fluid>
			<Image src={`https://localhost:44341/${product.productImageName}`} />
			<Card.Content>
				<Card.Header>{product.productName}</Card.Header>
				<Card.Meta>
					<span className="date">{product.updatedDate}</span>
				</Card.Meta>
				<Card.Description>{product.productDescription}</Card.Description>
			</Card.Content>
			<Card.Content extra>
				<Button.Group widths="2">
					<Button
						onClick={() => openForm(product.productId)}
						basic
						color="blue"
						content="EDIT"
					/>

					<Button
						onClick={cancelSelectedProduct}
						basic
						color="grey"
						content="CANCEL"
					/>
				</Button.Group>
			</Card.Content>
		</Card>
	);
}
