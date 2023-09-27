# Configuración del pipeline

En esta segunda etapa del proyecto, hemos implementado un pipeline automatizado utilizando GitHub Actions para agilizar y gestionar eficazmente el ciclo de vida del desarrollo de nuestro software. El pipeline se ha diseñado para realizar varias tareas esenciales en el proceso de construcción, prueba y entrega continua del software, sin entrar en detalles de código. A continuación, describiremos las principales funciones del pipeline:

### Automatización de Tareas

El pipeline se activa automáticamente en respuesta a los push y las pull requests que se realicen sobre la rama main del repositorio cuando se modifiquen archivos con extensiones .cs o .csproj. Esto garantiza que las tareas de construcción y prueba se realicen de manera coherente en cada cambio realizado en el código fuente.

### Fases del Pipeline

El pipeline se compone de varias fases, cada una con un propósito específico:

1. Checkout: Descarga el código fuente del repositorio en el runner del pipeline, asegurando que el código más reciente esté disponible para su procesamiento.

2. Setup .NET Core: Configura el entorno de desarrollo .NET Core en el runner del pipeline, lo que permite compilar y ejecutar el código de la aplicación.

3. Install dependencies: Restaura las dependencias del proyecto, lo que incluye bibliotecas y paquetes necesarios para la construcción y ejecución de la aplicación.

4. Build: Realiza la compilación del proyecto en modo Release. Durante esta fase, se genera el código ejecutable a partir del código fuente.

5. Test: Ejecuta las pruebas unitarias del proyecto. Esto verifica que el código producido cumple con las expectativas y no introduce errores.

6. Test Report: Genera un informe de pruebas que proporciona información detallada sobre los resultados de las pruebas realizadas. Este informe es útil para el análisis y seguimiento de problemas.

### Problemas Detectados

Durante la ejecución de las pruebas, hemos identificado que uno de los tests existentes no se comporta de la misma manera en el pipeline que cuando se ejecuta localmente desde Visual Studio. Este problema también se ha observado en uno de los tests creados en esta etapa, específicamente relacionado con la issue #21.

Es importante señalar que, debido a las restricciones de tiempo entre entregas, no hemos tenido la oportunidad de abordar y resolver estos problemas en esta etapa. Planeamos investigar y solucionar estas discrepancias en futuras etapas para garantizar la coherencia en la ejecución de pruebas en diferentes entornos.

En resumen, el pipeline automatizado implementado en GitHub Actions desempeña un papel fundamental en la construcción y prueba de nuestro software, garantizando que el código cumpla con los estándares de calidad y esté listo para su entrega continua en el ciclo de desarrollo.




