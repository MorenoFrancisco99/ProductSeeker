import { Link, Routes, Route } from "react-router-dom";
import DashBoard from "../../Components/DashBoard/DashBoard";
import { UserDataProvider } from "../../Provider/UserDataProvider";
import { BrowserRouter } from "react-router-dom";
function UserHome() {
  return (
    <>
        <DashBoard></DashBoard>
    </>
  );
}
export default UserHome;
