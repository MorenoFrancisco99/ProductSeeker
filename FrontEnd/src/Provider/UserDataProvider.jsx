import { createContext, useState, useEffect , useContext} from "react";
import axios from "axios";
import * as TestApi from "../testing/MockApi/ApiTestClient";
import { Outlet } from "react-router-dom";

const UserDataContext = createContext();

export const UserDataProvider = ({ children }) => {
  const [products, setProducts] = useState([]);
  const [stores, setBusinesses] = useState([]);

  // Cargar datos al login o inicialización
  useEffect(() => {
    const fetchData = async () => {
      const prodRes = await TestApi.GetProducts(); 
      const busRes = await TestApi.GetBusiness();
      setProducts(prodRes);
      setBusinesses(busRes);
      //   const [prodRes, busRes] = await Promise.all([
      //     axios.get("/api/user/products"),
      //     axios.get("/api/user/businesses")
      //   ]);
      //   setProducts(prodRes.data);
      //   setBusinesses(busRes.data);
    };
    fetchData();
  }, []);

  const addProduct = async (newProduct) => {
    const res = await TestApi.PostProduct(newProduct);
    setProducts((prev) => [...prev, res]);
    console.log("Nuevo producto posteado. Estado: \n", products);
    //const res = await axios.post("/api/user/products", newProduct);
    // setProducts((prev) => [...prev, res.data]);
  };

  const addBusiness = async (newBusiness) => {
    const res = await TestApi.PostBusiness(newBusiness);
    setBusinesses((prev) => [...prev, res]);
    console.log("Nuevo Business posteado. Estado: \n", businesses);
    //const res = await axios.post("/api/user/businesses", newBusiness);
    //setBusinesses((prev) => [...prev, res.data]);
  };

  const refreshData = async () => {
    const prodRes = await TestApi.GetProducts();
    const busRes = await TestApi.GetBusiness();
    setProducts(prodRes);
    setBusinesses(busRes);
    // const [prodRes, busRes] = await Promise.all([
    //   axios.get("/api/user/products"),
    //   axios.get("/api/user/businesses"),
    //]);
    //setProducts(prodRes.data);
    //setBusinesses(busRes.data);
  };

  return (
    <UserDataContext.Provider
      value={{
        products,
        stores,
        addProduct,
        addBusiness,
        refreshData,
      }}
    >
      {children}
    </UserDataContext.Provider>
  );
};

export const useUserData = () => useContext(UserDataContext);
