# Examen_1_AppServWEB_Mi_18_20
Examen evaluativo de la asignatura Aplicaciones y Servicios Web. 
Realiado por:
**Johan Esteba Arias y Yeison Andres Sanchez Rodas**

## Indice
- [Descripción del Problema](#Descripción-del-Problema)
  
	- [Se solicita](#Se-solicita)
   
 - [Tablas](#Tablas)
   
	- [Vivienda](#Vivienda)

	- [Agencia](#Agencia)

	- [Cliente](#Cliente)

	- [tipoVivienda](#tipoVivienda)

	- [Venta](#Venta)

- [Relaciones](#Relaciones)
- [Diagrama](#diagrama-modelo-relacional)
- [Video POSTMAN](#video-explicativo-y-desmostrativo-postman-)
- [Contribuidores](#contribuidores-)

## Descripción del Problema ❔
La agencia de venta de viviendas "ITM" tiene una única sede ubicada en la ciudad de
Medellín. Requiere un sistema para grabar las ventas de las viviendas que ofrece, sólo
ofrece viviendas nuevas.
El sistema debe permitir grabar las viviendas con sus principales características: 
número de cuartos, número de baños, Tamaño (En metros cuadrados), número de pisos,
y accesorios (Es un texto libre para ingresar las características: patio, jardín,
zonas comunes, etc).
Para el modelo solo debe considerar la agencia, el cliente, el tipo de vivienda (Casa,
apartamento, finca, etc), la vivienda y la venta. Considere que un cliente sólo compra 
una vivienda a la vez, es decir, puede comprar muchas viviendas en el tiempo, pero solo 
uno por "venta", y que como son viviendas nuevas, una vivivenda sólo se vende a un cliente.
No considere un modelo con tablas de referencia como el país, departamento, ciudad, 
urbanización, entre otras. Sólo considere las entidades sugeridas: agencia, cliente,
tipo de vivienda, vivienda y venta.


### Se solicita : ✏️ ✔️
- Debe crear una base de datos en SQL Server con el diagrama de datos que tenga las relaciones
entre las tablas.
- Elabore un servicio REST para Elaborar consultas (Al menos dos), el insert, update y delete
en la base de datos para el registro de las viviendas.
- Implemente las pruebas de todos los servicios desde Postman y grabe la información de ellas.

## Tablas 📑

### Vivienda 

| Name        | Type          | Settings                      | References                    | Note                           |
|-------------|---------------|-------------------------------|-------------------------------|--------------------------------|
| **codigo** | INT | 🔑 PK, not null , unique, autoincrement | fk_Vivienda _codigo_venta | |
| **numBanos** | INT | not null  |  | |
| **numCuartos** | INT| not null  |  | |
| **tamano** | INT | not null  |  | |
| **numPisos** | INT | not null  |  | |
| **accesorios** | TEXT(65535) | not null  |  | |
| **precio** | FLOAT | not null  |  | |
| **tipoVi** | INT | not null  |  | | 


### Agencia

| Name        | Type          | Settings                      | References                    | Note                           |
|-------------|---------------|-------------------------------|-------------------------------|--------------------------------|
| **codigo** | INT | 🔑 PK, not null , unique, autoincrement | fk_Agencia_codigo_venta | |
| **nombre** | VARCHAR(255) | not null  |  | |
| **sede** | VARCHAR(255) | not null  |  | | 


### cliente

| Name        | Type          | Settings                      | References                    | Note                           |
|-------------|---------------|-------------------------------|-------------------------------|--------------------------------|
| **codigo** | INT | 🔑 PK, not null , unique, autoincrement | fk_cliente_codigo_venta | |
| **nombre** | VARCHAR(255) | not null  |  | |
| **apellido** | VARCHAR(255) | not null  |  | |
| **telefono** | INT | not null  |  | |
| **correo** | VARCHAR(255) | not null  |  | | 


### tipoVivienda

| Name        | Type          | Settings                      | References                    | Note                           |
|-------------|---------------|-------------------------------|-------------------------------|--------------------------------|
| **codigo** | INT | 🔑 PK, not null , unique, autoincrement | fk_tipoVivienda_codigo_Vivienda  | |
| **nombre** | VARCHAR(255) | not null  |  | |
| **activo** | BOOLEAN | not null  |  | | 


### Venta

| Name        | Type          | Settings                      | References                    | Note                           |
|-------------|---------------|-------------------------------|-------------------------------|--------------------------------|
| **codigo** | INT | 🔑 PK, not null , unique, autoincrement |  | |
| **fechaCompra** | DATETIME | not null  |  | |
| **Valor** | FLOAT | not null  |  | |
| **cliente** | INT | not null  |  | |
| **agencia** | INT | not null  |  | |
| **vievienda** | INT | not null  |  | | 


## Relaciones

- **cliente to venta**: uno_a_muchos
- **tipoVivienda to Vivienda**: uno_a_muchos
- **Agencia to venta**: uno_a_muchos
- **Vivienda  to venta**: uno_a_uno
  
## Diagrama (Modelo Relacional)📎
![Modelo Relacional.png](https://github.com/ArsJohan/Examen_1_AppServWEB_Mi_18_20/blob/main/Modelo%20Relacional.png)


## Video: Explicativo y Desmostrativo (Postman) 🟠
[![Watch video](https://github.com/user-attachments/assets/653bce68-d004-47e9-bf64-015b9e4ecb20)
](https://youtu.be/Y6u0Cd0Lsw0)

## Contribuidores 🫂
<!-- readme: contributors -start -->
<table>
</table>
<!-- readme: contributors -end -->
