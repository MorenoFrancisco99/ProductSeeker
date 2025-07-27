import { useLocation } from "react-router-dom";
import ProductDetailCard from "../../Components/ProductDetailCard/ProductDetailCard";
import BussinesDetailCard from "../../Components/BussinesDetailCard/BussinesDetailCard";
function InspectElement() {
  const location = useLocation();
  const element = location.state.ClickedElement; //Este nombre debe coincidir con el nombre del objeto envviado en el state de Navigate()
  
  //Se usa el parametro de bussinesdays para diferenciar a que componente ir. 
  return (
    <>
      {element.BussinesDays == undefined ? (
        <ProductDetailCard product={element} />
      ) : (
        <BussinesDetailCard commerce={element} />
      )}
    </>
  );
}
export default InspectElement;
