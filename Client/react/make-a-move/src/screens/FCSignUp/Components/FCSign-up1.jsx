import React, { useEffect, useState } from "react";
import logo from "../../../assets/images/Logo.png"; //"../../../../assets/images/Logo.png";
import FCCustomPhoneInp from "../../../components/FCCustomPhoneInp";
import FCCustomMailInp from "../../../components/FCCustomMailInp";
import FCCustomPasswordInp from "../../../components/FCCustomPasswordInp";
import { makeAmoveUserServer } from "../../../services";
import FCCustomBtn from "../../../components/FCCustomBtn";
import { useSignUpContext } from "../SignUpContext";

export const FCSignUp1 = ({ setCurrentStep, currentStep, length }) => {
  const { signUpData, updateSignUpData } = useSignUpContext();

  const [errors, setErrors] = useState([]);
  const [changedKey, setChangedKey] = useState(null);

  useEffect(() => {
    if (changedKey) {
      setErrors(errors.filter((error) => error !== changedKey));
      makeAmoveUserServer
        .checkExist({ key: changedKey, value: signUpData[changedKey] })
        .then((res) => {
          if (res) setErrors((prev) => [...prev, changedKey]);
          setChangedKey(null);
        });
    }
  }, [signUpData, changedKey]);

  const handlePhoneCreation = (e) => {
    updateSignUpData("phoneNumber", e.target.value);
    setChangedKey("phoneNumber");
  };

  const handleEmailCreation = (e) => {
    updateSignUpData("email", e.target.value);
    setChangedKey("email");
  };

  const handlePasswordCreation = (e) => {
    updateSignUpData("password", e.target.value);
    setChangedKey("password");
  };

  const validateForm = () => {
    const newErrors = [];

    // Validate phone number
    const phoneNumber = signUpData["phoneNumber"];
    if (!/^\d{10}$/.test(phoneNumber)) {
      newErrors.push("phoneNumber");
    }

    // Validate password
    const password = signUpData["password"];
    if (password.length < 8) {
      newErrors.push("password");
    }

    setErrors(newErrors);
    return newErrors.length === 0;
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (validateForm()) {
      setCurrentStep((prev) => prev + 1);
    }
  };

  return (
    <>
      <form onSubmit={handleSubmit}>
        <img src={"." + logo} className="logoSM" alt="Logo" />
        <h1>הרשמה</h1>
        <p className="signup-p">אפשר לקבל את הטלפון שלך?</p>
        <FCCustomPhoneInp
          value={signUpData["phoneNumber"]}
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
        <p className="signup-p">אפשר לקבל את המייל שלך?</p>
        <FCCustomMailInp
          ph={"דוא''ל"}
          value={signUpData["email"]}
          error={!!errors.find((error) => error === "email")}
          onChange={handleEmailCreation}
          required
        />
        <p className="signup-p">סיסמה</p>
        <FCCustomPasswordInp
          ph={"סיסמה"}
          value={signUpData["password"]}
          error={!!errors.find((error) => error === "password")}
          onChange={handlePasswordCreation}
          required
        />
        {errors.includes("password") && (
          <p className="error-message">
            * הסיסמה חייבת להיות באורך של לפחות 8 תווים
          </p>
        )}
        <div
          style={{
            display: "flex",
            flexDirection: "row-reverse",
            justifyContent: "center",
            width: "25rem",
          }}
        >
          <FCCustomBtn
            style={{ width: "10rem", color: "black" }}
            title={currentStep === length - 1 ? "סיום" : "הבא"}
            type="submit"
          />
          {currentStep !== 0 ? (
            <FCCustomBtn
              style={{ width: "10rem", color: "black" }}
              onClick={() => setCurrentStep((prev) => prev - 1)}
              title={"הקודם"}
            />
          ) : (
            <span />
          )}
        </div>
      </form>
    </>
  );
};
