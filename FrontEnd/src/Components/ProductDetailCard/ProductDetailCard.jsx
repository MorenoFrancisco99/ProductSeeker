import "./ProductDetailCard.css";
import { useState } from "react";

function ProductDetailCard({ product }) {
  const [displayChart, setDisplayChart] = useState(false);
  if (!product) {
    return <p>No hay datos</p>;
  }

  const ToggleChart = () => {
    setDisplayChart(displayChart => !displayChart)
    console.log("funca")
  };
  //TODO se tiene que llamar al bac para ver la evolucion historica del producto
  //TODO falta tipo de producto: alimento, farmacia, higiene, etc
  //TODO Condicionarlo para mostrar subunidades si fuera necesario
  //TODO Reemplazar el id del negocio por el nombre. Tiene que ser hyperlink para poder explorarlo
  return (
    <div className="inspect-container">
      <div className="inspect-header">
        <h2>{product.Name}</h2>
        <h3>{product.Brand}</h3>
      </div>
      <div className="inspect-content">
        <div className="inspect-photo">
          <img
            src={product.Img || "/images/placeholder.png"}
            alt={product.Name}
          />
        </div>
        <div className="inspect-info">
          <p>
            <strong>Cantidad:</strong> {product.Amount}
          </p>
          <p>
            <strong>Subunidad:</strong> {product.AmountSubUnit}
          </p>
          <p>
            <strong>Precio actual:</strong> {product.Price}{" "}
            <button className="nav-button" onClick={() => ToggleChart()}>[Ver evolución histórica]</button>
          </p>
          <p>
            <strong>Info extra:</strong> {product.ExtraInfo}
          </p>
          <p>
            <strong>Negocio asociado:</strong> {product.StoreId || "N/A"}
          </p>
          <div className="inspect-actions">
            <button className="nav-button">[Editar]</button>
            <button className="nav-button">[Eliminar]</button>
            <button className="nav-button">[Volver]</button>
          </div>
        </div>
      </div>
      <div>
        {displayChart && <>PPPPP</>}
      </div>
    </div>
  );
}

export default ProductDetailCard;
