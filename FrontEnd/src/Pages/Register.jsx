import { useState, useActionState } from "react";
import { RegisterUser } from "../api/ApiServices";

function Register() {
  const [username, setUsername] = useState("");
  const [userpassword, setPassword] = useState("");
  const [userEmail, setUserEmail] = useState("");
  const [errorMsg, setErrorMsg] = useState("");


  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(username, userpassword, userEmail)


      // Validación básica
      if (!username.trim() || !userpassword.trim()) {
        setErrorMsg("Todos los campos son obligatorios.");
        return;
      }
  

    try {
        const response = await RegisterUser(username, userpassword, userEmail)
        console.log(response)

    } catch (error) {
         // Errores del servidor
      if (error.response) {
        if (error.response.status === 401) {
          setErrorMsg("Contraseña incorrecta.");
        } else if (error.response.status === 404) {
          setErrorMsg("Usuario no encontrado.");
        } else if (error.response.status === 400){
          setErrorMsg("Ocurrio un error al enviar la informacion.")
        }
        else {
          setErrorMsg("Ocurrió un error en el servidor.");
        }
      } else if (error.request) {
        setErrorMsg("No se pudo conectar con el servidor.");
      } else {
        setErrorMsg("Error desconocido.");
      }
    }
  };
 
  return (
    <>
        <h1>This is a registration form</h1>
        {errorMsg && <p style={{ color: "red" }}>{errorMsg}</p>}
        <form onSubmit={handleSubmit}>
        <label>Nombre</label>
        <br />
        <input
          type="text"
          placeholder="Nombre de usuario"
          id="username"
          value={username}
          onChange={(e) => {
            setUsername(e.target.value);
          }}
        ></input>
        <br />

        <label>Email</label>
        <br />
        <input
          type="email"
          placeholder="Email de usuario"
          id="email"
          value={userEmail}
          onChange={(e) => {
            setUserEmail(e.target.value);
          }}
        ></input>
        <br />

        <label>Password</label>
        <br />
        <input
          type="password"
          placeholder="Contraseña"
          id="userpassword"
          value={userpassword}
          onChange={(e) => {
            setPassword(e.target.value);
          }}
        ></input> <p>La contraseña debe tener porlomenos 12 caracteres, incluir minuscula y un numero</p>
        <br />
        <button type="submit">Log Me</button>
      </form>
    </>
  );
}
export default Register;
