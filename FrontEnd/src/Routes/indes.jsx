import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "../Provider/AuthProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import Layout from "../Pages/Layout";
import Home from "../Pages/Home";
import UserHome from "../Pages/ProtectedPages/UserHome";
import Login from "../Pages/Login";
import Register from "../Pages/Register";
import NuevaCompra from "../Pages/ProtectedPages/NuevaCompra";
import MisProductos from "../Pages/ProtectedPages/MisProductos";
import MisTiendas from "../Pages/ProtectedPages/MisNegocios";
import UserCollection from "../Pages/ProtectedPages/UserCollection";
import InspectElement from "../Pages/ProtectedPages/InspectElement";
const Routes = () => {
  const { token } = useAuth();

  const RoutesForPublic = [
    {
      path: "/something",
      element: <div>Something page</div>
    },
  ];

  const RoutesForAuthenticatedOnly = [
    {
      path: "/",
      element: <ProtectedRoute />,
      children: [
        {
          path: "/UserHome",
          element: <UserHome />
        },
        {
          path: "/NuevaCompra",
          element: <NuevaCompra />,
        },
        {
          path: "/MisProductos",
          element: <MisProductos/>
        },
        {
          path:"/UserCollection",
          element: <UserCollection/>
        },
        {
          path:"/InspectElement/:id",
          element: <InspectElement/>
        }
      ],
    },
  ];

  const RoutesForNotAuthenticatedOnly = [
    {
      element: <Layout />,
      children: [
        { path: "/", element: <Home /> },
        { path: "/Login", element: <Login /> },
        { path: "/Register", element: <Register /> },
      ],
    },
  ];

  const router = createBrowserRouter([
    ...RoutesForPublic,
    ...(!token ? RoutesForNotAuthenticatedOnly : []),
    ...RoutesForAuthenticatedOnly,
  ]);

  return <RouterProvider router={router} />;
};
export default Routes;
