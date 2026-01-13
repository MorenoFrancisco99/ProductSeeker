import axios from "axios";

const URL = "https://localhost:7111/api";

export function getUserProducts() {
  axios({
    url: "https://localhost:7111/api/Store",
    method: "GET",
  })
    .then((res) => console.log(res))
    .catch((err) => console.log(err));
}

export async function LoginUser(username, password) {
  try{
    const response = await axios.post(URL + "/Account/login", {
    username,
    password,
  });

  return response;
}
catch(error){
    console.log("Error al intentar al hacer login", error)
    throw error
}
}

export async function RegisterUser(username, password, email) {
try{
    const response = await axios.post(URL + "/Account/register",{
      username, 
      password,
      email
    })

    return response
}
    catch(error){
        console.log("Error al intentar registrar el usuario", error)
        throw error
    }
}

export async function PostProduct(product) {
  try{
      const response = await axios.post(URL + '/Product')
  }catch{

  }
  
}