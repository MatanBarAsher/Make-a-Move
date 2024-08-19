import {
  FCFeedback,
  FCFeedbackContinue,
  FCSignUp,
  FCUpdateProfile,
} from "../screens";
import FCLocation from "../screens/FCLocation";
import FCMap from "../screens/FCMap";
import { FCPrecerences } from "../screens/Preferences/components/FCPreferences";
import FCProfileView from "../screens/FCProfileView";
import FCSetImages from "../screens/FCSetImages";
import FCSignIn from "../screens/SignIn/FCSign-in";
import FCWellcome from "../screens/FCWellcome";
import FCSideMenu from "../screens/FCSideMenu";
import FCCities from "../screens/FCCities";
import { FCLoad } from "../loading/FCLoad";
import { FCRecommendations } from "../screens/FCRecommendations";
import FCMatchList from "../screens/FCMatches/components/FCMatchList";
import FCMyProfile from "../screens/FCMyProfile/components/FCMyProfile";
import FCCarousel from "../screens/FCCarousel";
import { FCHome } from "../screens/FCHome";
import { FCUpdatePreferences } from "../screens/Preferences/components/FCUpdatePreferences";

export const ROUTER = [
  { path: "/", Element: FCWellcome },
  { path: "/signin", Element: FCSignIn },
  { path: "/signup", Element: FCSignUp },
  { path: "/setImages", Element: FCSetImages },
  { path: "/preferences", Element: FCPrecerences },
  { path: "/profile", Element: FCProfileView },
  { path: "/location", Element: FCLocation },
  { path: "/map", Element: FCMap },
  { path: "/sideMenu", Element: FCSideMenu },
  { path: "/cities", Element: FCCities },
  { path: "/loading", Element: FCLoad },
  { path: "/feedback", Element: FCFeedback },
  { path: "/feedbackContinue", Element: FCFeedbackContinue },
  { path: "/myProfile", Element: FCMyProfile },
  { path: "/recommendations", Element: FCRecommendations },
  { path: "/matches", Element: FCMatchList },
  { path: "/updateProfile", Element: FCUpdateProfile },
  { path: "/carousel", Element: FCCarousel },
  { path: "/updatePreferences", Element: FCUpdatePreferences },
  { path: "/home", Element: FCHome },
];
