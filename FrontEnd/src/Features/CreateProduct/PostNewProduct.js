import { ParseProductFormToJson } from "../../utilities/Mapers/ProductMapers";

/**
 * Parses an HTMLFormElement.element into a productvar and POSTs it to the server.
 * @param {HTMLFormElement} FormInput Raw HTLMFormElement.element
 * @returns 
 */
//Currently there's no other source other than forms of new products that need to be POSTed. 
//So the parse can/should be in its own module and give the result json to the func
//But for now this will work
export function PostNewProduct(FormInput){ 

    const productJson = ParseProductFormToJson(FormInput)
    
    
}