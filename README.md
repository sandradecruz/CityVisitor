# 🏙️ CityVisitor - Unity VR XR 

**CityVisitor** es nuestro **Trabajo de Fin de Grado (TFG)**: una experiencia en **Realidad Virtual** desarrollada con **Unity** usando **XR (Unity XR / XR Interaction Toolkit)** que permite **visitar distintas ciudades** y descubrir sus **monumentos más característicos** acompañados de una **explicación informativa**.

Actualmente incluye ciudades como **Valladolid** y **Salamanca** (Aunque Salamanca no consta de ningún monumento).

---

## Descripción del proyecto

La aplicación está pensada como un recorrido inmersivo en VR donde la persona usuaria puede:

- Elegir o acceder a diferentes **entornos urbanos** (ciudades).
- Desplazarse por el escenario en VR para **explorar**.
- Interactuar con puntos de interés y **monumentos**.
- Consultar o activar una **explicación** (texto/audio, según implementación) sobre cada monumento.

---

## Ciudades y contenido

### Valladolid
Monumentos y puntos de interés incluidos en el recorrido:
- (Ej.) Plaza Mayor
- (Ej.) Catedral de Valladolid
- (Ej.) Campo Grande

---

## Tecnologías

- **Unity** (proyecto 3D)
- **C#**
- **XR / VR** (Unity XR + **XR Interaction Toolkit**)

---

## Requisitos

- **Unity Hub** + la **misma versión de Unity** con la que se creó el proyecto.
- (Opcional) Visual Studio / JetBrains Rider para editar scripts.
- Un visor compatible con OpenXR / el runtime configurado (según el dispositivo objetivo).

> Si el proyecto está configurado para **OpenXR**, asegúrate de tener el runtime correcto activo en tu sistema (por ejemplo, SteamVR u otro, según el visor).

---

## Instalación y ejecución

1. Clona el repositorio:
   ```bash
   git clone https://github.com/sandradecruz/CityVisitor.git
   ```
2. Abre **Unity Hub** → *Add/Open* y selecciona la carpeta del proyecto.
3. Abre la escena principal del proyecto (por ejemplo en `Assets/Scenes/`).
4. Conecta el visor VR (si aplica) y pulsa **Play**.

---

## Controles e interacción (VR)

- **Teleport / locomoción** mediante controladores VR.
- **Rotación** por snap turn o smooth turn.
- Interacción con elementos y puntos de información mediante **XR Ray Interactor** o interacción directa.
- Activación de la **explicación del monumento** al seleccionar un elemento.

---

## Roadmap (opcional)

- [ ] Añadir rutas guiadas
- [ ] Mejorar UI/UX de las explicaciones (audio + subtítulos, accesibilidad)
- [ ] Optimización de rendimiento para VR (LOD, baked lighting, occlusion, etc.)
- [ ] Menú de selección de ciudad y progreso/coleccionables (si aplica)

---

## Autoría

TFG realizado por **@Coloma35** **@sandradecruz**
Repositorio: https://github.com/sandradecruz/CityVisitor
