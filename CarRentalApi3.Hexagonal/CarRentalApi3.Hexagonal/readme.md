

# Pasos básicos

* 1 diseño del dominio
* 2 Mapeo del dominio a base de datos
* 3 Meter Datos de prueba
* 4 Crear casos de uso
* 5 Endpoints


# Migración

dotnet ef --verbose migrations add InitialCreate -p src/CarRentalApi3.Hexagonal.Infrastructure/ -s src/CarRentalApi3.Hexagonal.Api

dotnet ef database update -p src/CarRentalApi3.Hexagonal.Infrastructure/ -s src/CarRentalApi3.Hexagonal.Api


Quita mediatR y usa Cortex.Mediator





# Restricciones de la aplicación

* Agregar un crud para coches.

* Si un coche está siendo alquilado no podrá ser alquilado por otra persona.

* Los coches no podrán darse de alta si tiene una antigüedad mayor que 5 años.

* Para calcular el precio del alquiler es el precio del coche * el numero de días del alquiler.

# Ejecución 

Dentro de la carpeta src/CarRentalApi3.Hexagonal.Api hacer dotnet run