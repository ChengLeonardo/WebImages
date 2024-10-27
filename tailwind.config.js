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
        grisOscuro: "#cbcdd1",
        azulOscuro: "#4160b4",
        Amarillo: "#ffce00",
        Verde: "#4b7f69",
        Cielo: "#a3e1ff",
        VerdeOscuro: "#4b9469"
      }
    },
  },
  plugins: [],
}