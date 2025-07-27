import { useState } from "react";
import "./TestStyle.css";
import NewProductForm from "../../Components/NewProductForm/NewProductForm";

function NuevaCompra() {
  const [items, setItems] = useState([]);
  const [selectedItems, setSelectedItem] = useState([]);

  const cargarProdTest = () =>{
    setItems(["asdasd", "casa", "a"]);
  }


  const AddItem = (value) => {
    setSelectedItem(previtems => [...previtems, value])
    console.log(value)
    setItems(previosstate => (previosstate.filter(item => item !== value)))
  };

  const removeItem = (value) =>{
    setSelectedItem(previtems => (previtems.filter(item => item !== value)))
    setItems(prevItems => [...prevItems, value])
  };

 
  const createNewProduct = (event)=>{
    event.preventDefault();
    console.log(event.target.prodName.value)
    setSelectedItem(previtems => [...previtems, event.target.prodName.value])

  }

  /**
   * -Se clickea la opcion de editar
   * -Se hace invoca la funcion de editar
   * -Se despliega la edicion.
   * -Se compurba si los cambios son validos.
   * -Se envia a la backend como una ALTERACION, se guarda el estado anterior de los campos cambiados
   * -Se informa del exito o fracaso
   * 
   */

  return (
    <div>
      <div style={{ display: "inline" }}>
        <p>Compra echa en:</p>
        <button >Seleccionar Negocio</button>
        <button onClick={() => cargarProdTest()}>Test cagar producctos</button>
        
      </div>
      <NewProductForm createNewProductFunc={createNewProduct} isToggleable={true}/>
      <div style={{ display: "inline" }}>
        <div>
          <div class="Content-Panel">
            <div class="autoscroll-container">
              <div className="scroll-list">
                {items &&
                  items.map((item, index) => (
                    <div class="item">
                      <span>
                        <div class="inner-text">
                          <p key={index}>{`${index + 1}. ${item}`}</p>
                        </div>
                        <button>edit</button>
                        <button onClick={() => AddItem(item)}>add</button>
                      </span>
                    </div>
                  ))}
              </div>
            </div>
          </div>
          <div class="Content-Panel">
            <div class="autoscroll-container">
              <div className="scroll-list">
                {selectedItems &&
                  selectedItems.map((sitem, index) => (
                    <div class="item">
                      <span>
                        <div class="inner-text">
                          <p key={index}>{`${index + 1}. ${sitem}`}</p>
                        </div>
                        <button>edit</button>
                        <button onClick={() => removeItem(sitem)}>remove</button>
                      </span>
                    </div>
                  ))}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
export default NuevaCompra;
