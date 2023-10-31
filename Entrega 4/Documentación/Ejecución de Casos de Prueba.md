# Ejecución de Casos de Prueba

## Instrucciones para la ejecución
Para poder ejecutar las pruebas es importante ejecutar una serie de pasos.

Primero, se debe cargar el .bak que incluyeron los autores originales del obligatorio. Luego, se debe abrir Codigo/Backend/PharmaGo.sln con Visual Studio. Una vez abierto, se debe utilizar la Consola del Administrador de Paquetes para ejecutar el comando 'Update-Database'. Al haber hecho esto se puede pasar a la ejecución de las pruebas.

Un elemento a tener en cuenta al intentar ejecutar todas las pruebas automáticamente, es que esto puede resultar en errores en algunas pruebas de compra. Esto se debe a que durante la prueba se agregan productos al carrito. Al ejecutarse la siguiente prueba, los Asserts que controlan la cantidad de elementos en el carrito fallan, ya que detectan los productos que quedaron de la prueba anterior. La solución a esto es simplemente ejecutar individualmente las pruebas que fallen, vaciando el carrito entre ejecución y ejecución.

## Evidencia de la ejecución
A continuación se encuentran videos donde se demuestra la ejecución exitosa de las pruebas realizadas con Selenium.

Se decidió hacer un video por cada funcionalidad probada, en el cual se incluyen todos los escenarios correspondientes. Estas funcionalidades son:
* [Alta de Producto](https://fi365-my.sharepoint.com/:v:/g/personal/fm251662_fi365_ort_edu_uy/EU_0oupnRB9HiBuqvCVA0OMBTRLzw7i6widv9Ro46G2o-Q?nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJTdHJlYW1XZWJBcHAiLCJyZWZlcnJhbFZpZXciOiJTaGFyZURpYWxvZyIsInJlZmVycmFsQXBwUGxhdGZvcm0iOiJXZWIiLCJyZWZlcnJhbE1vZGUiOiJ2aWV3In19&e=oCn2G2)
* [Baja de Producto](https://fi365-my.sharepoint.com/:v:/g/personal/fm251662_fi365_ort_edu_uy/EXyY6eSIoQ1Jncr-iDyFeIgBzSRGZ39q687-AIxjCdZNlA?nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJTdHJlYW1XZWJBcHAiLCJyZWZlcnJhbFZpZXciOiJTaGFyZURpYWxvZyIsInJlZmVycmFsQXBwUGxhdGZvcm0iOiJXZWIiLCJyZWZlcnJhbE1vZGUiOiJ2aWV3In19&e=i9avra)
* [Modificación de Producto](https://fi365-my.sharepoint.com/:v:/g/personal/fm251662_fi365_ort_edu_uy/EXiuedRUiH9EkntSyJVIIhoBaMVu_uUaDVj_1MlKyvTt5A?nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJTdHJlYW1XZWJBcHAiLCJyZWZlcnJhbFZpZXciOiJTaGFyZURpYWxvZyIsInJlZmVycmFsQXBwUGxhdGZvcm0iOiJXZWIiLCJyZWZlcnJhbE1vZGUiOiJ2aWV3In19&e=PQCWLm)
* [Compra](https://fi365-my.sharepoint.com/:v:/g/personal/fm251662_fi365_ort_edu_uy/EUpUB_SRFf5Mh4kYMHMxEx0BWiFlJWXOKX14iSKbH_7A0Q?nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJTdHJlYW1XZWJBcHAiLCJyZWZlcnJhbFZpZXciOiJTaGFyZURpYWxvZyIsInJlZmVycmFsQXBwUGxhdGZvcm0iOiJXZWIiLCJyZWZlcnJhbE1vZGUiOiJ2aWV3In19&e=hRGef1)
