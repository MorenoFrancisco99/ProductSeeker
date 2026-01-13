import { Link } from "react-router-dom";
import "./DashBoard.css";
import { useUserData } from "../../Provider/UserDataProvider";
function DashBoard() {
  const {products,  stores} = useUserData();
  console.log(stores)
  return (
    <div className="dashboard-container">
      <div className="userinfo-bar">
        <h2>Bienvenido USER </h2>
        <p>[Cerrar sesion]</p>
      </div>
      <div className="userdata-state">
        <p className="user-products">Productos: {`${products.length}`}</p>
        <p className="user-stores">Comercios: {`${stores.length}`}</p>
      </div>
      <div className="main-content">
        <div className="left-content">
          <div className="nav-options">
            <p>Ir a:</p>
            <Link to="/UserCollection" className="nav-button">[Mi coleccion]</Link>
            <Link className="nav-button">[Buscar] </Link>
          </div>
          <div className="create-options">

            <p>Crear:</p>
            <Link className="nav-button">[Nueva Compra]</Link>{" "}
            <Link className="nav-button">[Nuevo Comercio]</Link>{" "}
          </div>
        </div>
        <div className="right-content">
          <p>Ulitmos productos:</p>
          <textarea name="Placeholder" id="Placeholder"></textarea>
        </div>
      </div>
    </div>
  );
}
export default DashBoard;
