import { useState, useRef } from "react";
import { useUserData } from "../../Provider/UserDataProvider";
import Navbar from "../Navbar/Navbar";
// Asumo que ProductDetailCard recibirá la función para guardar cambios
import ProductDetailCard from "../ProductDetailCard/ProductDetailCard"; 
import NewProductForm from "../NewProductForm/NewProductForm";
import ModifyProductCard from "../ModifyCards/ModifyProduct";
function NewBuyTab() {
  const { products, stores } = useUserData();
  const [storeSelected, setStoreSelected] = useState("");
  // productsToShow: Productos que el usuario puede AGREGAR o MODIFICAR/SELECCIONAR
  const [productsToShow, setProductsToShow] = useState(products); 
  // productsSelected: Productos que ya están SELECCIONADOS (y potencialmente MODIFICADOS)
  const [productsSelected, setProductsSelected] = useState([]);
  const [productToEdit, setProductToEdit] = useState(null);

  const dialogRef = useRef(null);
  const productDialogRef = useRef(null);

  // --- Lógica de la Tienda (Store) ---
  const HandleSelectChange = (e) => {
  const selectedStoreId = e.target.value;
  setStoreSelected(selectedStoreId);

  // 1. Identificadores de los productos que ya están seleccionados
  const selectedProductIds = productsSelected.map((p) => p.Id);
  
  // 2. Determinar la base de productos (todos o filtrados por tienda)
  let productsBase = products; // Siempre empezamos con la lista maestra

  if (selectedStoreId !== "") {
    // Si hay una tienda seleccionada, filtramos por esa tienda
    productsBase = products.filter((p) => p.StoreId == selectedStoreId);
  }

  // 3. Mostrar solo los productos de la base que NO han sido seleccionados aún
  const productsFiltered = productsBase.filter(
    (p) => !selectedProductIds.includes(p.Id)
  );

  setProductsToShow(productsFiltered);
};
  
  // --- Lógica de Agregar/Seleccionar ---
  const HandleAddButton = (product) => {
    // Al agregar, el producto pasa directamente a seleccionados SIN MODIFICACIONES
    // Usamos el producto original de 'products' si es necesario, o el que viene de 'productsToShow'
    setProductsSelected((prev) => [...prev, product]);
    // Lo ocultamos de la lista 'productsToShow'
    setProductsToShow((prev) => prev.filter((p) => p.Id !== product.Id));
  };
  
  // --- Lógica de Remover/Descartar Modificaciones ---
  const HandleRemoveButton = (productToRemove) => {
    // 1. Eliminar de la lista de seleccionados
    setProductsSelected((prev) =>
      prev.filter((p) => p.Id !== productToRemove.Id)
    );

    // 2. Descartar modificaciones y devolver a la lista de disponibles (productsToShow)
    // BUSCAMOS la versión ORIGINAL del producto en el array 'products' (del hook useUserData)
    // Esto asegura que si estaba modificado, el cambio se DESCARTA.
    const originalProduct = products.find((p) => p.Id === productToRemove.Id);

    if (originalProduct) {
      // Solo lo agregamos si coincide con el filtro de tienda actual
      if (
        storeSelected === "" ||
        originalProduct.StoreId == storeSelected
      ) {
        setProductsToShow((prev) => [...prev, originalProduct]);
      }
    }
  };

  // --- Lógica de Modificar ---
  const HandleModifyButton = (product) => {
    // Si el producto ya está seleccionado, editamos la versión en 'productsSelected'.
    // Si no está seleccionado, editamos la versión original.
    const productToStartEdit = productsSelected.find((p) => p.Id === product.Id) 
      || product;

    setProductToEdit(productToStartEdit);
    dialogRef.current.showModal();
  };
  
  // Función que se llama desde ProductDetailCard cuando el usuario guarda los cambios
  const HandleSaveModification = (modifiedProduct) => {
    // 1. Ocultar de productos disponibles (si no estaba seleccionado aún)
    setProductsToShow((prev) => prev.filter((p) => p.Id !== modifiedProduct.Id));

    // 2. Actualizar/Agregar el producto modificado a 'productsSelected'
    setProductsSelected((prev) => {
      // Verificar si el producto ya estaba en la lista de seleccionados
      const index = prev.findIndex((p) => p.Id === modifiedProduct.Id);

      if (index > -1) {
        // Si ya estaba, lo actualizamos (Editando una selección previa)
        const newSelected = [...prev];
        newSelected[index] = modifiedProduct;
        return newSelected;
      } else {
        // Si es la primera vez que se modifica, lo agregamos (Modificar -> Seleccionar)
        return [...prev, modifiedProduct];
      }
    });

    HandleCloseDialog(); // Cerrar el diálogo después de guardar
  };

  const HandleCloseDialog = () => {
    dialogRef.current.close();
    setProductToEdit(null);
  };
  const HandleNewProductButton = () => {
    productDialogRef.current.showModal();
  };

  const HandleCloseNewProductFormDialog = () => {
    productDialogRef.current.close();
  };


  const HandleSubmit= (productToSend) =>{
    console.log(productToSend)

  }
  return (
    <>
      <Navbar />
      <h1>NUEVA COMPRA</h1>
      <p>
        Los productos seleccionado aportan a la evolucion historica del producto
      </p>
      <button onClick={() => HandleNewProductButton()}>Nuevo Produto</button>
      <label htmlFor="select-store">
        Actualmente seleccionando productos de:{" "}
      </label>
      <select
        name="select-store"
        id="select-store"
        value={storeSelected}
        onChange={HandleSelectChange}
      >
        <option value="">- - - - - -</option>
        {stores &&
          stores.map((store) => (
            <option value={store.Id}>{`${store.Name}`}</option>
          ))}
      </select>
      <hr />
      <fieldset>
        <p>-------Productios disponibles----------</p>
        {productsToShow &&
          productsToShow.map((product) => (
            <div>
              <span>
                <p>{`${product.Name}`}</p>{" "}
                <button onClick={() => HandleAddButton(product)}>
                  Agregar
                </button>
                <button onClick={() => HandleModifyButton(product)}>
                  Modificar
                </button>
              </span>
            </div>
          ))}
      </fieldset>

      <fieldset>
        <p>------Productos seleccionados---------</p>
        {productsSelected &&
          productsSelected.map((product) => (
            <span>
              <p>{`${product.Name}`}</p>{" "}
              <button onClick={() => HandleRemoveButton(product)}>
                Remover
              </button>
            </span>
          ))}
      </fieldset>
     {/* DIALOG DE MODIFICACIÓN */}
      <dialog ref={dialogRef}>
        {productToEdit && (
          <>
            {/* Se pasa la función de guardar al componente ProductDetailCard */}
            <ModifyProductCard 
              product={productToEdit} 
              onSave={HandleSaveModification} 
              onClose={HandleCloseDialog}
            />
            {/* Ahora el botón de cerrar debería estar dentro de ProductDetailCard o la acción de cerrar se ejecuta después de guardar/cancelar */}
            <button onClick={HandleCloseDialog}>Cerrar sin guardar</button> 
          </>
        )}
      </dialog>
      <dialog ref={productDialogRef}>
        <>
        <NewProductForm onSubmitFunc={HandleAddButton}></NewProductForm>
          <button onClick={HandleCloseNewProductFormDialog}>Cerrar</button>
        </>
      </dialog>

      <button type="submit" onClick={() => HandleSubmit(productsSelected)}>Enviar</button>
    </>
  );
}
export default NewBuyTab;
