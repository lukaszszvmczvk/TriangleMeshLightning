# 🌐 TriangleMeshLighting

**TriangleMeshLighting** is a **.NET Windows Forms** application that visualizes a **triangulated 3rd-degree Bezier surface** using a **Lambertian lighting model** and **barycentric approximation** for polygon rendering. The program allows users to define control points, generates a triangulated mesh, and visualizes the result with dynamic lighting and customizable parameters.

---

## Overview
The program constructs a **triangular mesh** representing the **3rd-degree Bezier surface** by interpolating control points in the \((x, y)\) domain. The lighting on the surface is computed using the **Lambertian lighting model**, which simulates realistic diffuse shading based on the object's orientation relative to the light source.

---

## Lighting Model
The lighting is calculated using the Lambertian model:

**I = ka * IL * IO + kd * IL * IO * cos(θ_N,L) + ks * IL * IO * cos^m(θ_V,R)**

Where:
- **ka, kd, ks, m**: Material coefficients (ambient, diffuse, specular).
- **IL**: Light intensity.
- **IO**: Object color.
- **N**: Surface normal vector.
- **L**: Light direction vector.
- **V**: Viewing vector.
- **R**: Reflection vector.

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
