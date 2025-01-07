# 🌐 TriangleMeshLighting

**TriangleMeshLighting** is a **.NET Windows Forms** application that visualizes a **triangulated 3rd-degree Bezier surface** using a **Lambertian lighting model** and **barycentric approximation** for polygon rendering. The program allows users to define control points, generates a triangulated mesh, and visualizes the result with dynamic lighting and customizable parameters.

---

## Overview
The program constructs a **triangular mesh** representing the **3rd-degree Bezier surface** by interpolating control points in the \((x, y)\) domain. The lighting on the surface is computed using the **Lambertian lighting model**, which simulates realistic diffuse shading based on the object's orientation relative to the light source.

---

## Lighting Model
The lighting is calculated using the **Lambertian model**:
\[
I = k_a \cdot I_L \cdot I_O + k_d \cdot I_L \cdot I_O \cdot \cos(\theta_{N,L}) + k_s \cdot I_L \cdot I_O \cdot \cos^m(\theta_{V,R})
\]
where:
- \( k_a, k_d, k_s, m \) – material coefficients (ambient, diffuse, specular).
- \( I_L \) – light intensity.
- \( I_O \) – object color.
- \( N \) – surface normal vector.
- \( L \) – light direction vector.
- \( V \) – viewing vector.
- \( R \) – reflection vector.

---

##  Features
- **Dynamic Light Position**: Move the light source and see real-time shading changes.
- **Customizable Normal Map**: Modify the surface normals for more detailed lighting effects.
- **Custom Textures**: Apply different textures to enhance the visual detail of the surface.
- **Adjustable Colors**: Customize the color and intensity of the light source and object surface.

---

## 🛠️ Tech Stack
- **Language**: C#
- **Framework**: .NET (Windows Forms)
- **Graphics Library**: GDI+ for custom rendering.
