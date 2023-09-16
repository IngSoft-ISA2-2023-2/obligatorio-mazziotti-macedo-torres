# Explicación del Tablero y su Vínculo con el Proceso de Ingeniería


Nuestro tablero consta de tres columnas: Todo, In Progress, y Done, donde cada una de ellas representa un estado del proceso de ingeniería.

A su vez, la columna de In Progress tiene un límite de WIP (Work In Progress) de tres, definiendose así una tarea a la vez para cada intergrante del equipo.

Cada Tarea va a contar con un Tipo en su descripción, el cuál puede ser Documentación, Instalación de Ambiente, Bug o Análisis de Código.

El procedimiento que elegimos fue el siguiente:

1. Al momento de agregar una tarea al tablero:
    * Creamos una tarea en la columna de **Todo**.
    * Le asignamos un nombre, id y una breve descripción.
    * A su vez, a la descripción le añadimos los campos de Tipo, Responsable, Tiempo en Todo, Tiempo en In Progress, Tiempo bloqueado, Esfuerzo. Estos campos serán completados más adelante.
2. Luego, cuando alguien del equipo toma la tarea:
    * Mueve dicha tarea a la columna de **In Progress**.
    * Rellena los campos antes mencionados.
    * De ser necesario amplía la descripción de la tarea.
3. Por último, cuando la tarea es resuelta:
    * Se mueve a la columna de **Done**.
    * Se añaden los tiempos y esfuerzo que tomó realizarla.

En el reporte de bugs se utilizarán dos tags:
* Prioridad, que determina la prioridad. Puede tener los siguientes valores:
    * Inmediata: plazo máximo 24 hs.
    * Alta: plazo máximo 48 hs. 
    * Media: plazo máximo un par de semanas.
    * Baja: sin plazo.  
* Severidad:
    * Critico: un defecto que obstaculice o bloquee completamente la prueba o el uso de un producto o función.
    * Mayor: una función principal que no cumpla con los requisitos y se comporte de manera diferente a lo esperado. Cuando funciona muy lejos de las expectativas o no está haciendo lo que debería estar haciendo.
    * Menor: función que no cumpla con sus requisitos y se comporte de manera diferente a lo esperado, pero su impacto es insignificante hasta cierto punto o no tiene un impacto importante en la aplicación.
    * Leve: defecto cosmético. 

De esta manera, el tablero nos permite visualizar el trabajo que se va realizando para poder emplearlo en el Standup Diario, y tener una mayor idea de quien está a cargo de que tarea y en que estado se encuentra.