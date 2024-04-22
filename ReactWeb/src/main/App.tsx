import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import Header from "./Header";
import HouseList from "../house/HouseList";
import HouseAdd from "../house/HouseAdd";
import HouseEdit from "../house/HouseEdit";
import HouseDetail from "../house/HouseDetail";

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <Header subtitle="Creating houses all over the world" />
        <Routes>
          <Route path="/" element={<HouseList />}></Route>
          <Route path="/house/add" element={<HouseAdd />}></Route>
          <Route path="/house/edit/:id" element={<HouseEdit />}></Route>
          <Route path="/house/:id" element={<HouseDetail />}></Route>
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
