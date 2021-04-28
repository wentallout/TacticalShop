import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router";
import { Link } from "react-router-dom";

import { Card, Image, Button } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";

import { useStore } from "../../../app/stores/store";

export default observer(function ProductDetails() {
	const { productStore } = useStore();
	const {
		selectedProduct: product,
		loadProduct,
		loadingInitial,
	} = productStore;
	const { productid } = useParams<{ productid: string }>();

	useEffect(() => {
		if (productid) loadProduct(productid);
	}, [productid, loadProduct]);

	if (loadingInitial || !product) return <LoadingComponent />;

	return (
		<Card fluid>
			<Image
				centered
				size="medium"
				src={`https://localhost:44341/${product.productImageName}`}
			/>
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
						as={Link}
						to={`/manage/product/${product.productId}`}
						basic
						color="blue"
						content="EDIT"
					/>

					<Button
						as={Link}
						to="/products"
						basic
						color="grey"
						content="CANCEL"
					/>
				</Button.Group>
			</Card.Content>
		</Card>
	);
});
