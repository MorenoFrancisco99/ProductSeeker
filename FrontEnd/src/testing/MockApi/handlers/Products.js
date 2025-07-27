import productos from "../data/productos_mock.json"

export function GetProducts(){
    console.log("Obteniendo productos...\n", productos)
    return Promise.resolve(productos)
}

export function PostProduct(newProduct){
    console.log("Posteando producto: \n",newProduct)
    return Promise.resolve(newProduct)
}