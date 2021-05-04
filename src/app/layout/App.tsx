import { Container } from "semantic-ui-react";
import NavBar from "./NavBar";
import ProductDashboard from "../../features/products/dashboard/ProductDashboard";
import { observer } from "mobx-react-lite";
import HomePage from "../../features/home/HomePage";
import ProductForm from "../../features/products/form/ProductForm";
import { Route, useLocation } from "react-router-dom";
import ProductDetails from "../../features/products/details/ProductDetails";
import Auth from "../../features/oidc/Auth";
import CategoryDetails from "../../features/categories/details/CategoryDetails";
import CategoryDashboard from "../../features/categories/dashboard/CategoryDashboard";
import CategoryForm from "../../features/categories/form/CategoryForm";
import PhotoForm from "../../features/photos/PhotoForm";
import UserDashboard from "../../features/users/dashboard/UserDashboard";
import "./styles.css";
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
							<Route exact path="/categories" component={CategoryDashboard} />
							<Route exact path="/users" component={UserDashboard} />

							<Route
								exact
								path="/products/:productid"
								component={ProductDetails}
							/>

							<Route
								exact
								path="/categories/:categoryid"
								component={CategoryDetails}
							/>

							<Route
								key={location.key}
								exact
								path={["/createProduct", "/manage/product/:productid"]}
								component={ProductForm}
							/>

							<Route
								exact
								path={["/addPhoto", "/addphoto"]}
								component={PhotoForm}
							/>

							<Route
								// key={locationCategory.key}
								exact
								path={["/createCategory", "/manage/category/:categoryid"]}
								component={CategoryForm}
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
