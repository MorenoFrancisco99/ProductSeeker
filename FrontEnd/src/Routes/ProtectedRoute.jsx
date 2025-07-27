import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../Provider/AuthProvider";
import { UserDataProvider } from "../Provider/UserDataProvider";
export const ProtectedRoute = () => {
  const { token } = useAuth();

  if (!token) {
    return <Navigate to="/login" />;
  }
  return (
    <>
      <UserDataProvider>
        <Outlet />
      </UserDataProvider>
    </>
  );
};
