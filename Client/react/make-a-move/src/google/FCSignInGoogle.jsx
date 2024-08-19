import React from "react";
import { useEffect } from "react";
import { jwtDecode } from "jwt-decode";
import { makeAmoveUserServer } from "../services";

export default function FCSignInGoogle() {
  useEffect(() => {
    // global google
    google.accounts.id.initialize({
      client_id:
        "1077213519683-6j4bu7u6i3frlj7192npaftn8eju03ev.apps.googleusercontent.com",
      callback: handleCallbackResponse,
    });

    google.accounts.id.renderButton(document.getElementById("signInDiv"), {
      theme: "outline",
      size: "large",
    });
  }, []);

  const handleCallbackResponse = (response) => {
    console.log("Encoded JWT ID token " + response.credential);
    const userObj = jwtDecode(response.credential);
    console.log(userObj);
    loginGoogle(userObj.email);
  };

  const loginGoogle = (email) => {
    makeAmoveUserServer
      .getAllUsersEmails()
      .then((emails) => {
        console.log(emails);

        const lowerCaseEmails = emails.map((email) => email.toLowerCase());
        console.log(lowerCaseEmails);

        const stringSet = new Set(lowerCaseEmails);
        console.log(stringSet);

        if (stringSet.has(email.toLowerCase())) {
          localStorage.setItem("current-email", JSON.stringify(email));
          navigate("/location");
        } else {
          console.log("user does'nt exists");
        }
      })
      .catch((error) => {
        console.error("Error processing emails:", error);
      });
  };
  return <div id="signInDiv" className="google-signin-btn"></div>;
}
