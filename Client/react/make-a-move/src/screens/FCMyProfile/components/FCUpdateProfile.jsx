import React, { useCallback, useEffect, useState } from "react";
import { useNavigate } from "react-router";
import FCCustomX from "../../../components/FCCustomX";
import FCCustomPhoneInp from "../../../components/FCCustomPhoneInp";
import FCCustomMailInp from "../../../components/FCCustomMailInp";
import FCCustomPasswordInp from "../../../components/FCCustomPasswordInp";
import FCCustomBtn from "../../../components/FCCustomBtn";
import FCCustomNumberInp from "../../../components/FCCustomNumberInp";
import FCCustomDateInp from "../../../components/FCCustomDateInp";
import FCCustomTxtInp from "../../../components/FCCustomTxtInp";
import axios from "axios";
import { FCMultiSelect } from "../../../components";
import { useAsync } from "../../../hooks";
import { makeAmoveUserServer } from "../../../services";
import { ProfileErrorDialog } from "../components/DialogsProfiles/profileErrorDialog";
import { ProfileSuccessDialog } from "../components/DialogsProfiles/profileSuccessDialog";
import { FCLoad } from "../../../loading/FCLoad";

export const FCUpdateProfile = () => {
  const [showSuccessModal, setShowSuccessModal] = useState(false); // State to manage modal visibility
  const [showErrorModal, setShowErrorModal] = useState(false); // State to manage modal visibility
  const [isLoading, setIsLoading] = useState(false);
  const Navigate = useNavigate();
  const [errors, setErrors] = useState([]);
  const [changedKey, setChangedKey] = useState(null);
  const [updatedUserData, setUpdatedUserData] = useState({});
  const [cityOptions, setCityOptions] = useState([]);
  const [filteredCities, setFilteredCities] = useState([]);
  const [cityMap, setCityMap] = useState({});
  const [personalInterstsOptions, setPersonalInterstsOptions] = useState([]);
  const [selectedInterests, setSelectedInterests] = useState([]);
  const [selectedInterestsIndexes, setSelectedInterestsIndexes] = useState([]);

  const userEmail = JSON.parse(localStorage.getItem("current-email"));
  // console.log(userEmail);
  // console.log(updatedUserData);
  // console.log(personalInterstsOptions);

  const getUserFunc = useCallback(async () =>
    makeAmoveUserServer
      .getUserByEmail(userEmail)
      .then((res) => {
        console.log(res);
        res.birthday = res.birthday.split("T")[0];
        // console.log(res);
        setUpdatedUserData(res);
        setGender(res.gender);
      })
      .catch((res) => console.log(res))
  );

  // const getInterestsFunc = useCallback(async () =>
  //   makeAmoveUserServer.GetPersonalInterestsByEmail(userEmail).then((res) => {
  //     setSelectedInterestsIndexes(res);
  //     setSelectedInterests(getInterestDescByCodes(res));
  //     console.log(selectedInterestsIndexes);
  //     console.log(selectedInterests);
  //   })
  // );

  useEffect(() => {
    getUserFunc();

    // getInterestsFunc();

    makeAmoveUserServer
      .GetPersonalInterests()
      .then((res) => setPersonalInterstsOptions(res));

    setSelectedInterests(getInterestDescByCodes(selectedInterestsIndexes));
    // console.log(selectedInterestsIndexes);
    // console.log(selectedInterests);
  }, []);

  useEffect(() => {
    makeAmoveUserServer.GetPersonalInterestsByEmail(userEmail).then((res) => {
      setSelectedInterestsIndexes(res);
      setSelectedInterests(getInterestDescByCodes(res));
      console.log(selectedInterestsIndexes);
      console.log(selectedInterests);
    });
  }, [updatedUserData]);

  var genders = [
    { label: "גבר", id: 1 },
    { label: "אישה", id: 2 },
    { label: "אחר", id: 3 },
  ];
  const [gender, setGender] = useState(null);
  const handleGenderCreation = (id) => {
    setGender(id);
    changeUpdatedUserData("gender", id);
  };
  // makeAmoveUserServer
  //       .getUserByEmail(userEmail)
  //       .then((res) => {
  //         console.log(res);
  //         setUpdatedUserData(res);
  //       })
  //       .catch((res) => console.log(res));

  const changeUpdatedUserData = (key, value) =>
    setUpdatedUserData((prev) => ({ ...prev, [key]: value }));

  // const userData = useAsync(
  //   getUserFunc, // to select user data
  //   []
  // );
  // console.log(userData);

  // useEffect(() => setUpdateUserData(userData), [userData]); // to set user data

  //   useEffect(() => {
  //     if (changedKey) {
  //       setErrors(errors.filter((error) => error !== changedKey));
  //       makeAmoveUserServer
  //         .checkExist({ key: changedKey, value: updateUserData[changedKey] })
  //         .then((res) => {
  //           if (res) setErrors((prev) => [...prev, changedKey]);
  //           setChangedKey(null);
  //         });
  //     }
  //   }, [updateUserData, changedKey]);

  const handlePhoneCreation = (e) => {
    changeUpdatedUserData("phoneNumber", e.target.value);
    setChangedKey("phoneNumber");
  };

  const handleEmailCreation = (e) => {
    changeUpdatedUserData("email", e.target.value);
    setChangedKey("email");
  };

  const handlePasswordCreation = (e) => {
    changeUpdatedUserData("password", e.target.value);
    setChangedKey("password");
  };

  const handleFirstNameCreation = (e) => {
    changeUpdatedUserData("firstName", e.target.value);
  };
  const handleLastNameCreation = (e) => {
    changeUpdatedUserData("lastName", e.target.value);
  };

  const handleHeightCreation = (e) => {
    changeUpdatedUserData("height", e.target.value);
  };

  const handleBirthdayCreation = (e) => {
    changeUpdatedUserData("birthday", e.target.value);
  };

  const handleCityCreation = (citySymbol, cityName) => {
    changeUpdatedUserData("city", `${citySymbol}`);
    // console.log(citySymbol);
    document.getElementById("cityName").value = cityName;
    document.getElementById("myDropdown").classList.toggle("show");
  };
  const handleDescriptionCreation = (e) => {
    changeUpdatedUserData("persoalText", e.target.value);
  };

  //   const handlePersonalInterestsIdsChange = (event) => {
  //     const {
  //       target: { value },
  //     } = event || {};
  //     setUpdateUserData(
  //       "personalInterestsIds",
  //       typeof value === "string" ? value.split(",") : value
  //     );
  //   };

  const fetchCities = async () => {
    try {
      const response = await axios.get(
        "https://data.gov.il/api/3/action/datastore_search?resource_id=b282b438-0066-47c6-b11f-8277b3f5a0dc&limit=2000"
      );
      const citiesData = response.data.result.records;
      const cityMap = {};
      citiesData.forEach((city) => {
        cityMap[city["תיאור ישוב"]] = city["סמל ישוב"];
      });
      setCityMap(cityMap);
      setCityOptions(Object.keys(cityMap));
      // console.log(
      //   Object.entries(cityMap).find(([key, val]) => val === 103)?.[0]
      // );
      // console.log(cityMap);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  useEffect(() => {
    fetchCities();
  }, []);

  const filterCities = (e) => {
    const query = e.target.value;
    if (query.length > 1) {
      const filtered = cityOptions.filter((city) => city.includes(query));
      setFilteredCities(filtered);
    } else {
      setFilteredCities([]);
    }
  };

  const validateForm = () => {
    const newErrors = [];

    // Validate phone number
    const phoneNumber = updatedUserData["phoneNumber"];
    if (!/^\d{10}$/.test(phoneNumber)) {
      newErrors.push("phoneNumber");
    }

    // Validate password
    const password = updatedUserData["password"];
    if (password.length < 8) {
      newErrors.push("password");
    }

    setErrors(newErrors);
    return newErrors.length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      handleInterestsSelection();

      const response = await makeAmoveUserServer
        .updateUser(updatedUserData)
        .then((res) => res);
      console.log(response);
      if (response) {
        console.log(response);
        setShowSuccessModal(true);
        // Navigate("/MyProfile");
      } else {
        setShowErrorModal(true);
      }
    } catch (error) {
      console.error("Error updating profile", error);
      setShowErrorModal(true);
    } finally {
      setIsLoading(false); // Set loading to false after the API call completes
    }
  };

  let tempArr3 = [];

  const handlePersonalInterestsIdsChange = (event) => {
    console.log(event.target);
    const {
      target: { value },
    } = event || {};
    const tempArr = typeof value === "string" ? value.split(",") : value;
    const tempArr2 = [];
    tempArr.forEach((p) =>
      personalInterstsOptions.forEach((element) =>
        element.interestDesc === p ? tempArr2.push(element.interestCode) : 0
      )
    );
    tempArr3 = tempArr2.filter((item) => item > 0);
    setSelectedInterests([...tempArr]);
    setSelectedInterestsIndexes(tempArr3);
    console.log(tempArr);
    console.log(tempArr2);
    console.log(tempArr3);
    console.log(selectedInterests);
  };

  const handleInterestsSelection = () => {
    console.log(selectedInterestsIndexes);
    makeAmoveUserServer
      .UpdatePersonalInterestsByEmail(userEmail, selectedInterestsIndexes)
      .then((res) => console.log(res))
      .catch((res) => console.log(res));
    console.log(selectedInterests);
  };

  const getInterestDescByCodes = (codesArr) => {
    const temp = [];
    codesArr.forEach((index) => {
      personalInterstsOptions.forEach((option) => {
        if (option.interestCode === index) {
          temp.push(option.interestDesc);
        }
      });
    });
    return temp;
  };

  return (
    <span>
      <ProfileSuccessDialog />
      {isLoading && <FCLoad />}

      {!isLoading && (
        <>
          <ProfileSuccessDialog
            open={showSuccessModal}
            setClose={() => {
              setShowSuccessModal(false);
              Navigate("/myProfile");
            }}
          />
          <ProfileErrorDialog
            open={showErrorModal}
            setClose={() => {
              setShowErrorModal(false);
            }}
          />
          <div>
            <div>
              <span onClick={() => Navigate("/myProfile")}>
                <FCCustomX color="white" />
              </span>
              <h1>עריכת פרופיל</h1>
            </div>
            <form onSubmit={handleSubmit}>
              <p className="update-p">דוא"ל:</p>
              <FCCustomMailInp
                ph={"דוא''ל"}
                value={updatedUserData["email"]}
                error={!!errors.find((error) => error === "email")}
                onChange={handleEmailCreation}
                required
                disabled
              />
              <p className="update-p">טלפון:</p>
              <FCCustomPhoneInp
                value={updatedUserData["phoneNumber"]}
                ph={"מס' טלפון"}
                onChange={handlePhoneCreation}
                error={!!errors.find((error) => error === "phoneNumber")}
                required
              />
              {errors.includes("phoneNumber") && (
                <p className="error-message">
                  * מספר הטלפון חייב להיות באורך 10 ספרות
                </p>
              )}

              <p className="update-p">סיסמה:</p>
              <FCCustomPasswordInp
                ph={"סיסמה"}
                value={updatedUserData["password"]}
                error={!!errors.find((error) => error === "password")}
                onChange={handlePasswordCreation}
                required
              />
              {errors.includes("password") && (
                <p className="error-message">
                  * הסיסמה חייבת להיות באורך של לפחות 8 תווים
                </p>
              )}
              <p className="update-p">שם פרטי:</p>
              <FCCustomTxtInp
                ph="שם פרטי"
                onChange={handleFirstNameCreation}
                required
                value={updatedUserData["firstName"]}
              />
              <p className="update-p">שם משפחה:</p>
              <FCCustomTxtInp
                ph="שם משפחה"
                onChange={handleLastNameCreation}
                required
                value={updatedUserData["lastName"]}
              />
              <div className="gender-inp">
                {genders.map((g) => (
                  <span key={g.id}>
                    <input
                      checked={gender === g.id}
                      // checked={true}
                      id={"gender_" + g.id}
                      type="radio"
                      value={g.id}
                      onClick={() => handleGenderCreation(g.id)}
                    />
                    <label htmlFor={"gender_" + g.id}>{g.label}</label>
                  </span>
                ))}
              </div>
              <p className="update-p">מאיפה אתה?</p>
              <div className="dropdown">
                <input
                  type="text"
                  id="cityName"
                  className="text-inp"
                  value={
                    Object.entries(cityMap).find(
                      ([key, val]) => val === parseInt(updatedUserData.city)
                    )?.[0]
                  }
                  defaultValue={
                    Object.entries(cityMap).find(
                      ([key, val]) => val === updatedUserData.city
                    )?.[0]
                  }
                  onChange={filterCities}
                />
                {filteredCities.length > 0 && (
                  <div id="myDropdown" className="dropdown-content show">
                    {filteredCities.map((city, index) => (
                      <div
                        key={index}
                        onClick={() => handleCityCreation(cityMap[city], city)}
                        className="dropdown-item"
                      >
                        {city}
                      </div>
                    ))}
                  </div>
                )}
              </div>
              <p className="update-p">תאריך לידה:</p>
              <FCCustomDateInp
                ph="dd/mm/yyyy"
                onChange={handleBirthdayCreation}
                value={updatedUserData["birthday"]}
                required
              />
              <p className="update-p">גובה (ס''מ):</p>
              <FCCustomNumberInp
                value={updatedUserData["height"]}
                ph="ס''מ"
                min={0}
                onChange={handleHeightCreation}
                required
              />
              <p className="update-p">מה את/ה אוהב/ת לעשות בזמנך הפנוי?</p>
              <FCMultiSelect
                label="תחומי עיניין"
                options={personalInterstsOptions.map((o) => o.interestDesc)}
                onChange={handlePersonalInterestsIdsChange}
                value={[selectedInterests][0]}
              />
              <p className="update-p">
                ספר/י על עצמך:
                <span style={{ fontWeight: "200" }}></span>
              </p>
              <FCCustomTxtInp
                className="description-inp"
                ph="כאן מספרים..."
                onChange={handleDescriptionCreation}
                required
                value={updatedUserData["persoalText"]}
              />
              <div
                style={{
                  display: "flex",
                  flexDirection: "row-reverse",
                  justifyContent: "center",
                  width: "25rem",
                }}
              >
                <FCCustomBtn
                  style={{ width: "15rem", color: "black" }}
                  title={"סיום"}
                  type="submit"
                />
              </div>
            </form>
          </div>
        </>
      )}
    </span>
  );
};
