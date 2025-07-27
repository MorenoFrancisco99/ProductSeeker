import AuthProvider from "./Provider/AuthProvider"
import Routes from "./Routes/indes"
import Home from "./Pages/Home"
import { Route } from "react-router-dom"
import UserCollection from "./Pages/ProtectedPages/UserCollection"

function App() {

  return (
    <AuthProvider>
      <Routes>
      </Routes>
    </AuthProvider>
  )
};

export default App
