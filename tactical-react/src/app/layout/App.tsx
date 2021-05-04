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
<<<<<<< HEAD

import "./styles.css";
import PhotoForm from "../../features/photos/PhotoForm";
=======
import PhotoForm from "../../features/photos/PhotoForm";
import UserDashboard from "../../features/users/dashboard/UserDashboard";
import "./styles.css";
>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55
function App() {
	const location = useLocation();

	return (
		<>
			<Route exact path="/" component={HomePage} />
<<<<<<< HEAD

			<Route
				path={"/(.+)"}
				render={() => (
					<>
						<NavBar />
						<Container style={{ marginTop: "7em" }}>
							<Route exact path="/products" component={ProductDashboard} />
							<Route exact path="/categories" component={CategoryDashboard} />
=======

			<Route
				path={"/(.+)"}
				render={() => (
					<>
						<NavBar />
						<Container style={{ marginTop: "7em" }}>
							<Route exact path="/products" component={ProductDashboard} />
							<Route exact path="/categories" component={CategoryDashboard} />
							<Route exact path="/users" component={UserDashboard} />

>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55
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
