# Explicación del Tablero y su Vínculo con el Proceso de Ingeniería

En esta tercera etapa, hemos realizado una actualización en el diseño de nuestro tablero Kanban para adaptarlo a las necesidades específicas del proceso de ingeniería. El tablero ahora consta de ocho columnas: <strong>Todo, In Progress, Requirements Definition, Test Cases Implementation, Application Implementation and Testing, Refactoring, Integration Testing y Done</strong>.

En esta versión del tablero se mantienen las limitaciones de <strong>WIP (Work In Progress)</strong> creadas en la versión anterior. Esto significa que en cualquier momento, el equipo puede trabajar en un máximo de tres tareas en las columnas In Progress, Requirements Definition, Test Cases Implementation, Application Implementation and Testing, Refactoring, Integration Testing combinadas. Esta restricción nos ayuda a mantener un flujo de trabajo más equilibrado y a evitar la sobrecarga de trabajo en cualquiera de estas etapas del proceso de ingeniería.

Las tareas de documentación siguen un flujo simplificado, igual a la etapa anterior, a través de tres columnas: Todo, In Progress, y Done.

Por otro lado, las user stories siguen un proceso más completo, avanzando a través de las siete columnas: Todo, Requirements Definition, Test Cases Implementation, Application Implementation and Testing, Refactoring, Integration Testing, Done.

Las columnas Todo, In Progress y Done se siguen comportando de la misma forma que lo hacían en la etapa anterior. 

En la columna Requirements Definition se escriben los escenarios sobre las User Stories. Luego pasan a la columna Test Cases Implementation, donde se crean los tests utilizando specflow y los escenarios anteriormente definidos. Finalizado esto, pasan a la etapa de Application Implementation and Testing, donde se implementa primer el front-end, luego el back-end, y por último se ejecutan las pruebas creadas con specflow. Después se pasa a la etapa de Refactoring, donde se toma el código y se reescribe sin alterar su comportamiento para mejorar su legibilidad y mantenibilidad. Habiendo terminado esto, se pasa a la etapa de Integration Testing, donde se testea manualmente las funcionalidades nuevas desde el frontend, pasando luego de esta revisión directo a Done.

También es importante destacar que todos los miembros del equipo desempeñan múltiples roles. Todos somos <strong>Desarrolladores y Testers</strong>, y además, las decisiones que tomaría el <strong>Product Owner</strong> son tomadas en conjunto por el equipo, por lo que asumimos la responsabilidad de revisar y aprobar las soluciones propuestas por miembros del equipo. A su vez, Nicolás es el <strong>Scrum Master</strong>.
