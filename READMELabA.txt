Laboratorio A Analizador Sintactico Descendente Recursivo
-Errores
Los errores de esta fase se manejaron en el metodo matchtoken(string analizada), esto siguiendo una gramatica con sus reglas de producciones (Gramatica mas adelante). 
En el metodo matchtoken, se hizo una comparacion entre el token que estaba siendo analizado actualmente y el token que se supone que el programa deberia de recibir, al 
momento de que estos dos tokens no cuadren, el programa marca un error y presenta el token recibido y el token esperado y el numero de linea. Si los errores son muy 
ambiguos, el programa solamente presenta distintos mensajes de SYNTAX ERROR.
-Gramatica
program -> Decl+

Decl -> VariableDecl Decl'| FunctionDecl Decl'
Decl'->	Decl Decl' | eps

--VariableDecl -> Variable ;

--Variable -> Type ident

--Type ->  int Type' | double Type' | bool Type' 
	| string Type' | ident Type'

--Type' -> [] Type' | eps

--FunctionDecl -> Type ident ( Formals ) Stmt* 
		| void ident ( Formals ) Stmt*

--Stmt -> IfStmt | ForStmt | Expr ; | ReturnStmt | PrintStmt 

--Formals -> Variable+ , | eps

--IfStmt -> if ( Expr ) Stmt (else Stmt)?

--ForStmt -> for ( Expr? ; Expr ; Expr? ) Stmt

--ReturnStmt -> return Expr? ;

--PrintStmt -> Print ( Expr+ , ) ;

Expr -> Expr' Expr''
Expr' -> - Expr | ! Expr | 
	New(ident)  | LValue = Expr  | 
	Constant  | LValue | this |
	( Expr )

Expr''->  + Expr |  - Expr |  * Expr |
	 / Expr | % Expr |  < Expr |
	 <= Expr |  > Expr |  >= Expr | 
	 == Expr |  != Expr |  && Expr | 
	 . ident |  [ Expr ] | eps

--LValue -> ident 

--Constant -> intConstant | doubleConstant | boolConstant 
		| stringConstant | null
