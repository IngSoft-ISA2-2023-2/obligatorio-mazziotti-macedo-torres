# Explicación del Tablero y su Vínculo con el Proceso de Ingeniería

Para esta cuarta etapa, hemos revisado y actualizado nuestro tablero Kanban en función de las nuevas actividades y ceremonias incorporadas. El diseño actual del tablero es un reflejo directo de nuestro proceso de ingeniería mejorado, garantizando una correspondencia clara entre las tareas y las columnas del tablero.

El tablero ahora se compone de las siguientes columnas: **Todo**, **Requirements Definition**, **Test Cases Implementation**, **Application Implementation**, **Automation Testing**, **Refactoring**, **Test Driven Development (TDD)**, **In Progress**, **Integration Testing** y **Done**.

**Tipos de Issues:**
1. **User Stories:** Estas avanzan a través de las columnas en el siguiente orden:
   - **Todo**: Punto de partida donde se definen las user stories.
   - **Requirements Definition**: Aquí se definen los escenarios sobre las User Stories.
   - **Test Cases Implementation**: Se diseñan los tests utilizando Specflow basándose en los escenarios definidos.
   - **Application Implementation**: Se desarrolla la implementación de las user stories.
   - **Automation Testing**: Se ejecutan los tests diseñados con Specflow para validar las implementaciones.
   - **Refactoring**: Se realiza una revisión del código, reescribiéndolo para mejorar su legibilidad y mantenibilidad sin alterar su comportamiento.
   - **Integration Testing**: Se crean y ejecutan las pruebas de las funcionalidades desde el frontend utilizando Selenium.
   - **Done**: Etapa final donde la user story se considera completa.

2. **Bugs:** Los bugs tienen un flujo de trabajo diferente:
   - **Todo**: Inicio del bug identificado.
   - **Test Driven Development (TDD)**: Engloba todo el proceso de red, green, refactor. Incluye la creación de tests, codificación para el arreglo del bug, ejecución de los tests y refactor.
   - **Integration Testing**: Se llevan a cabo pruebas manuales y se crean y ejecutan tests con Selenium para asegurarse de que el bug ha sido resuelto.
   - **Done**: La conclusión indica que el bug ha sido solucionado satisfactoriamente.

3. **Tareas Auxiliares**: Otras tareas, como pueden ser la instalación de ambientes, creación de proyectos y documentación, tienen un flujo simplificado:
   - **Todo**: Inicio de la tarea.
   - **In Progress**: Desarrollo y realización de la tarea.
   - **Done**: Conclusión de la tarea.

El WIP (Work In Progress) sigue estando limitado a 3 tareas para mantener un flujo de trabajo equilibrado y garantizar que el equipo no se sobrecargue en ninguna etapa.

En cuanto a los roles del equipo, todos los miembros desempeñan múltiples funciones siendo tanto **Desarrolladores** como **Testers**. Las decisiones que generalmente tomaría el **Product Owner** se deciden colectivamente por el equipo, garantizando la responsabilidad compartida. Además, para esta entrega, Fiorella actuará como **"Scrum Master"**, guiando y asegurando la adherencia a las prácticas de Kanban.

Con estas adaptaciones, nos aseguramos de tener un tablero Kanban que refleje fielmente nuestro proceso de ingeniería y nos permita monitorear y ajustar nuestro progreso de manera efectiva.