const sol = document.getElementById('sun');
const luna = document.getElementById('mooon');

let mov_realizado = true;

// Verificar el estado guardado al cargar la página
document.addEventListener('DOMContentLoaded', () => {
    const isDarkMode = localStorage.getItem('darkMode') === 'true';
    if (isDarkMode) {
        // Agregar clase para deshabilitar transiciones durante la carga
        document.documentElement.classList.add('no-transition');
        
        // Aplicar estilos sin animación
        sol.style.opacity = '0';
        luna.style.opacity = '1';
        document.documentElement.classList.add('dark');
        mov_realizado = false;
        
        // Remover la clase después de aplicar los estilos
        setTimeout(() => {
            document.documentElement.classList.remove('no-transition');
        }, 100);
    }
});

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
    localStorage.setItem('darkMode', 'true');
}

function movimiento_inverso() {
    sol.style.transform = 'rotate(-180deg)';
    sol.style.opacity = '1';
    luna.style.opacity = '0';
    luna.style.transform = 'rotate(-200deg)';
    mov_realizado = true;
    localStorage.setItem('darkMode', 'false');
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