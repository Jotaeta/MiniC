using System;

namespace MiniC
{
    public static class Laboratorio1
    {
        public static void Parse_Program()
        {
            //Eliminar corridas con otro metodo
            var corridas = 0;
            var cantidad = 0;
            var bandera = false;
            while (!bandera)
            {
                if (Singleton.Instance.analizada != Singleton.Instance.Lista_palabras[Singleton.Instance.Lista_palabras.Count - 1])
                {
                    Parse_Decl();
                    cantidad++;
                }
                else
                {
                    bandera = true;
                }
                corridas++;
                if (corridas >= 15)
                {
                    bandera = true;
                }
            }
            if (cantidad == 0)
            {

                Singleton.Instance.hayError = true;

            }
        }

        public static void Parse_Decl()
        {
            if (Singleton.Instance.analizada == "int" || Singleton.Instance.analizada == "double" || Singleton.Instance.analizada == "bool" || Singleton.Instance.analizada == "string"
                || Singleton.Instance.analizada == "ident")
            {

                if (Singleton.Instance.Lista_palabras[Singleton.Instance.contador + 1] == ";" || Singleton.Instance.Lista_palabras[Singleton.Instance.contador + 2] == ";")
                {
                    Variable_DECL();
                }
                else if (Singleton.Instance.Lista_palabras[Singleton.Instance.contador + 1] == "(" || Singleton.Instance.Lista_palabras[Singleton.Instance.contador + 2] == "(")
                {
                    FunctionDecl();
                }
            }
            else if (Singleton.Instance.analizada == "void")
            {
                FunctionDecl();
            }
            else
            {
                Console.WriteLine("SYNTAX ERROR Posee un error de (;).");
            }
        }

        //Terminada
        public static void Variable_DECL()
        {
            Variable();
            Singleton.Instance.matchtoken(";");
        }

        //Terminada
        public static void Variable()
        {
            if (Singleton.Instance.analizada == "int" || Singleton.Instance.analizada == "double" || Singleton.Instance.analizada == "bool" || Singleton.Instance.analizada == "string" || Singleton.Instance.analizada == "ident")
            {
                Type();
                Singleton.Instance.matchtoken("ident");
            }
            else
            {
            Singleton.Instance.hayError = true;

            }
        }

        //Terminada
        public static void Type()
        {
            if (Singleton.Instance.analizada == "int")
            {
                Singleton.Instance.matchtoken("int");
                Ttype();
            }
            else if (Singleton.Instance.analizada == "double")
            {
                Singleton.Instance.matchtoken("double");
                Ttype();
            }
            else if (Singleton.Instance.analizada == "bool")
            {
                Singleton.Instance.matchtoken("bool");
                Ttype();
            }
            else if (Singleton.Instance.analizada == "string")
            {
                Singleton.Instance.matchtoken("string");
                Ttype();
            }
            else if (Singleton.Instance.analizada == "ident")
            {
                Singleton.Instance.matchtoken("ident");
                Ttype();
            }

            else
            {
            Singleton.Instance.hayError = true;

            }
        }

        //Terminada
        public static void Ttype()
        {
            if (Singleton.Instance.analizada == "[]")
            {
                Singleton.Instance.matchtoken("[]");
                Ttype();
            }
            else
            {

            }
        }

        //Terminada
        public static void FunctionDecl()
        {
            if (Singleton.Instance.analizada == "int" || Singleton.Instance.analizada == "double" || Singleton.Instance.analizada == "bool" || Singleton.Instance.analizada == "string" || Singleton.Instance.analizada == "ident")
            {
                Type();
                Singleton.Instance.matchtoken("ident");
                Singleton.Instance.matchtoken("(");
                Formals();
                Singleton.Instance.matchtoken(")");
                var cantidad = 0;
                var bandera = false;
                while (!bandera)
                {
                    if (Singleton.Instance.analizada == "if" || Singleton.Instance.analizada == "for" || Singleton.Instance.analizada == "return" || Singleton.Instance.analizada == "Print" || Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" || Singleton.Instance.analizada == "ident" || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)" || Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant" || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null")
                    {
                        Stmt();
                        cantidad++;
                    }
                    else
                    {
                        bandera = true;
                    }
                }
            }
            else if (Singleton.Instance.analizada == "void")
            {
                Singleton.Instance.matchtoken("void");
                Singleton.Instance.matchtoken("ident");
                Singleton.Instance.matchtoken("(");
                Formals();
                Singleton.Instance.matchtoken(")");
                var bandera = false;
                var cantidad = 0;
                while (!bandera)
                {
                    if (Singleton.Instance.analizada == "if" || Singleton.Instance.analizada == "for" || Singleton.Instance.analizada == "return" || Singleton.Instance.analizada == "Print" || Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" || Singleton.Instance.analizada == "ident" || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)" || Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant" || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null")
                    {
                        Stmt();
                        cantidad++;
                    }
                    else
                    {
                        bandera = true;
                    }
                }
            }
            else
            {
                Singleton.Instance.hayError = true;

            }
        }

        //Terminada
        public static void Stmt()
        {
            if (Singleton.Instance.analizada == "if")
            {
                IfStmt();
            }

            else if (Singleton.Instance.analizada == "for")
            {
                ForStmt();
            }

            else if (Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" || Singleton.Instance.analizada == "ident"
                 || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)" || Singleton.Instance.analizada == "intConstant"
                 || Singleton.Instance.analizada == "doubleConstant" || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant"
                 || Singleton.Instance.analizada == "null")
            {
                Expr();
                Singleton.Instance.matchtoken(";");
            }

            else if (Singleton.Instance.analizada == "return")
            {
                ReturnStmt();
            }

            else if (Singleton.Instance.analizada == "Print")
            {
                PrintStmt();
            }

            else
            {
                Singleton.Instance.hayError = true;

            }
        }

        //Terminada
        public static void Formals()
        {
            var cantidad = 0;
            var bandera = false;
            while (!bandera)
            {
                if (Singleton.Instance.analizada == "int" || Singleton.Instance.analizada == "double" || Singleton.Instance.analizada == "bool" || Singleton.Instance.analizada == "string" || Singleton.Instance.analizada == "ident")
                {
                    Variable();

                    cantidad++;
                }
                else
                {
                    bandera = true;
                }
            }
            if (cantidad == 0)
            {

            }
            else
            {
                Singleton.Instance.matchtoken(",");

            }
        }

        //Terminada
        public static void IfStmt()
        {
            Singleton.Instance.matchtoken("if");
            Singleton.Instance.matchtoken("(");
            Expr();
            Singleton.Instance.matchtoken(")");
            Stmt();

            if (Singleton.Instance.analizada == "(")
            {
                Singleton.Instance.matchtoken("(");
                Singleton.Instance.matchtoken("else");
                Stmt();
                Singleton.Instance.matchtoken(")");
            }
            else
            {

            }
        }

        //Terminada
        public static void ForStmt()
        {
            Singleton.Instance.matchtoken("for");
            Singleton.Instance.matchtoken("(");
            //Validar Expr
            if (Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" ||
                Singleton.Instance.analizada == "ident" || Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant"
                || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null"
                || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)")
            {
                Expr();
            }
            Singleton.Instance.matchtoken(";");
            Expr();
            Singleton.Instance.matchtoken(";");
            //Validar Expr
            //Validar Expr
            if (Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" ||
                Singleton.Instance.analizada == "ident" || Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant"
                || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null"
                || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)")
            {
                Expr();
            }
            Singleton.Instance.matchtoken(")");
            Stmt();
        }

        //Terminada
        public static void ReturnStmt()
        {
            Singleton.Instance.matchtoken("return");
            //Validar Expr
            if (Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" ||
                Singleton.Instance.analizada == "ident" || Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant"
                || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null"
                || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)")
            {
                Expr();
            }
            Singleton.Instance.matchtoken(";");
        }

        //Terminada
        public static void PrintStmt()
        {
            Singleton.Instance.matchtoken("Print");
            Singleton.Instance.matchtoken("(");
            var cantidad = 0;
            var bandera = false;
            while (!bandera)
            {
                //Validar Expr
                if (Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" ||
                    Singleton.Instance.analizada == "ident" || Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant"
                    || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null"
                    || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)")
                {
                    Expr();
                    cantidad++;
                }
                else
                {
                    bandera = true;
                }
            }
            if (cantidad <= 1)
            {
                Singleton.Instance.matchtoken(",");
                Singleton.Instance.matchtoken(")");
                Singleton.Instance.matchtoken(";");
            }
            else
            {

                Singleton.Instance.hayError = true;

            }
        }

        //Terminada
        public static void Expr()
        {
            ExprPrima();
            ExprBiprima();

        }
        //EXPR PRIMA 
        public static void ExprPrima()
        {

            if (Singleton.Instance.analizada == "-")
            {
                Singleton.Instance.matchtoken("-");
                Expr();
            }

            else if (Singleton.Instance.analizada == "!")
            {
                Singleton.Instance.matchtoken("!");
                Expr();
            }

            else if (Singleton.Instance.analizada == "New(ident)")
            {
                Singleton.Instance.matchtoken("New(ident)");
            }

            else if (Singleton.Instance.analizada == "ident")
            {

                Parse_LValue();
                if (Singleton.Instance.analizada == "=")
                {
                    Singleton.Instance.matchtoken("=");
                    Expr();
                }
                else
                {

                }

            }

            else if (Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant"
                || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null")
            {
                Parse_Constant();
            }

            else if (Singleton.Instance.analizada == "this")
            {
                Singleton.Instance.matchtoken("this");
            }

            else if (Singleton.Instance.analizada == "(")
            {
                Singleton.Instance.matchtoken("(");
                Expr();
                Singleton.Instance.matchtoken(")");
            }

            else
            {

                Singleton.Instance.hayError = true;
            }
        }

        //EXPR BIPRIMA 
        public static void ExprBiprima()
        {

            if (Singleton.Instance.analizada == "+")
            {
                Singleton.Instance.matchtoken("+");
                Expr();
            }
            else if (Singleton.Instance.analizada == "-")
            {
                Singleton.Instance.matchtoken("-");
                Expr();
            }
            else if (Singleton.Instance.analizada == "*")
            {
                Singleton.Instance.matchtoken("*");
                Expr();
            }
            else if (Singleton.Instance.analizada == "/")
            {
                Singleton.Instance.matchtoken("/");
                Expr();
            }
            else if (Singleton.Instance.analizada == "%")
            {
                Singleton.Instance.matchtoken("%");
                Expr();
            }
            else if (Singleton.Instance.analizada == "<")
            {
                Singleton.Instance.matchtoken("<");
                Expr();

            }
            else if (Singleton.Instance.analizada == "<=")
            {
                Singleton.Instance.matchtoken("<=");
                Expr();
            }
            else if (Singleton.Instance.analizada == ">")
            {
                Singleton.Instance.matchtoken(">");
                Expr();
            }
            else if (Singleton.Instance.analizada == ">=")
            {
                Singleton.Instance.matchtoken(">=");
                Expr();
            }
            else if (Singleton.Instance.analizada == "==")
            {
                Singleton.Instance.matchtoken("==");
                Expr();
            }
            else if (Singleton.Instance.analizada == "!=")
            {
                Singleton.Instance.matchtoken("!=");
                Expr();
            }
            else if (Singleton.Instance.analizada == "&&")
            {
                Singleton.Instance.matchtoken("&&");
                Expr();
            }
            else if (Singleton.Instance.analizada == ".")
            {
                Singleton.Instance.matchtoken(".");
                Singleton.Instance.matchtoken("ident");
            }
            else if (Singleton.Instance.analizada == "[")
            {
                Singleton.Instance.matchtoken("[");
                Expr();
                Singleton.Instance.matchtoken("]");
            }
            else
            {

            }

        }

        //Terminada
        public static void Parse_LValue()
        {
            if (Singleton.Instance.analizada == "ident")
            {
                Singleton.Instance.matchtoken("ident");
            }
            else
            {
                Singleton.Instance.hayError = true;

            }
        }

        //Terminada
        public static void Parse_Constant()
        {
            if (Singleton.Instance.analizada == "intConstant")
            {
                Singleton.Instance.matchtoken("intConstant");
            }
            else if (Singleton.Instance.analizada == "doubleConstant")
            {
                Singleton.Instance.matchtoken("doubleConstant");
            }
            else if (Singleton.Instance.analizada == "boolConstant")
            {
                Singleton.Instance.matchtoken("boolConstant");
            }
            else if (Singleton.Instance.analizada == "stringConstant")
            {
                Singleton.Instance.matchtoken("stringConstant");
            }
            else if (Singleton.Instance.analizada == "null")
            {
                Singleton.Instance.matchtoken("null");
            }
            else
            {
                Singleton.Instance.hayError = true;

            }
        }

    }
}
