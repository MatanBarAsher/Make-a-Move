import React, { useEffect, useState } from "react";
import FCHamburger from "../components/FCHamburger";
import locationPin from "../assets/images/locationPin1.png";
import WomanIcon from "@mui/icons-material/Woman";
import ManIcon from "@mui/icons-material/Man";
import WcIcon from "@mui/icons-material/Wc";
import FCCarousel from "./FCCarousel";
import { useNavigate } from "react-router-dom";
import { makeAmoveUserServer } from "../services";
import { border } from "@mui/system";
import { FCLoad } from "../loading/FCLoad";

export default function FCMap({ location }) {
  const [users, setUsers] = useState([]);
  const navigate = useNavigate();

  const userEmail = JSON.parse(localStorage.getItem("current-email"));
  const currentPlace = JSON.parse(localStorage.getItem("current-place"));
  localStorage.setItem("origin", JSON.stringify("Map"));
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    const fetchUserDetails = async () => {
      setIsLoading(true);
      if (userEmail) {
        try {
          const userEmails = await makeAmoveUserServer.readUsersByPreference(
            userEmail
          );
          const userDetails = await Promise.all(
            Object.keys(userEmails).map(async (email) => {
              const user = await makeAmoveUserServer.GetUserNoPasswordByEmail(
                email
              );
              const percentage = userEmails[email]["item1"];
              return { ...user, percentage };
            })
          );
          setUsers(
            userDetails.filter(
              (u) => u.email !== userEmail && +u.currentPlace === +currentPlace
            )
          );
        } catch (error) {
          console.error("Error retrieving user details:", error);
        } finally {
          setIsLoading(false); // Set loading to false after the API call completes
        }
      } else {
        console.error("User email not found in localStorage");
      }
    };

    fetchUserDetails();
  }, []);

  // const showUserDetails = (user) => {
  //   console.log("User details:", user);
  //   localStorage.setItem("user-to-show", JSON.stringify(user));
  //   navigate("/profile");
  // };

  return (
    <span>
      {isLoading && <FCLoad />}

      {!isLoading && (
        <>
          <div className="map-container">
            <FCHamburger />
            <div className="icon-container">
              {users.length > 0 ? (
                <FCCarousel users={users} />
              ) : (
                <h3 style={{ marginTop: 200 }}>אין משתמשים זמינים כרגע</h3>
              )}
            </div>
          </div>
        </>
      )}
    </span>
  );
}
