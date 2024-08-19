import React from "react";
import axios from "axios";

export default function FCCities() {
  let data = {};
  axios
    .get(
      "https://data.gov.il/api/3/action/datastore_search?resource_id=b282b438-0066-47c6-b11f-8277b3f5a0dc&limit=2000"
    )
    .then((response) => {
      console.log(response.data);
      // Handle response data
      data = response.data.result.records;
      console.log(data[0]["תיאור ישוב"]);
    })
    .catch((error) => {
      console.error("Error fetching data:", error);
      // Handle errors
    });

  return <div>FCCities</div>;
}
