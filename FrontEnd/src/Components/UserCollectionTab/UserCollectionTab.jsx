import Navbar from "../../Components/Navbar/Navbar";
import ElementsList from "../../Components/ElementsList/ElementsList";
import { useState, useEffect } from "react";
import { useUserData } from "../../Provider/UserDataProvider";
import SearchBar from "../../Components/SearchBar/SearchBar";

function UserCollectionTab(){
 const [activeTab, setActiveTab] = useState("products");
  const { products, stores } =
    useUserData();
  const [selectedProducts, setSelectedProducts] = useState([]);
  const [selectedStores, setSelectedStores] = useState([]);

  useEffect(() => {
    if (products) {
      setSelectedProducts(products);
    }
  }, [products]);

  useEffect(() => {
    if (stores) {
      setSelectedStores(stores);
    }
  }, [stores]);

  const HandleSearch = (searchTerm) => {
    if (searchTerm.trim().length === 0) {
      setSelectedProducts(products);
      setSelectedStores(stores);
    } else {
      const loweredTerm = searchTerm.toLowerCase();
      if (activeTab == "products") {
        setSelectedProducts(
          products.filter((x) => x.Name.toLowerCase().includes(loweredTerm))
        );
      } else if (activeTab == "stores") {
        setSelectedStores(
          stores.filter((x) => x.Name.toLowerCase().includes(loweredTerm))
        );
      }
    }
  };

  return (
    <div>
      <Navbar />
      <div className="content-container">
        <span onClick={() => setActiveTab("products")}>
          Lista de productos
        </span>
        <span className="separator">/</span>
        <span onClick={() => setActiveTab("stores")}>Lista de Negocios</span>

        <div className="actions-bar">
          <p>[Nuevo producto]</p>
          <p>[seleccionar negocio]</p>
          <SearchBar onSearch={HandleSearch} />
        </div>
        <div className="elements-container"></div>
        <ElementsList
          products={selectedProducts}
          stores={selectedStores}
          activeTab={activeTab}
        />
      </div>
    </div>
  );
}
export default UserCollectionTab