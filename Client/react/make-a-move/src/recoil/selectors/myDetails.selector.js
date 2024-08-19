import { selector } from "recoil";

export const myDetailsState = selector({
  key: "myDetailsState",
  get: () => {
    const DETAILS_MOCK = {
      name: "מתן בר אשר",
      age: 26,
      city: "חרב לאת",
      height: 180,
      interests: "אופנועים, קפה, כדורגל",
      aboutMe: "פה אני מספר קצת על עצמי",
    };
    return DETAILS_MOCK;
  }, //get my details from service makeAmoveService.getMyDetails().then(( data ) => data )
});
