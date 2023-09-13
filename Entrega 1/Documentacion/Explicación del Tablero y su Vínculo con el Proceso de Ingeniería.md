# Explicación del Tablero y su Vínculo con el Proceso de Ingeniería

En primera instancia, decidimos no añadir al tablero ninguna tarea vinculada al trabajo previo del análisis del código y testing, ya que, aunque consideramos que dichas tareas son parte del proceso de ingeniería, no nos es relevante tenerlas en el mismo.

Dicho esto, nuestro tablero consta de tres columnas: Todo, In Progress, y Done, donde cada una de ellas representa un estado del proceso de ingeniería.

A su vez, la columna de In Progress tiene un límite de WIP (Work In Progress) de tres, definiendose así una tarea para cada intergrante del equipo.

El procedimiento que elegimos fue el siguiente:

1. Al momento de encontrar un bug o issue:
    * Creamos un issue en la columna de **Todo**.
    * Le asignamos un nombre y una breve descripción (la cual será profundizada posteriormente).
    * A su vez, a la descripción le añadimos los campos de Responsable, Estado, Día de Inicio y Día de Finalización que serán completados más adelante.
2. Luego, cuando alguien del equipo toma la issue:
    * Mueve dicha tarea a la columna de **In Progress**.
    * Rellena los campos antes mencionados.
    * Analiza detenidamente el caso.
    * Amplía la descripción, y adjunta una captura del error.
3. Por último, cuando la issue es resuelta:
    * Se mueve a la columna de **Done**.
    * Se añade la Fecha de Finalización.

De esta manera, el tablero nos permite visualizar el trabajo que se va realizando para poder emplearlo en el Standup Diario, y tener una mayor idea de quien está a cargo de que tarea y en que estado se encuentra.