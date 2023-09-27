# Evidencia de Ejecución de Casos de Prueba

La Evidencia de Ejecución de Casos de Prueba desempeña un papel crítico en el proceso de desarrollo de software, permitiendo la validación y verificación de las funcionalidades implementadas. 

En este documento, se analizará en detalle cómo se aplicó esta práctica adoptando Test Driven Development (TDD) en dos de las issues a resolver, mientras que en una, al estar relacionada con el desarrollo frontend, se optó por no seguir esta modalidad ya que no era un requisito.

- [Issue #18](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/18)
    - Es posible realizar solicitudes de reposición de stock por cantidades negativas.
      
    - Visualizando el error:
      
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Error.png)

    - El test realizado:
      
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Test.png)
      
    - En estado Red:
 
      Como se puede apreciar, no existe verificación de que la cantidad sea negativa para luego lanzar la debida exception.
 
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Red.png)
      
    - En estado Green:
 
      Se añadió dicha verificación junto con su exception. 
 
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Green.png)

    - Visualizando el arreglo:
      
      ![](../Imágenes/Issue%20%2318/Issue%20%2318%20-%20Fixed.jpg)

- [Issue #20](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/20)
    - Es posible agregar cantidades decimales de medicamentos en las solicitudes de reposición de stock. No es posible efectuar las solicitudes. El mensaje de error que lo comunica no es el indicado.

    - Visualizando el error:

      No se verfica la prohibición de ingresar digitos que no sean números (para así formar decimales).

      ![](../Imágenes/Issue%20%2320/Issue%20%2320%20-%20Code%20Error.png)
      
      ![](../Imágenes/Issue%20%2320/Issue%20%2320%20-%20Error.png)

    - Visualizando el arreglo:
      
      Se añadió dicha verificación. 

      ![](../Imágenes/Issue%20%2320/Issue%20%2320%20-%20Code%20Fixed.png)

      ![](../Imágenes/Issue%20%2320/Issue%20%2320%20-%20Fixed.jpg)

- [Issue #21](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/21)
    - Como dueño de una farmacia, al ver el historial de esa farmacia, al seleccionar un único día en el calendario, no es posible ver las compras de ese día.
      
    - Visualizando el error:

      Si probamos buscar las compras de un día en particular (16/09/2023), no se muestra ninguna compra.
      
      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Error(1).png)

      Sin embargo, al expandir la busqueda un día más (17/09/2023), se puede ver que en realidad si existían compras en el día anterior (16/09/2023).

      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Error(2).png)

    - El test realizado:
      
      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Test.jpg)
      
    - En estado Red:
 
      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Red(1).png)

      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Red(2).png)
      
    - En estado Green:
 
      Lo que ocurría era un error al tomar las horas del start y end date, por lo que añadió la especificación de que sea desde las 00:00:00 hasta las 23:59:59. 
 
      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Green(1).png)

      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Green(2).png)

    - Visualizando el arreglo:

      Ahora si es posible visualizar la compra realizada el día en cuestión (16/09/2023).
      
      ![](../Imágenes/Issue%20%2321/Issue%20%2321%20-%20Fixed.png)      