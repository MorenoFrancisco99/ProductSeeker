// ProductDetailCard.jsx (Ejemplo con un campo de nombre y precio)
import { useState } from "react";
function ModifyProductCard({ product, onSave, onClose }) {
  // Estado local para manejar los cambios del formulario
  const [editedProduct, setEditedProduct] = useState(product);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditedProduct((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Llama a la función que guarda los cambios en el componente padre
    onSave(editedProduct); 
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Modificar Producto: {product.Name}</h2>
      
      <label>
        Nombre:
        <input
          type="text"
          name="Name"
          value={editedProduct.Name}
          onChange={handleChange}
        />
      </label>
      
      <label>
        Precio:
        <input
          type="number"
          name="Price"
          value={editedProduct.Price || ''}
          onChange={handleChange}
        />
      </label>
      
      <button type="submit">Guardar Modificaciones</button>
      <button type="button" onClick={onClose}>
        Cancelar
      </button>
    </form>
  );
}

export default ModifyProductCard;