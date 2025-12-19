# ProductSeeker

## Descripción general

**ProductSeeker** es una aplicación en desarrollo cuyo objetivo es permitir a los usuarios registrar, consultar y comparar **productos y comercios**, haciendo especial foco en el **seguimiento histórico de precios**.

El proyecto surge como una evolución consciente de un desarrollo inicial basado en un tutorial. Luego de una primera implementación, se realizó una **replanificación completa del dominio**, los requerimientos y la estructura del sistema, priorizando coherencia conceptual, extensibilidad y decisiones de diseño justificadas.
ProductSeeker
Descripción general

ProductSeeker es una aplicación en desarrollo cuyo objetivo es permitir a los usuarios registrar, consultar y comparar productos y comercios, haciendo especial foco en el seguimiento histórico de precios.

El proyecto surge como una evolución de un desarrollo inicial basado en un tutorial. Luego de una primera implementación, se realizó una replanificación completa del dominio, los requerimientos y la estructura del sistema, priorizando coherencia conceptual, extensibilidad y decisiones de diseño justificadas.

Actualmente el proyecto se encuentra en una **fase de reestructuración activa**.

---

## Objetivo del proyecto

Construir una aplicación que permita:

* Registrar comercios y productos
* Asociar productos a comercios
* Mantener un **histórico completo de precios**
* Consultar la evolución de precios en el tiempo
* Comparar precios entre distintos comercios


---

## Funcionalidad mínima (MVP)

El alcance mínimo indispensable del proyecto incluye:

* Registro de **comercios**
* Registro de **productos**
* Registro de **precios por comercio y fecha**
* Consulta de información histórica de precios

Quedan explícitamente fuera del MVP:

* Servicios
* Notificaciones automáticas
* Rankings avanzados
* Reputación de usuarios
* Compartición de listas de productos

Estos puntos están contemplados a nivel conceptual, pero no forman parte del alcance actual.

---

## Conceptos de dominio clave

### Comercios

Un comercio representa un punto de venta físico o virtual.
Se almacena información básica como nombre, tipo, ubicación y datos descriptivos.

### Productos

Los productos representan bienes comercializables de distintos tipos (alimentos, higiene, electrodomésticos, etc.).

El modelo busca un equilibrio entre:

* estructura suficiente para permitir indexación y comparación
* flexibilidad para distintos tipos de productos

### Precios e histórico

Un producto **no tiene un precio único**.

Cada precio es:

* contextual (producto + comercio)
* temporal (fecha)
* registrado como un evento

Cada cambio de precio se modela como **un nuevo registro**, nunca como una actualización destructiva.
El “precio actual” es simplemente el último registro válido.

El sistema introduce una entidad de produto globala que se crea de forma emergente bajo demanda, a partir de los registros de usuarios.
No existe un catalogo precargado ni un proceso de verificacion en el MVP, la normalizacion y consolidacion de productos se considera mejora futura

---

## Tecnologías utilizadas

### Backend

* **.NET**
* **ASP.NET Web API**
* **Entity Framework Core**
* **SQL Server**

### Frontend

* En desarrollo (estructura inicial separada del backend)

---

## Estado actual

* El proyecto se encuentra en proceso de **refactorización estructural**
* Se están redefiniendo:

  * entidades
  * relaciones
  * responsabilidades del dominio
* Parte del código inicial responde a un tutorial y está siendo reemplazado progresivamente

Esto es intencional y forma parte del proceso de aprendizaje y mejora del diseño.

---

## Motivación

Este proyecto no busca ser únicamente funcional, sino servir como:

* ejercicio de diseño de dominio
* práctica real de modelado de datos
* demostración de criterio técnico y capacidad de replantear decisiones

---

## Notas finales

ProductSeeker es un proyecto en evolución.
Las decisiones de diseño priorizan claridad conceptual y extensibilidad por sobre soluciones rápidas.

Cualquier feedback o sugerencia es bienvenida.
