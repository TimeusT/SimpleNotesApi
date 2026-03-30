import ReactDOM from "react-dom/client";
import App from "./App";
import { Auth0Provider } from "@auth0/auth0-react";
import { StrictMode } from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { MessageProvider } from "./components/MessageContext";

const domain = "dev-kgtk8ordbs6z6d26.us.auth0.com";
const clientId = "SQp4JNKu6hd2MOqb1bGI5wXkKbd7Tj33";
const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById("root")).render(
  <StrictMode>
    <Auth0Provider
      domain={domain}
      clientId={clientId}
      authorizationParams={{ redirect_uri: window.location.origin }}
    >
      <QueryClientProvider client={queryClient}>
        <MessageProvider>
          <App />
        </MessageProvider>
      </QueryClientProvider>
    </Auth0Provider>
  </StrictMode>,
);
