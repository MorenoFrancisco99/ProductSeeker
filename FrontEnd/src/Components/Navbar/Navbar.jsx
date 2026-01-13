import { Link } from "react-router-dom";
import "./Navbar.css";

function Navbar() {
  return (
    <div className="main-container">
      <div className="main-header">
        <p>[Home]</p>
        <p>/Usuario/</p>
        <p>Productos: N</p>
        <p>Negiocis: M</p>
        <p>[Cerrar sesion]</p>
      </div>
      <div className="nav-bar">
            <p>Mi coleccion</p>
            <p>Nueva Compa</p>
            <p>Bucar</p>
      </div>
      </div>
  );
}



export default Navbar;
