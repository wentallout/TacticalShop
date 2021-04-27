import "./styles.css";
import { Container } from "semantic-ui-react";
import NavBar from "./NavBar";
import ProductDashboard from "../../features/products/dashboard/ProductDashboard";
import { observer } from "mobx-react-lite";
import HomePage from "../../features/home/HomePage";
import ProductForm from "../../features/products/form/ProductForm";
import { Route, useLocation } from "react-router-dom";
import ProductDetails from "../../features/products/details/ProductDetails";
import Auth from "../../features/oidc/Auth";

function App() {
	const location = useLocation();

	return (
		<>
			<Route exact path="/" component={HomePage} />

			<Route
				path={"/(.+)"}
				render={() => (
					<>
						<NavBar />
						<Container style={{ marginTop: "7em" }}>
							<Route exact path="/products" component={ProductDashboard} />
							<Route path="/products/:productid" component={ProductDetails} />
							<Route
								key={location.key}
								path={["/createProduct", "/manage/:productid"]}
								component={ProductForm}
							/>
							<Route path="/authentication/:action" component={Auth} />
						</Container>
					</>
				)}
			/>
		</>
	);
}

export default observer(App);
