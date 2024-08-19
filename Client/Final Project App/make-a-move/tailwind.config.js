/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./app/**/*.{js,jsx,ts,tsx}", "/.components/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors: {
        primary: "#3C0753",
        purple1: "#5D3587",
        purple2: "#3F2E3E",
        purple3: "#331D2C",
        gray1: "#D9D9D9",
        gray2: "#989898",
        white: "#fff",
        bgColor: "rgb(93,53,135)",
        bgGradient:
          "linear-gradient(0deg, rgba(93,53,135,1) 0%, rgba(63,46,62,1) 76%, rgba(51,29,44,1) 100%)",
      },

      fontFamily: {
        main: ["Heebo", "sans-serif"],
        secondary: ["Inter", "sans-serif"],
      },
    },
  },
  plugins: [],
};
