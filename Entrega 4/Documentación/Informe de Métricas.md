# Informe de Métricas

En el siguiente informe, abordaremos las métricas para User Stories (US) y para bugs. Es importante señalar que, para las User Stories, consolidaremos los datos de las entregas 3 y 4 para realizar un análisis más robusto y coherente.

Las métricas a analizar serán:

- Tiempo de Entrega (Lead Time)
- Tiempo de Ciclo (Cycle Time)
- Eficiencia del Flujo (Flow Efficiency)
- Capacidad de Entrega (Throughput)

A continuación, se presenta una breve descripción de cada métrica y se detallan sus resultados.


## Tiempo de Entrega (Lead Time)

**Definición**: Refiere al tiempo desde que se crea una tarea (se coloca en TO DO) hasta que se finaliza la tarea (pasa a DONE). Se mide en alguna unidad de tiempo, en este caso, días.

| Tipo de Tarea | Nombre | Tiempo (Días) | Desviación |
|---------------|--------|---------------|------------|
| Bugs | Realizar Solicitudes de Stock por Cantidades Negativas | 3 | 0.33 |
| Bugs | Filtrado por Fecha en el Historial de Compras | 2 | -0.67 |
| Bugs | Realizar Solicitudes de Stock por Cantidades Decimales | 3 | 0.33 |
| US   | Alta de Productos | 12 | -2.25 |
| US   | Baja de Productos | 14 | -0.25 |
| US   | Modificación de Produtos | 15 | 0.75 |
| US   | Compra de Productos | 16 | 1.75 |

**Promedio de Lead Time**:
- Bugs: 2.67 días
- US: 14.25 días

**Comparación**:
Comparar el Lead Time de Bugs con US en este contexto puede ser desafiante debido a la naturaleza y complejidad de las tareas. Por lo general, los bugs tienden a ser más específicos y requieren menos tiempo que las User Stories, que abarcan funcionalidades completas.

---

## Tiempo de Ciclo (Cycle Time)

**Definición**: Refiere al tiempo desde que se comienza una tarea (pasa a IN PROGRESS) hasta que se finaliza la tarea (pasa a DONE). Se mide en días.

| Tipo de Tarea | Nombre | Tiempo (Días) | Desviación |
|---------------|--------|---------------|------------|
| Bugs | Realizar Solicitudes de Stock por Cantidades Negativas | 1 | 0 |
| Bugs | Filtrado por Fecha en el Historial de Compras | 1 | 0 |
| Bugs | Realizar Solicitudes de Stock por Cantidades Decimales | 1 | 0 |
| US   | Alta de Productos | 7 | -1 |
| US   | Baja de Productos | 8 | 0 |
| US   | Modificación de Produtos | 8 | 0 |
| US   | Compra de Productos | 9 | 1 |

**Promedio de Cycle Time**:
- Bugs: 1 día
- US: 8 días

**Comparación**:
Al igual que con el Lead Time, el Cycle Time para bugs tiende a ser menor debido a su especificidad. Las US, al ser tareas más grandes, requieren más tiempo en desarrollo.

---

## Eficiencia del Flujo (Flow Efficiency)

**Definición**: Representa la eficiencia con la que se maneja una tarea. Se calcula como: 
Flow Efficiency = Cycle Time / Lead Time x 100%

| Tipo de Tarea | Nombre | Flow Efficiency |
|---------------|--------|------------------|
| Bugs | Realizar Solicitudes de Stock por Cantidades Negativas | 33.33% |
| Bugs | Filtrado por Fecha en el Historial de Compras | 50% |
| Bugs | Realizar Solicitudes de Stock por Cantidades Decimales | 33.33% |
| US   | Alta de Productos | 58.33% |
| US   | Baja de Productos | 57.14% |
| US   | Modificación de Produtos | 53.33% |
| US   | Compra de Productos | 56.25% |

**Referencias sobre Flow Efficiency**: Según David J Anderson de la Kanban University, una buena Flow Efficiency en Kanban es considerada cualquier valor por encima del 40%. Sin embargo, es vital tener en cuenta que los equipos que recién se inician en la gestión de proyectos Lean o aquellos que no se centran en su flujo suelen tener una eficiencia de flujo entre el 5% y el 15%. 

Es por ello que podemos afirmar que, de a cuerdo a Anderson, en general este obligatorio cuenta con una buena eficiencia de flujo, principalmente en las user stories.

Para profundizar en este tema, se pueden revisar los siguientes enlaces:
1. [Kanban University: Flow Efficiency](https://kanban.university/flow-efficiency-a-great-metric-you-probably-arent-using/)
2. [Kanbanize Blog: How to Measure Flow Efficiency on a Kanban Board](https://businessmap.io/blog/how-to-measure-the-flow-efficiency-of-a-process-on-a-kanban-board).

---

## Capacidad de Entrega (Throughput)

**Definición**: Cantidad de elementos entregados al cliente en un periodo dado.

**Periodo**:
- Bugs: 10 días
- US: 31 días (suma entre entregas 3 y 4)

| Tipo de Tarea | Cantidad Entregada |
|---------------|--------------------|
| Bugs          | 3                  |
| US            | 4                  |

**Comparación**:
El Throughput, al ser una métrica absoluta, muestra que se entregaron más US en comparación con bugs en los tiempos estipulados. No obstante, es esencial considerar el contexto; los bugs suelen ser más específicos y rápidos de resolver, mientras que las US representan funcionalidades completas.

---

**Reflexiones finales**:
Si bien se han hecho comparaciones entre bugs y US en cada métrica, es fundamental entender el contexto de cada proyecto y equipo. Las User Stories suelen ser tareas más amplias y complejas, mientras que los bugs, aunque críticos, suelen ser más específicos. La interpretación de estas métricas debe hacerse con cuidado y siempre en el contexto del proyecto específico.