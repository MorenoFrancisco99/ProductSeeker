import { Link } from "react-router-dom";

function Home(){

    return (
        <div>
            
            <h1>My Home</h1>
            <Link to= '/Login'>Logear </Link>
            <Link to= '/Register'>Registrar</Link>
            <Link to= '/UserHome'>Test JWT</Link>

        </div>
    )
}
export default Home;