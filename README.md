# TriangleMeshLighting

**TriangleMeshLighting** is a **.NET Windows Forms** application that visualizes a **triangulated 3rd-degree Bezier surface** using a **Lambertian lighting model** and **barycentric approximation** for polygon rendering. The program allows users to define control points, generates a triangulated mesh, and visualizes the result with dynamic lighting and customizable parameters.

---

## **Lighting Model**  

The lighting is calculated using the **Lambertian model**:

**I = k_d * I_L * I_O * cos(θ<sub>N,L</sub>) + k_s * I_L * I_O * cos<sup>m</sup>(θ<sub>V,R</sub>)**

where:
- **k_a, k_d, k_s, m** – material coefficients (ambient, diffuse, specular).
- **I_L** – light intensity.
- **I_O** – object color.
- **N** – surface normal vector.
- **L** – light direction vector.
- **V** – viewing vector.
- **R** – reflection vector.

---

## Tech Stack
- **Language**: C#
- **Framework**: .NET (Windows Forms)
- **Graphics Library**: GDI+ for custom rendering.

---

## Presentation 

![](meshExample.gif)
