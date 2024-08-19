import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import FCCustomTxtInp from "../../../components/FCCustomTxtInp";
import { PERSONAL_INTERESTS } from "../../../constants";
import { FCMultiSelect } from "../../../components/MultiSelect";
import FCCustomBtn from "../../../components/FCCustomBtn";
import { makeAmoveUserServer } from "../../../services";
import { ErrorDialog, SuccessDialog } from "../Components";
import { useSignUpContext } from "../SignUpContext";
import { FCLoad } from "../../../loading/FCLoad";

export const FCSignUp3 = ({ setCurrentStep, currentStep, length }) => {
  const navigate = useNavigate("");
  const [showSuccessModal, setShowSuccessModal] = useState(false); // State to manage modal visibility
  const [showErrorModal, setShowErrorModal] = useState(false); // State to manage modal visibility
  const [isLoading, setIsLoading] = useState(false);
  const { signUpData, updateSignUpData } = useSignUpContext();
  const [personalInterstsOptions, setPersonalInterstsOptions] = useState([]);
  const [selectedInterests, setSelectedInterests] = useState([]);
  const [selectedInterestsIndexes, setSelectedInterestsIndexes] = useState([]);
  console.log(signUpData);

  localStorage.setItem("imageOrigin", JSON.stringify("signUp"));

  let tempArr3 = [];

  const handleDescriptionCreation = (e) => {
    updateSignUpData("description", e.target.value);
  };

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

  useEffect(() => {
    console.log(selectedInterestsIndexes);
  }, [selectedInterestsIndexes]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(selectedInterestsIndexes);

    setIsLoading(true);
    try {
      const response = await makeAmoveUserServer.createUser(signUpData);
      if (response) {
        handleInterestsSelection();
        localStorage.setItem("current-email", JSON.stringify(signUpData.email));
        setShowSuccessModal(true);
      } else {
        setShowErrorModal(true);
      }
    } catch (error) {
      console.error("Error signing up:", error);
      setShowErrorModal(true);
    } finally {
      setIsLoading(false); // Set loading to false after the API call completes
    }
  };

  useEffect(() => {
    makeAmoveUserServer
      .GetPersonalInterests()
      .then((res) => setPersonalInterstsOptions(res));
  }, []);

  const handleInterestsSelection = () => {
    console.log(selectedInterestsIndexes);
    console.log(signUpData.email);
    makeAmoveUserServer
      .postPersonalInterests(signUpData.email, selectedInterestsIndexes)
      .then((res) => console.log(res))
      .catch((res) => console.log(res));
    console.log(selectedInterests);
  };

  // useEffect(() => {
  //   makeAmoveUserServer
  //     .GetPersonalInterestsByEmail(signUpData.email)
  //     .then((res) => res);
  // }, []);

  // navigate("/setImages");

  return (
    <span>
      {isLoading && <FCLoad />}

      {!isLoading && (
        <>
          <SuccessDialog
            open={showSuccessModal}
            setClose={() => {
              setShowSuccessModal(false);
              navigate("/setImages");
            }}
          />
          <ErrorDialog
            open={showErrorModal}
            setClose={() => {
              setShowErrorModal(false);
              navigate("/");
            }}
          />

          <form>
            <h1>פרופיל</h1>
            <p className="signup2-p">מה את/ה אוהב/ת לעשות בזמנך הפנוי?</p>
            <FCMultiSelect
              label="תחומי עיניין"
              // options={PERSONAL_INTERESTS}
              options={personalInterstsOptions.map((o) => o.interestDesc)}
              onChange={handlePersonalInterestsIdsChange}
              value={[selectedInterests][0]}
            />

            <p className="signup2-p">
              ספר/י לנו קצת על עצמך:
              <span style={{ fontWeight: "200" }}>
                <br /> מה את/ה אוהב/ת לשתות?
                <br />
                איפה את/ה אוהב/ת לבלות?
                <br /> מעשנ/ת?
                <br /> אוהב/ת בע’’ח?
                <br /> וכל דבר נוסף...
              </span>
            </p>
            <FCCustomTxtInp
              className="description-inp"
              ph="כאן מספרים..."
              onChange={handleDescriptionCreation}
              required
              value={signUpData["description"]}
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
                onClick={handleSubmit}
                title={currentStep === length - 1 ? "סיום" : "הבא"}
                // type="submit"
              />
            </div>
          </form>
        </>
      )}
    </span>
  );
};
