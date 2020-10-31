# MiniC - Fase I - Análisis Léxico
Jonathan Argueta #1029418 Allan Dávila #1160118

Nuestro proyecto Minic es un analizador léxico el cual cumple con las instrucciones requeridas y puntos a observar, donde de manera eficaz y eficiente logramos que el análizador léxico complete lo solicitado se hace basandose en conceptos dados por la Ingeniera los cuales es la utilización de la libreria System.Text.RegularExpressions para la construccion de Expresiones Regulares las cuales con necesaria para ver si nuestro programa cumple con las funciones de:

• Reconocer todas los tokens los cuales se dividen en:

  •Simbolos Permitidos: Los cuales son todos aquellos caracteres especificos que por medio de una descisión propia son incluidos para analizar o no.
  
  •Palabras Reservadas: Son todas las palabras que no se pueden utilizar para otra función que no sea la que ya esta predefinida por el programa, es decir no se pueden 
  utilizar para nombrar variables o metodos.
  
  •Identificadores: Son cualquier combinación de carácteres que no sean palabras reservadas y se ecuentren en símbolos permitidos en este caso seran la combinación de 
  (letras,numeros y guión bajo) 
  
  •Simbolos de Puntuación
  
  •Operadores: Serían todos los conectores lógicos (+,-,/,|,etc)
  
  •Booleanos: True, False
  
  •Constantes (int, double, bool, string)
  
  •Errores: El manejo de errores es la parte más fundamental para este proyecto, porque con ellos podemos analizar el contexto de lo que queremos validar. Para estos mismos en 
  el proyecto se hace un escaneo completo del programa donde con las expresiones regulares y su función .IsMatch() es de implementación esencial ya que esto nos permite hacer una validación mas eficiente de las cadenas a analizar.
  
  
El proyecto trabaja de la siguiente forma, le pide al usuario que ingrese una ruta la cual seria el archivo que se va analizar, una vez el usuario realiza este proceso, el analizador empieza a trabajar de la siguiente manera: El programa lee el archivo y lo recorre en bloque, luego empieza analizar las palabras encontradas en el archivo a evaluar y comienza un proceso interno, donde con la libreria regex junto con las expresiones regulares diseñadas por nosotros inician la clasificación de los tokens que fueron descritos anteriormente, luego cuando una expresión regular no hace match procedemos a manejar errores donde pueden estar los siguientes:

•Token no valido: al analizar la palabra y obtener un resultado con la función regex y que este no haga match con los simbolos permitidos, este pasara a una inspección más detallada para asi determinar cual es error dado y el símbolo entrante que no es permitido. Por lo que se separa la palabra del símbolo no permitido en 2 tokens y así clasificar independientemente el símbolo no permito, y el otro token, e ingresarlo a la lista tokens y el error se mostrara en pantalla indicando que el error es un símbolo no permitido.

•Falta cierre de comillas: al analizar la linea que con ayuda de un regex se sabe que contiene una cadena de string, se pasara a separar los tokens antes de las comillas y luego de las comillas, y si en dado caso no contiene cierre de comillas el programa lo maneja como una cadena de string sin cierre y lo reporta en el archivo al igual que en la pantalla indicando el tipo de error.

•Cierre de comentarios:al analizar la linea que con ayuda de un regex se sabe que contiene un comentario, se pasara a separar los tokens antes de los indicadores /*, el programa incia una busqueda para encontrar el cierre de los identificadores */, si en dado caso no contiene cierre de comentario el programa lo maneja como un comentario sin cierre y lo reporta en el archivo como no cierra comentario(Llega a un EOF) al igual que en la pantalla indicando el tipo de error.

•Comentario no aperturado:al analizar la palabra y obtener un resultado con la función regex y que este no haga match con el token */, este sera comentado como comentario no aperturado se guarda en el archivo indicando el error y se muestra en pantalla de igual manera.

•Identificadores no sean mayor a 31 char: al encontrar un indentificador con la función regex este entra a una validación donde se valida la longitud de dicho identificador, si el tamaño del identificador es mayor a 31 automaticamente se obtienen los primeros 31 carácteres mostrandose en el archivo.

Luego todo esto se guarda en una lista <string> que se llama ListaTokens para que esta pueda ser utilizada en el proceso continuo que es terminar el archivo, y generar la escritura un archivo con la extensión .out donde se mostrara token por token y errores, donde se indica el tipo de token, número de linea, número de columna. Finalizando con este proceso se le pedira al Usuario que brinde una ruta para guardar el archivo resultante.  

# MiniC - Fase II 

#Gramatica 


PROGRAM -> DECLA

DECLA -> DECL DECLA

DECLA -> DECL 

DECL -> VARIABLEDECL

DECL -> FUNCTIONDECL

DECL -> CONSTDECL

DECL -> CLASSDECL

DECL -> INTERFACEDECL

VARIABLEDECL -> VARIABLE ;

VARIABLE -> TYPE ident 

CONSTDECL -> const CONSTTYPE ident ;

CONSTTYPE -> int

CONSTTYPE -> double

CONSTTYPE -> bool

CONSTTYPE -> string 

TYPE -> int TTYPE

TYPE -> double TTYPE

TYPE -> bool TTYPE

TYPE -> string TTYPE

TYPE -> ident TTYPE

TTYPE-> [] TTYPE

TTYPE -> ''

FUNCTIONDECL -> TYPE ident ( FORMALS ) STMTBLOCK

FUNCTIONDECL -> void ident ( FORMALS ) STMTBLOCK

FORMALS -> VARIABLE , FORMALS

FORMALS -> VARIABLE

CLASSDECL -> class ident PIDENT CIDENT {​​​​​​ FFIELD }​​​​​​

PIDENT -> : ident

PIDENT-> ''

CIDENT -> , ident CCIDENT ,

CCIDENT -> ident

CCIDENT -> ''

FFIELD -> FIELD FFIELD

FFIELD -> ''

FIELD -> VARIABLEDECL

FIELD -> FUNCTIONDECL

FIELD -> CONSTDECL

INTERFACEDECL -> interface ident {​​​​​​ PPROTOTYPE }​​​​​​

PPROTOTYPE -> PROTOTYPE PPROTOTYPE

PPROTOTYPE -> ''

PROTOTYPE -> TYPE ident ( FORMALS ) ;

PROTOTYPE -> void ident ( FORMALS ) ;

STMTBLOCK -> {​​​​​​ VDEC CDEC SSTMT }​​​​​​

VDEC-> VARIABLEDECL VDEC

VDEC-> ''


CDEC-> CONSTDECL CDEC

CDEC-> ''

SSTMT-> STMT SSTMT

SSTMT-> ''

STMT -> IFSTMT

STMT -> WHILESTMT

STMT -> FORSTMT

STMT -> BREAKSTMT

STMT -> RETURNSTMT

STMT -> PRINTSTMT

STMT -> STMTBLOCK

STMT -> EXPRI ;

EXPRI -> EXPR

EXPRI -> ''

EEXPR -> EXPR EEXPR

EEXPR -> ''

IFSTMT -> if ( EXPR ) STMT ELSE

ELSE -> else STMT

ELSE -> ''

WHILESTMT -> while ( EXPR ) STMT

FORSTMT -> for ( EXPR ; EXPR ; EXPR ) STMT

RETURNSTMT -> return EXPR ;

BREAKSTMT -> break ;

PRINTSTMT -> Console.Writeline ( EXPR EEXPR , ) ;

EXPR -> EXPRM EXPR'

EXPR' -> && EXPR'

EXPRM -> EXPRN EXPRM'

EXPRM' -> == EXPRM'

EXPRN -> EXPRO EXPRN'

EXPRN' -> < EXPRN'

EXPRN' -> <= EXPRN'

EXPRO -> EXPRP EXPRO'

EXPRO' -> + EXPRO'

EXPRP -> EXPRQ EXPRP'

EXPRP' -> * EXPRP' 

EXPRP' -> % EXPRP'

EXPRQ -> ! EXPR

EXPRQ ->  - EXPR

EXPRQ -> LVALUE = EXPR

EXPRQ -> CONSTANT

EXPRQ -> LVALUE

EXPRQ -> this

EXPRQ -> ( EXPR )

EXPRQ -> New ( ident )

LVALUE -> ident

LVALUE -> EXPR . ident

CONSTANT -> intConstant

CONSTANT -> doubleConstant

CONSTANT -> boolConstant

CONSTANT -> stringConstant

CONSTANT -> null
