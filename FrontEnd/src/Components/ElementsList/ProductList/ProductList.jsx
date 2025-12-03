import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import "../ElementsList.css";

export default function ProductsList({ products }) {
  const Navigate = useNavigate();
  const categories = ["Nombre", "Marca", "Precio", "Cantidad"];

  const [sortedProducts, setSortedProducts] = useState([...products]);
  const [sortConfig, setSortConfig] = useState({ key: "Nombre", direction: "ascending" });

  useEffect(() => {
    // Cuando la lista de productos cambie, actualiza la lista ordenada por defecto (Nombre)
    const sorted = [...products].sort((a, b) => {
      if (a.Name < b.Name) {
        return -1;
      }
      if (a.Name > b.Name) {
        return 1;
      }
      return 0;
    });
    setSortedProducts(sorted);
    setSortConfig({ key: "Nombre", direction: "ascending" });
  }, [products]);

  const HandleClick = (clickedElement) => {
    Navigate(`/InspectElement/${clickedElement.Id}`, {
      state: { clickedElement },
    });
  };

  const sortList = (key) => {
    let direction = "ascending";
    if (sortConfig.key === key && sortConfig.direction === "ascending") {
      direction = "descending";
    }

    const sorted = [...sortedProducts].sort((a, b) => {
      // Mapear el nombre de la categoría a la propiedad del objeto
      const sortKey = {
        "Nombre": "Name",
        "Marca": "Brand",
        "Precio": "Price",
        "Cantidad": "Quantity"
      }[key];

      const aValue = a[sortKey];
      const bValue = b[sortKey];

      // Lógica de ordenamiento para valores numéricos vs. cadenas de texto
      if (typeof aValue === 'number' && typeof bValue === 'number') {
        if (aValue < bValue) {
          return direction === "ascending" ? -1 : 1;
        }
        if (aValue > bValue) {
          return direction === "ascending" ? 1 : -1;
        }
      } else { // Ordenamiento por cadena de texto
        if (aValue < bValue) {
          return direction === "ascending" ? -1 : 1;
        }
        if (aValue > bValue) {
          return direction === "ascending" ? 1 : -1;
        }
      }
      return 0;
    });

    setSortedProducts(sorted);
    setSortConfig({ key, direction });
  };

  

  return (
    <div className="autoscroll-container">
      <div className="table-head">
        {categories.map((category) => (
          <div
            key={category}
            className={`table-head-item ${sortConfig.key === category ? sortConfig.direction : ""}`}
            onClick={() => sortList(category)}
          >
            {category}
          </div>
        ))}
      </div>
      <div className="scroll-list">
        {sortedProducts && sortedProducts.length > 0 ? (
          sortedProducts.map((aProduct, index) => (
            <div
              key={aProduct.Id || index}
              className="item"
              onClick={() => HandleClick(aProduct)}
            >
              <div className="inner-text">
                <p>
                  {`${aProduct.Name}, ${aProduct.Brand} - $${aProduct.Price} ${aProduct.Quantity} ${aProduct.UnitType}`}
                </p>
              </div>
            </div>
          ))
        ) : (
          <p>No hay productos.</p>
        )}
      </div>
    </div>
  );
}