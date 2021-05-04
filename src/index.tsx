import ReactDOM from "react-dom";
import App from "./app/layout/App";
import reportWebVitals from "./reportWebVitals";
import "semantic-ui-css/semantic.min.css";
import { store, StoreContext } from "./app/stores/store";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { storeoidc } from "./features/oidc/storeoidc";
import "./app/layout/styles.css";

ReactDOM.render(
	<>
		<StoreContext.Provider value={store}>
			<BrowserRouter>
				<Provider store={storeoidc}>
					<App />
				</Provider>
			</BrowserRouter>
		</StoreContext.Provider>
	</>,
	document.getElementById("root"),
);

reportWebVitals();
