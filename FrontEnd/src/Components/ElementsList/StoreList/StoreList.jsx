import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import "../ElementsList.css";

export default function StoresList({ stores }) {
  const Navigate = useNavigate();
  const categories = ["Nombre", "Tipo"];

  const [sortedStores, setSortedStores] = useState([...stores]);
  const [sortConfig, setSortConfig] = useState({ key: "Nombre", direction: "ascending" });

  useEffect(() => {
    // Cuando la lista de tiendas cambie, actualiza la lista ordenada por defecto (Nombre)
    const sorted = [...stores].sort((a, b) => {
      if (a.Name < b.Name) {
        return -1;
      }
      if (a.Name > b.Name) {
        return 1;
      }
      return 0;
    });
    setSortedStores(sorted);
    setSortConfig({ key: "Nombre", direction: "ascending" });
  }, [stores]);

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

    const sorted = [...sortedStores].sort((a, b) => {
      const aValue = a[key === "Nombre" ? "Name" : key === "Tipo" ? "Type" : key];
      const bValue = b[key === "Nombre" ? "Name" : key === "Tipo" ? "Type" : key];

      if (aValue < bValue) {
        return direction === "ascending" ? -1 : 1;
      }
      if (aValue > bValue) {
        return direction === "ascending" ? 1 : -1;
      }
      return 0;
    });

    setSortedStores(sorted);
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
        {sortedStores && sortedStores.length > 0 ? (
          sortedStores.map((aStore, index) => (
            <div
              key={aStore.Id || index}
              className="item"
              onClick={() => HandleClick(aStore)}
            >
              <div className="inner-text">
                <p>
                  {`${aStore.Name} - ${aStore.Type}`}
                </p>
              </div>
            </div>
          ))
        ) : (
          <p>No hay negocios.</p>
        )}
      </div>
    </div>
  );
}