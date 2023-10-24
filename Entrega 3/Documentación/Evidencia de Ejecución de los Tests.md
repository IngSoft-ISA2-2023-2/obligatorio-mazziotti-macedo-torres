# Evidencia de Ejecución de los Tests

### Introducción

El desarrollo de software moderno exige no solo escribir código, sino también asegurarse de que este código funcione como se espera en diversos escenarios. Para esto, las pruebas automatizadas son esenciales. Dentro del conjunto de herramientas disponibles para .NET, SpecFlow emerge como una opción popular para la implementación de pruebas de comportamiento o BDD (Behavior-Driven Development).

En nuestro proyecto, decidimos adoptar SpecFlow para definir y ejecutar ciertos escenarios de prueba. Sin embargo, durante este proceso, enfrentamos algunos desafíos notables que detallamos a continuación.

### Enfoque sin "Mocking"

Con base en discusiones en clase y exploraciones iniciales, optamos por un enfoque de pruebas sin "mocking". Esto significa que, al ejecutar nuestras pruebas con SpecFlow, los datos se creaban y eliminaban directamente en la base de datos real. Aunque este método puede parecer menos convencional y potencialmente riesgoso, proporciona una verificación más cercana al comportamiento real del sistema en un entorno de producción.

Sin embargo, este enfoque también presentó el desafío de la gestión de datos y el mantenimiento de la coherencia en la base de datos entre las ejecuciones de prueba.

### Desafíos con GitHub Actions

Al intentar integrar nuestras pruebas de SpecFlow en el pipeline de CI/CD de GitHub Actions, enfrentamos problemas. Dado que nuestras pruebas interactuaban directamente con la base de datos, cualquier variación en los datos o su estado podía causar fallos en las pruebas. A pesar de nuestros esfuerzos por estabilizar el entorno, las pruebas de SpecFlow resultaron ser menos deterministas en el entorno de CI/CD.

Por esta razón, tomamos la decisión de no incluir estas pruebas en GitHub Actions. A pesar de ello, garantizamos que estas pruebas funcionaban correctamente en un entorno local, específicamente en Visual Studio.

![Tests Specflow](../Imágenes/Tests%20Specflow.jpeg)

### Reflexiones y Aprendizajes

Si bien enfrentamos desafíos con nuestro enfoque de pruebas y la integración en GitHub Actions, también aprendimos lecciones valiosas:

1. Importancia de un entorno de pruebas controlado: Asegurarse de que las pruebas se ejecuten en un entorno estabilizado y controlado es crucial. Esto puede incluir la utilización de bases de datos de pruebas o el uso de "mocking".

2. Flexibilidad en las herramientas de CI/CD: Aunque GitHub Actions es una herramienta poderosa, como cualquier herramienta, tiene sus limitaciones. Es importante estar preparado para adaptarse y buscar soluciones alternativas cuando surgen problemas.

3. Valor del feedback local: A pesar de los problemas en el CI/CD, el feedback local que proporcionó SpecFlow fue invaluable. Las pruebas exitosas en Visual Studio reforzaron nuestra confianza en la calidad del código.

### Conclusión

A pesar de los desafíos enfrentados, SpecFlow demostró ser una herramienta valiosa en nuestro proceso de desarrollo. Nos permitió definir y verificar el comportamiento de nuestro sistema de manera clara y estructurada. A medida que avanzamos, nos llevamos los aprendizajes de esta experiencia para mejorar nuestros procesos y decisiones en futuros proyectos.
