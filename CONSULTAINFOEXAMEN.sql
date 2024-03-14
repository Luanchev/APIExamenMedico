-- CONSULTA QUE NOS MUESTRA CON EL NUMERO DE DOCUMENTO DEL PACIENTE LA INFORMACION DEL EXAMEN QUE SE HIZO, 
-- CON LA INFORMACION DEL TIPO DE MUESTRA, Y LA INFORMACION IMPORTANTE DEL PACIENTE

SELECT 
	E.IDEXAMEN,
	E.CODIGO,
	E.NOMBRE,
	E.PRECIO,
	E.IDMUESTRAFK,
	TP.IDMUESTRA,
	TP.TIPOMUESTRA,
	E.idpacientefk,
	p.idpaciente,
	P.NUMDOC,
	P.NOMBRE,
	P.APELLIDO,
	P.EDAD
	
FROM EXAMENMEDICO AS E
JOIN tipomuestra AS TP ON E.IDMUESTRAFK = TP.idmuestra
JOIN paciente  AS P ON E.idpacientefk = P.idpaciente
WHERE numdoc = '17686631E';