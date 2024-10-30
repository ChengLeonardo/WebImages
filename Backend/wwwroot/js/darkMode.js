const sol = document.getElementById('sun');
const luna = document.getElementById('mooon');

let mov_realizado = true;


sol.addEventListener('mouseover', () => {
    luna.style.fill = "#163a9e";
})

sol.addEventListener('mouseout', () => {
    luna.style.fill = "#D7D9DE"; // O el color original
});

function movimiento() {
  sol.style.transform = 'rotate(180deg)';
  sol.style.opacity = '0';
  luna.style.opacity = '1';
  luna.style.transform = 'rotate(320deg)';
  mov_realizado = false;
}

function movimiento_inverso() {
  sol.style.transform = 'rotate(-180deg)';
  sol.style.opacity = '1';
  luna.style.opacity = '0';
  luna.style.transform = 'rotate(-200deg)';
  mov_realizado = true;
}


if (sol) {
    sol.addEventListener('click', function() {
      console.log("click object");
      if (mov_realizado) {
        movimiento();
        document.documentElement.classList.toggle('dark');
      } else {
        movimiento_inverso();
        document.documentElement.classList.remove('dark');
      }
    });
  }