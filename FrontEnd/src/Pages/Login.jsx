import { useState } from "react";
import { LoginUser } from "../api/ApiServices";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../Provider/AuthProvider";

function Login() {
  const [username, setUsername] = useState("");
  const [userpassword, setPassword] = useState("");
  const [errorMsg, setErrorMsg] = useState("");
  const navigate = useNavigate();
  const { setToken } = useAuth();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMsg(""); // limpiamos error anterior

    
    try {
      const response = await LoginUser(username.trim(), userpassword.trim());

      const token = response.data.token;
      setToken(token);


      navigate("/"); // Redirige a Home
      setErrorMsg("Registro exitoson, usuario:" + username)
    } catch (error) {
      // Errores del servidor
      if (error.response) {
        if (error.response.status === 401) {
          setErrorMsg("Contraseña incorrecta.");
        } else if (error.response.status === 404) {
          setErrorMsg("Usuario no encontrado.");
        } else {
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
      <h1>Iniciar sesión</h1>
      {errorMsg && <p style={{ color: "red" }}>{errorMsg}</p>}

      <form onSubmit={handleSubmit}>
        <label htmlFor="username">Nombre de usuario</label>
        <input
          type="text"
          id="username"
          placeholder="Usuario"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required/> comodoro2012<br />

        <label htmlFor="userpassword">Contraseña</label>
        <input
          type="password"
          id="userpassword"
          placeholder="Contraseña"
          value={userpassword}
          onChange={(e) => setPassword(e.target.value)}
          required/>asdasd123123<br />

        <button type="submit">Iniciar sesión</button>
      </form>
    </>
  );
}

export default Login;
