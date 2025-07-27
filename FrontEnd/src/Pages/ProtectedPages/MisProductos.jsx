import { useEffect } from "react";
import { useState } from "react";
import { PostNewProduct } from "../../Features/CreateProduct/PostNewProduct";
import NewProductForm from "../../Components/NewProductForm/NewProductForm";
import ScrolleableListSingleBttn from "../../Components/ScrolleableLists/ScrolleableListSingleButton/ScrolleableListSingleBttn";
import * as TestApi from "../../testing/MockApi/ApiTestClient";

function MisProductos() {
  const [testitems, setTestItems] = useState([]);
  


  const testcategories = ["a", "b", "c"]
    
  const dummy = () =>{
    /**
     * NUEVO PRODUCTO
     * -Recibir objeto
     * -Checkear si es valido
     * -Enviarlo al back para un post
     * -Si es exito, retorna:
     *      -Codigo que indica exito 
     *      - Objeto Producto a manipular
     * * -Si falla: alert Error en crear produicto
     *      - Codigo que indica el fracaso
     * 
     * --croquis---
     * const mimetodocallbak (objetoretornado){
     *      const outcome = MiFeaturePost(objetoretornado) //Esta es una feature que checkea el objeto y retorna lo necesario. NO es el metodo axios de post
     * 
     *      if outcome.success
     *          MethodA //Esto es un metodo que muestra un cartel(no alert) que la operacion fue un exito. Aca igual se puede agregar el objeto a lista
     *      else{
     *      MethodB //Metodo que muestra en pantalla el error en cuestion a partir de un campo por ejemplo outcome.error     
     *      }
     * }
     */
  }



  const mycallbackfunc= (formRaw) =>{

    PostNewProduct(formRaw.target)
  }


  return (
    <div>
      <button>TEST</button>
      <div>
        <ScrolleableListSingleBttn
          items={testitems}
          categories={testcategories}
          optionsfunc={dummy}
        />
      </div>
      <div>
        <NewProductForm onSubmitFunc={mycallbackfunc} isToggleable={true}/>
      </div>
    </div>
  );
}
export default MisProductos;
