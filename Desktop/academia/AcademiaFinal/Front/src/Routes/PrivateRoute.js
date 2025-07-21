import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../Contexts/AuthContext";

const PrivateRoute = () => {
  const { token, loadingAuth } = useAuth();

  if (loadingAuth) {
    return null;
  }

  if (!token) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
};

export default PrivateRoute;
