import "./App.css";
import {
  HashRouter,
  Route,
  BrowserRouter as Router,
  Routes,
} from "react-router-dom";
import { RecoilRoot } from "recoil";
import { ROUTER } from "./Routs";

import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { makeAmoveMatchServer } from "./services";
import { useEffect } from "react";

function App() {
  useEffect(() => {
    checkMatched();
  }, []);

  const notify = () =>
    toast(`יש לנו Match! אפשר לראות אותו עכשיו במסך ההתאמות`, {
      position: "top-center",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: false,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "dark",
      onClick: console.log("click"),
    });

  const checkMatched = async () => {
    const currentNumOfMatches = localStorage.getItem("number-of-matches");
    try {
      const res = await makeAmoveMatchServer.getMatchesByEmail(
        JSON.parse(localStorage.getItem("current-email"))
      );
      console.log(res);
      console.log(currentNumOfMatches);
      if (res.length > currentNumOfMatches) {
        localStorage.setItem("number-of-matches", res.length);
        notify();
      }
    } catch (error) {
      console.error("Error fetching matches:", error);
    }
  };

  return (
    <RecoilRoot>
      <HashRouter>
        <Routes>
          {ROUTER.map(({ path, Element }) => (
            <Route path={path} element={<Element />} key={path} />
          ))}
        </Routes>
      </HashRouter>
      <ToastContainer
        position="top-center"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick={false}
        rtl
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
        transition:Slide
      />
      {/* <button onClick={notify}>Notify!</button> */}
    </RecoilRoot>
  );
}

export default App;
