import React, { useState, useEffect } from "react";
import { json, useNavigate } from "react-router-dom";
import FCCustomNumberInp from "../../../components/FCCustomNumberInp";
import FCCustomBtn from "../../../components/FCCustomBtn";
import { Slider } from "@mui/material";
import { makeAmoveUserServer } from "../../../services";
import { makeAmoveUserPreferencesServer } from "../../../services";
import { PreferenceErrorDialog } from "./Dialog/PreferenceErrorDialog";
import { PreferenceSuccessDialog } from "./Dialog/PreferenceSuccessDialog";
import { FCLoad } from "../../../loading/FCLoad";
import FCCustomX from "../../../components/FCCustomX";

export const FCUpdatePreferences = () => {
  const navigate = useNavigate("");
  const [showSuccessModal, setShowSuccessModal] = useState(false); // State to manage modal visibility
  const [showErrorModal, setShowErrorModal] = useState(false); // State to manage modal visibility
  const [isLoading, setIsLoading] = useState(false);
  const email = JSON.parse(localStorage.getItem("current-email"));
  console.log(email);

  const [preferencesData, setPreferencesData] = useState({
    email: email,
    preferenceGender: 0,
    maxDistance: 0,
    ageRange: [18, 80],
    heightRange: [120, 250],
  });
  console.log(preferencesData);

  useEffect(() => {
    const getPreferencesData = async () => {
      try {
        const res =
          await makeAmoveUserPreferencesServer.ReadUserPreferencesByEmail(
            email
          );
        const tempData = {
          email: res.email,
          preferenceGender: res.preferenceGender,
          maxDistance: res.maxDistance,
          ageRange: [res.minAge, res.maxAge],
          heightRange: [res.minHeight, res.maxHeight],
        };
        console.log(tempData);
        setPreferencesData(tempData);
      } catch (error) {
        console.error("Failed to fetch user preferences", error);
      }
    };

    getPreferencesData();
  }, [email]);

  const handleGenderCreation = (e) => {
    setPreferencesData((prev) => ({
      ...prev,
      ["preferenceGender"]: parseInt(e.target.id),
    }));
  };
  const handleDistanceCreation = (e) => {
    setPreferencesData((prev) => ({
      ...prev,
      ["maxDistance"]: e.target.value,
    }));
  };

  const handleAgeRangeChange = (event, newValue) => {
    setPreferencesData((prev) => ({ ...prev, ["ageRange"]: newValue }));
  };
  const handleHeightRangeChange = (event, newValue) => {
    setPreferencesData((prev) => ({ ...prev, ["heightRange"]: newValue }));
  };

  const handleInterestsCreation = (event) => {
    const {
      target: { value },
    } = event || {};
    setPreferencesData((prev) => ({
      ...prev,
      ["preferedInterests"]:
        typeof value === "string" ? value.split(",") : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    const tempData = {
      email: preferencesData.email,
      preferenceGender: preferencesData.preferenceGender,
      minAge: preferencesData.ageRange[0],
      maxAge: preferencesData.ageRange[1],
      minHeight: preferencesData.heightRange[0],
      maxHeight: preferencesData.heightRange[1],
      maxDistance: preferencesData.maxDistance,
    };
    try {
      //go to server with preferencesData as prop
      const response = await makeAmoveUserPreferencesServer
        .UpdateUserPreference(tempData)
        .then((res) => res);
      if (response) {
        setShowSuccessModal(true);
        console.log("success");
        console.log(response);
        console.log(tempData);
        // navigate("/sideMenu");
      } else {
        console.log(tempData);
        console.log("failure");
        console.log(response);
        setShowErrorModal(true);
      }
    } catch (error) {
      console.error("Error:", error);
      setShowErrorModal(true);
    } finally {
      setIsLoading(false); // Set loading to false after the API call completes
    }
  };
  return (
    <span>
      {isLoading && <FCLoad />}

      {!isLoading && (
        <>
          <PreferenceSuccessDialog
            open={showSuccessModal}
            setClose={() => {
              setShowSuccessModal(false);
              navigate("/sideMenu");
            }}
          />
          <PreferenceErrorDialog
            open={showErrorModal}
            setClose={() => {
              setShowErrorModal(false);
            }}
          />
          <span onClick={() => navigate("/sideMenu")}>
            <FCCustomX color="white" />
          </span>
          <form onSubmit={handleSubmit}>
            <h1 className="pref-h1">העדפות</h1>
            <p className="preference-p">אני מחפש/ת:</p>
            <div className="gender-inp">
              <span>
                <input
                  checked={preferencesData["preferenceGender"] === 1}
                  onChecked={preferencesData["preferenceGender"] === 1}
                  id="1"
                  type="radio"
                  name="preferenceGender"
                  onChange={handleGenderCreation}
                  required
                />
                <label htmlFor="male">גבר</label>
              </span>
              <span>
                <input
                  checked={preferencesData["preferenceGender"] === 2}
                  onChecked={preferencesData["preferenceGender"] === 2}
                  id="2"
                  type="radio"
                  name="preferenceGender"
                  onChange={handleGenderCreation}
                  required
                />
                <label htmlFor="female">אישה</label>
              </span>
              <span>
                <input
                  checked={preferencesData["preferenceGender"] === 3}
                  onChecked={preferencesData["preferenceGender"] === 3}
                  id="3"
                  type="radio"
                  name="preferenceGender"
                  onChange={handleGenderCreation}
                  required
                />
                <label htmlFor="other">פתוח להצעות</label>
              </span>
            </div>

            <p className="preference-p">מרחק מקסימלי (מאיפה שאני גר)</p>
            <FCCustomNumberInp
              value={preferencesData["maxDistance"]}
              ph="ק''מ"
              min={0}
              onChange={handleDistanceCreation}
              required
            />
            <p className="preference-p">בגיל:</p>
            <span className="range">
              <Slider
                sx={{ color: "#efe1d1" }}
                getAriaLabel={() => "Temperature range"}
                value={preferencesData["ageRange"]}
                onChange={handleAgeRangeChange}
                valueLabelDisplay="on"
                className="slider"
                min={18}
                max={80}
              />
            </span>
            <p className="preference-p">בגובה:</p>
            <span className="range">
              <Slider
                sx={{ color: "#efe1d1" }}
                getAriaLabel={() => "Temperature range"}
                value={preferencesData["heightRange"]}
                onChange={handleHeightRangeChange}
                valueLabelDisplay="on"
                className="slider"
                min={120}
                max={250}
              />
            </span>
            <div
              style={{
                display: "flex",
                flexDirection: "row-reverse",
                justifyContent: "center",
                width: "25rem",
              }}
            >
              <FCCustomBtn
                style={{ width: "15rem", color: "black", margin: "30px 0" }}
                title={"סיום"}
                type="submit"
              />
            </div>
          </form>
        </>
      )}
    </span>
  );
};
