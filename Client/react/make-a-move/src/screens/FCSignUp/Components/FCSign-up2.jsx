import React, { useState, useEffect } from "react";
import FCCustomTxtInp from "../../../components/FCCustomTxtInp";
import FCCustomDateInp from "../../../components/FCCustomDateInp";
import FCCustomNumberInp from "../../../components/FCCustomNumberInp";
import { FCSelect } from "../../../components/Select/FCSelect";
import FCCustomBtn from "../../../components/FCCustomBtn";
import { useSignUpContext } from "../SignUpContext";
import axios from "axios";

export const FCSignUp2 = ({ setCurrentStep, currentStep, length }) => {
  const { signUpData, updateSignUpData } = useSignUpContext();
  const [cityOptions, setCityOptions] = useState([]);
  const [filteredCities, setFilteredCities] = useState([]);
  const [cityMap, setCityMap] = useState({});

  const handleFirstNameCreation = (e) => {
    updateSignUpData("firstName", e.target.value);
  };
  const handleLastNameCreation = (e) => {
    updateSignUpData("lastName", e.target.value);
  };

  var genders = [
    { label: "גבר", id: 1 },
    { label: "אישה", id: 2 },
    { label: "אחר", id: 3 },
  ];
  const [gender, setGender] = useState(null);
  const handleGenderCreation = (id) => {
    setGender(id);
    updateSignUpData("gender", id);
  };
  const handleHeightCreation = (e) => {
    updateSignUpData("height", e.target.value);
  };

  const handleBirthdayCreation = (e) => {
    updateSignUpData("birthday", e.target.value);
  };

  const handleCityCreation = (citySymbol, cityName) => {
    updateSignUpData("city", `${citySymbol}`);
    console.log(citySymbol);
    document.getElementById("cityName").value = cityName;
    document.getElementById("myDropdown").classList.toggle("show");
    console.log(signUpData);
  };

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

  return (
    <>
      <form onSubmit={() => setCurrentStep((prev) => prev + 1)}>
        <h1>פרופיל</h1>
        <p className="signup2-p">שם פרטי:</p>
        <FCCustomTxtInp
          ph="שם פרטי"
          onChange={handleFirstNameCreation}
          required
          value={signUpData["firstName"]}
        />
        <p className="signup2-p">שם משפחה:</p>
        <FCCustomTxtInp
          ph="שם משפחה"
          onChange={handleLastNameCreation}
          required
          value={signUpData["lastName"]}
        />
        <div className="gender-inp">
          {genders.map((g) => (
            <span key={g.id}>
              <input
                checked={gender === g.id}
                id={"gender_" + g.id}
                type="radio"
                value={g.id}
                onClick={() => handleGenderCreation(g.id)}
              />
              <label htmlFor={"gender_" + g.id}>{g.label}</label>
            </span>
          ))}
        </div>
        <p className="signup2-p">מאיפה אתה?</p>
        <div className="dropdown">
          <input
            type="text"
            id="cityName"
            className="text-inp"
            placeholder="חפש..."
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
        <p className="signup2-p">תאריך לידה:</p>
        <FCCustomDateInp
          ph="dd/mm/yyyy"
          onChange={handleBirthdayCreation}
          value={signUpData["birthday"]}
          required
        />
        <p className="signup2-p">גובה (ס''מ):</p>
        <FCCustomNumberInp
          value={signUpData["height"]}
          min={0}
          ph="ס''מ"
          onChange={handleHeightCreation}
          required
        />
        <div
          style={{
            display: "flex",
            flexDirection: "row-reverse",
            justifyContent: "center",
            width: "25rem",
          }}
        >
          {currentStep !== 0 && (
            <FCCustomBtn
              style={{ width: "10rem", color: "black" }}
              onClick={() => setCurrentStep((prev) => prev - 1)}
              title={"הקודם"}
            />
          )}{" "}
          <FCCustomBtn
            style={{ width: "10rem", color: "black" }}
            onClick={() => setCurrentStep((prev) => prev + 1)}
            title={currentStep === length - 1 ? "סיום" : "הבא"}
            type="submit"
          />
        </div>
      </form>
    </>
  );
};
