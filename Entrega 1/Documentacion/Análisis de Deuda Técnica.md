# Análisis de Deuda Técnica

El objetivo principal de esta primera etapa es realizar un exhaustivo Testing Exploratorio y un análisis estático de código para identificar posibles problemas, vulnerabilidades y deuda técnica en el software existente.

Para llevar a cabo esta tarea de manera organizada y efectiva, hemos utilizado un tablero de Kanban que nos ha permitido gestionar y seguir el progreso de nuestro trabajo de manera visual. En este informe, detallaremos las dos principales áreas de enfoque de nuestra evaluación: el Testing Exploratorio y el Análisis Estático de Código.

## Testing Exploratorio

Para el Testing Exploratorio, hemos creado una serie de sesiones que describen los pasos que hemos seguido para evaluar el funcionamiento del sistema. Cada sesión se ha documentado y vinculado a las issues correspondientes en nuestro tablero de Kanban. De esta manera, hemos asegurado que cada hallazgo sea fácilmente rastreable y esté conectado a su correspondiente acción correctiva. Cada sesión de test exploratorio se basa en una funcionalidad específica, entre las cuales tenemos "Registro y Login de Usuarios", "Invitación a Usuarios", "Visualización y Modificación de Invitaciones", "Creación de Farmacias", "Invitación a Usuarios por Dueño", "Alta y Baja de Medicamentos", "Visualización de Compras", "Listado y Filtros de Solicitudes de Stock", "Tracking de Compras", "Compras", "Stock Request", "Confirmación de compras como empleado", "Ver historial de compras como dueño de una farmacia", "Exportación de medicamentos".

En todas estas sesiones se utilizó un [template](./Template%20Sesiones%20de%20Testing%20Exploratorio.md) y se añadió en sus respectivos tableros de Kanban.

#### Issues de Testing Exploratorio:

Notas: 
* La issue #1 fue creada como una prueba inicial para comprender el funcionamiento del tablero de Kanban y se cerró luego de su creación. 
* Las issues enumeradas desde la #2 hasta la #6 se originaron debido a un error en la configuración del puerto del backend en el frontend, que solo se manifestaba cuando el programa no se ejecutaba desde el debugger. Es importante destacar que estas issues no representan bugs reales del sistema, sino que fueron errores de los testers y, por lo tanto, se cerraron después de su identificación.

A continuación, presentamos un resumen del resto de las issues identificadas durante el proceso de Testing Exploratorio. Cada una de ellas representa un área de mejora o una preocupación específica en relación con el software existente. Cada issue incluye una descripción del problema y un enlace a la Issue correspondiente en nuestro tablero de Kanban, en donde se encuentra evidencia de este. Todas estas Issues utilizan el mismo [template](./Template%20Issues.md)

- Issue #7
    - Al intentar editar una invitación, dejando campos vacíos, esta se actualiza correctamente, sin tomar en cuenta la falta de datos, dejando los originales, lo cuál que no nos parece intuitivo.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/7)

- Issue #8
    - Al intentar dar de alta un medicamento con números elevados, tanto en precio como en cantidad, se muestra el mensaje de error "Crate Drug failed: undefined"
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/8)

- Issue #9
    - Al intentar dar de alta un medicamento con precio no numérico se muestra el mensaje de error "Crate Drug failed: undefined"
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/9)

- Issue #10
    - Al presionar el botón de borrar de un medicamento con nombre muy largo, este se sale de la pantalla. El mismo bug visual también ocurre en la lista de farmacias.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/10)

- Issue #11
    - En el menú de compras, a la hora de filtrar por farmacias, cuando muestra los medicamentos de todas las farmacias, dice "All Pharmacys" cuando debería decir "All Pharmacies".
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/11)

- Issue #12
    - Luego de agregar un medicamento al carro, y clickear el botón para seguir agregando medicamentos, el filtro por farmacias se reinicia. Independientemente de la opción que haya seleccionado el usuario, vuelve a "All Pharmacys".
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/12)

- Issue #13
    - La barra de búsqueda por medicamento devuelve solo los medicamentos con el nombre exacto al que se buscó. Por ejemplo, la búsqueda de "Kalitron" no devuelve resultados, pero la búsqueda de "Kalitron Fuerte 10 Grageas" si.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/13)

- Issue #14
    - Las notificaciones que aparecen en la parte inferior de la pantalla no expiran, y permanecen hasta que el usuario las cierra manualmente.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/14)

- Issue #15
    - Es posible agregar cantidades negativas de medicamentos al carrito. A la hora de seleccionar la cantidad de medicamentos a agregar, es posible agregar manualmente un signo de menos. <br>
    Al agregar medicamentos de forma que el precio sea positivo, es posible proceder a la pestaña de compras. No es posible efectuar la compra, ya que devuelve un error, pero la descripción del mensaje de error no parece ser la correcta.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/15)

- Issue #16
    - Es posible agregar medicamentos con cantidad 0 al carrito.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/16)

- Issue #17
    - Es posible agregar medicamentos con cantidades decimales al carrito.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/17)

- Issue #18
    - Es posible realizar solicitudes de reposición de stock por cantidades negativas.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/18)

- Issue #19
    - Es posible realizar solicitudes de reposición de stock con cantidad 0.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/19)

- Issue #20
    - Es posible agregar cantidades decimales de medicamentos en las solicitudes de reposición de stock. No es posible efectuar las solicitudes. El mensaje de error que lo comunica no es el indicado.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/20)

- Issue #21
    - Como dueño de una farmacia, al ver el historial de esa farmacia, al seleccionar un único día en el calendario, no es posible ver las compras de ese día.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/21)


## Análisis Estático de Código

Además de nuestro enfoque en el Testing Exploratorio, también hemos llevado a cabo un análisis estático del código fuente del proyecto. Este análisis nos ha permitido identificar posibles problemas de diseño, violaciones de buenas prácticas de codificación y oportunidades para mejorar la calidad del código.

En la siguiente sección, detallaremos nuestras observaciones y hallazgos en el análisis estático de código, proporcionando recomendaciones para abordar la deuda técnica y mejorar la calidad del software.

La encargada de este análisis fue Fiorella debido a que, al no poder instalar el ambiente por problemas con su computadora mac, no pudo participar demasiado en el Testing Exploratorio.

Luego de una investigación acerca de que analizador estatico de codigo era el mejor, la herramienta que elegimos fue SonarLint, ya que se integraba con Visual Studio Code muy facilmente, y permitia visualizar los errores a simple vista.

A continuación, al igual que con el Testing Exploratorio, presentamos un resumen de las issues identificadas.

Para TypeScript en el Frontend:

- Issue #22
    - El constructor es innecesario ya que está vacío, y es posible removerlo.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/22)
- Issue #23
    - Uso del tipo var en el constructor al asignarle "this" a la variable "self".
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/23)
- Issue #24
    - Constructor con nueve parámetros cuando el correcto uso solo permite siete.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/24)
- Issue #25
    - Uso de tipos innecesarios en funciones or ("|") debido a la palabra registrada "any", la cual hace referencia a todos los posibles tipos.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/25)
- Issue #26
    - Import innecesario el cual es posible remover.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/26)
- Issue #27
    - Posible expresión en cadena utilizando el símbolo ?.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/27)
- Issue #28
    - Uso redundante de "undefined" cuando es posible removerlo y la función lo recibirá de igual manera.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/28)
- Issue #29
    - Variables que no se utilizan y es posible remover su declaración.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/29)

Para C# en el Backend:

- Issue #30
    - Código que está comentado y no es relevante, siendo posible removerlo.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/30)
- Issue #31
    - Existen atributos que pueden modificarse para ser readonly.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/31)
- Issue #32
    - Existen funciones con mismo nombre (pero parámetros distintos) que deberían ser adyacentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/32)
- Issue #33
    - Existen funciones que pueden devolver valores nulos sin tenerlos en cuenta.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/33)
- Issue #34
    - Es posible modificar la visibilidad de atributos.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/34)
- Issue #35
    - El argumento que se le pasa a una función como parámetro puede llegar a ser nulo, y no se tiene verificación previa.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/35)
- Issue #36
    - Se le asigna a variables no nuleables, un null.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/36)
- Issue #37
    - Doble if que puede ser unido y terminar con uno solo.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/37)
- Issue #38
    - Chequeo con la palabra "false" innecesario cuando es posible omitirla.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/38)
- Issue #39
    - Se tiene de ambos lados de == a la misma expresión.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/39)
- Issue #40
    - Se accede a atributos que pueden ser nulos, y no se verifica dicha operación.
    - Esta situación se repite en varios componentes.
    - [Link a la Issue](https://github.com/IngSoft-ISA2-2023-2/obligatorio-mazziotti-macedo-torres/issues/40)

Con estos dos enfoques complementarios, esperamos proporcionar una evaluación completa y útil del backend y frontend existentes, destacando las áreas que requieren atención y acción.