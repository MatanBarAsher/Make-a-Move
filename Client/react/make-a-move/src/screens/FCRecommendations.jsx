import React, { useEffect, useState } from "react";
import FCCustomX from "../components/FCCustomX";
import { Navigate, useNavigate } from "react-router";
import { makeAmoveUserServer } from "../services";
import { FCLoad } from "../loading/FCLoad";

export const FCRecommendations = () => {
  const Navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const [dataToShow, setDataToShow] = useState({});

  useEffect(() => {
    const fetchUserDetails = async () => {
      let userEmail = JSON.parse(localStorage.getItem("current-email"));
      setIsLoading(true);
      if (userEmail) {
        try {
          const recommend = await makeAmoveUserServer.getAnalysis(userEmail);
          // makeAmoveUserServer
          //   .getAnalysis(userEmail)
          //   .then((res) => console.log(res));
          // const userDetails = await Promise.all(
          //   Object.keys(userEmails).map(async (email) => {
          //     const user = await makeAmoveUserServer.GetUserNoPasswordByEmail(
          //       email
          //     );

          // })
          // );
          // console.log(userDetails);
          // console.log(recommend);

          setDataToShow(recommend);
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

  useEffect(() => {
    console.log(dataToShow);
  }, [dataToShow]);

  return (
    <span>
      {isLoading && <FCLoad />}

      {!isLoading && (
        <>
          <div onClick={() => Navigate("/sideMenu")}>
            <FCCustomX onclick="" color="white" />
            <div>
              <h1 style={{ flex: "100%" }}>המלצות אישיות</h1>
              <h3 className="recommend-h">
                על סמך המשובים שלך יש לנו כמה המלצות בשבילך:
              </h3>
            </div>
            <div className="lower-recommend">
              <h3 className="recommend-h3">
                1. ב{" "}
                {dataToShow.PlacePrecentage ? dataToShow.PlacePrecentage : ""}%
                מהפעמים ההתאמות שלך היו ב
                {dataToShow.Place ? dataToShow.Place : ""}
              </h3>
              <h3 className="recommend-h3">
                2. ב{" "}
                {dataToShow.NamePrecentage ? dataToShow.NamePrecentage : ""}%{" "}
                הגעת לשם עם {dataToShow.Name ? dataToShow.Name : ""}
              </h3>
              <h3 className="recommend-h3">
                3. ב{dataToShow.NonMatchingAgePercentage}% מהמקרים בהם המשכת
                למפגש נוסף גיל המשתמש היה שונה משהגדרת
              </h3>

              <h3 className="recommend-h3">
                4.{" "}
                {dataToShow.HighestInterestPercentage
                  ? dataToShow.HighestInterestPercentage
                  : ""}
                % מהמשתמשים שהמשכת איתם למפגש נוסף סימנו בתחומי עניין שלהם:{" "}
                {dataToShow.PersonalInterests
                  ? dataToShow.PersonalInterests
                  : ""}
              </h3>
            </div>
          </div>
        </>
      )}
    </span>
  );
};
