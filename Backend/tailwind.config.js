/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./FrontEnd/**/*.{html,js}"],
  theme: {
    extend: {
      height:{
        '11\/12': '91.666667%'
      },
      colors:{
        white: "#ffff",
        azul: "#5378E0",
        gris: "#D7D9DE",
        grisOscuro: "#cbcdd1"
      }
    },
  },
  plugins: [],
}