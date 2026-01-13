import Card from "../Card/Card";
import "./ElementsList.css";
import { useNavigate } from "react-router-dom";

export default function ElementsList({
  products,
  stores,
  activeTab,
  categories,
}) {
  const Navigate = useNavigate();

  const HandleClick = (ClickedElement) => {
    Navigate(`/InspectElement/${ClickedElement.Id}`, { state: { ClickedElement } });
  };
  return (
    <div className="autoscroll-container">
      <div className="table-head">
        {categories &&
          categories.map((category) => (
            <div className="table-head-item">{`${category}`}</div>
          ))}
      </div>

      <div className="scroll-list">
        {activeTab === "products" ? (
          products && products.length > 0 ? (
            products.map((aProduct, index) => (
              <div className="item" onClick={() => HandleClick(aProduct)}>
                <div className="inner-text">
                  <p key={index}>{`${index + 1}. ${aProduct.Name}, $${
                    aProduct.Price
                  }`}</p>
                </div>
              </div>
            ))
          ) : (
            <p>No hay productos.</p>
          )
        ) : stores && stores.length > 0 ? (
          stores.map((aBussines, index) => (
            <div className="item" onClick={() => HandleClick(aBussines)}>
              <div className="inner-text">
                <p key={index}>{`${index + 1}. ${aBussines.Name}`}</p>
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
