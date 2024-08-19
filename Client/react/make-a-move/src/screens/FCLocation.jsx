import React, { useState, useEffect } from "react";
import FCCustomBtn from "../components/FCCustomBtn";
import logo from "../assets/images/Logo.png";
import FCHamburger from "../components/FCHamburger";
import GooglePlacesAutocomplete from "react-google-places-autocomplete";
import { makeAmoveUserServer } from "../services";
import { useNavigate } from "react-router-dom";
import { FCLoad } from "../loading/FCLoad";
import FCCustomX from "../components/FCCustomX";

const FCLocation = () => {
  const [value, setValue] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const userEmail = JSON.parse(localStorage.getItem("current-email"));
  const navigate = useNavigate();
  const [UserData, setUserData] = useState({});
  localStorage.setItem("origin", JSON.stringify("Location"));

  useEffect(() => {
    makeAmoveUserServer
      .getUserByEmail(userEmail)
      .then((res) => {
        console.log(res);
        setUserData(res);
      })
      .catch((res) => console.log(res));
    console.log(UserData);
  }, []);

  const handleLocationChange = (e) => {
    const tempDetails = e.value.description.split(",");
    let tempString = "";
    tempDetails.forEach((element) => {
      if (tempDetails.indexOf(element) > 0) {
        tempString += element + ", ";
      }
    });
    const placeToAdd = {
      name: tempDetails[0],
      address: tempString,
    };
    console.log(placeToAdd);
    setValue(placeToAdd);
    console.log(e.value.description);
  };

  const handleSubmit = async (e) => {
    // e.preventDefault();
    console.log(userEmail);
    console.log(value);
    setIsLoading(true); // Set loading to true before making the API call
    try {
      //go to server with locationValue as prop
      const response = await makeAmoveUserServer.setLocationValue(
        value,
        userEmail
      );
      if (response) {
        updateTimeStamp();
        console.log("success");
        console.log(response.data);
        makeAmoveUserServer
          .updateUser(response.data)
          .then((res) => console.log(res));
        localStorage.setItem(
          "current-place",
          response.data.currentPlace
          // JSON.stringify({
          // placeCode: response.data.currentPlace,
          // placeName: value,
          // })
        );
        console.log(localStorage.getItem("current-place"));
        navigate("/map");
      } else {
        console.log("failure");
      }
      // } catch (error) {
      //   console.error("Error logging in:", error);
      //   setShowErrorModal(true);
    } finally {
      setIsLoading(false); // Set loading to false after the API call completes
    }
  };

  const updateTimeStamp = () => {
    let logoutUser = UserData;
    logoutUser.currentPlace = 0;
    const date = new Date();
    const formattedDate = date.toISOString().split(".")[0];
    console.log(formattedDate);
    logoutUser.timeStamp = formattedDate;
    console.log(logoutUser);
    makeAmoveUserServer
      .updateUser(logoutUser)
      .then((res) => console.log(res))
      .catch((res) => console.log(res));
  };

  return (
    <span>
      {isLoading && <FCLoad />}

      {!isLoading && ( // Render the form and other content only if isLoading is false
        <>
          {/* <FCHamburger /> */}
          <span onClick={() => navigate("/home")}>
            <FCCustomX />
          </span>
          <img src={"." + logo} className="logoSM" />
          <h1>אישור מיקום:</h1>
          <div>
            <GooglePlacesAutocomplete
              selectProps={{
                onChange: handleLocationChange,
              }}
              apiKey={"AIzaSyDfO7S5c0OcZki3aQEBN1xMrDj4qD9v8Uk"}
              debounce={300}
            />
          </div>
          <FCCustomBtn
            title="אישור"
            mt={"130px"}
            onClick={() => handleSubmit(value)}
          />
        </>
      )}
    </span>
  );
};
export default FCLocation;
