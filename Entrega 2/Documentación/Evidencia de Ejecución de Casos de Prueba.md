# Evidencia de Ejecución de Casos de Prueba

La Evidencia de Ejecución de Casos de Prueba desempeña un papel crítico en el proceso de desarrollo de software, permitiendo la validación y verificación de las funcionalidades implementadas. 

En este documento, se analizará en detalle cómo se aplicó esta práctica adoptando Test Driven Development (TDD) en dos de las issues a resolver, mientras que en la tercera, al estar relacionada con el desarrollo frontend, se optó por no seguir esta modalidad ya que no era un requisito.

- [Issue #18](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/18)
    - Es posible realizar solicitudes de reposición de stock por cantidades negativas.
      
    - Visualizando el error:
      
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Error.png)
      
    - En estado Red:
 
      Como se puede apreciar, no existe verificación de que la cantidad sea negativa para luego lanzar la debida exception.
 
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Red.png)

      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Test.png)
      
    - En estado Green:
 
      Se añadió dicha verificación junto con su exception. 
 
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Green.png)

    - Visualizando el arreglo:
      
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Fixed.jpg)
