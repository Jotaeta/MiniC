using System;

namespace MiniC
{
    class Laboratorio1
    {
        public void Parse_Program()
        {
            //Validar Decl
            var cantidad = 0;
            var bandera = false;
            while (!bandera)
            {
                if (Singleton.Instance.analizada != null)
                {
                    Parse_Decl();
                    cantidad++;
                }
                else
                {
                    bandera = true;
                }
            }
            if (cantidad == 0)
            {
                Console.WriteLine("Syntax Error");
            }
        }

        public void Parse_Decl()
        {
            if (Singleton.Instance.analizada == "int" || Singleton.Instance.analizada == "double" || Singleton.Instance.analizada == "bool" || Singleton.Instance.analizada == "string"
                || Singleton.Instance.analizada == "ident")
            {
                // Crear una validacion para estas
                if (Singleton.Instance.analizada == "[]")
                {
                    Variable_DECL();
                    FunctionDecl();
                }
                if (Singleton.Instance.analizada == "ident")
                {

                }

            }
            else if (Singleton.Instance.analizada == "void")
            {
                FunctionDecl();
            }
            else
            {
                Parse_Decl();
            }
        }

        //Terminada
        public void Variable_DECL()
        {
            Variable();
            Singleton.Instance.matchtoken(";");
        }

        //Terminada
        public void Variable()
        {
            if (Singleton.Instance.analizada == "int" || Singleton.Instance.analizada == "double" || Singleton.Instance.analizada == "bool" || Singleton.Instance.analizada == "string" || Singleton.Instance.analizada == "ident")
            {
                Type();
                Singleton.Instance.matchtoken("ident");
            }
            else
            {
                Console.WriteLine("SYSTAX ERROR");
            }
        }

        //Terminada
        public void Type()
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
                Console.WriteLine("SYSTAX ERROR");
            }
        }

        //Terminada
        public void Ttype()
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
        public void FunctionDecl()
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
                if (cantidad == 0)
                {
                    Console.WriteLine("Syntax Error");
                }
            }
            else if (Singleton.Instance.analizada == "void")
            {
                Singleton.Instance.matchtoken("void");
                Singleton.Instance.matchtoken("ident");
                Singleton.Instance.matchtoken("(");
                Formals();
                Singleton.Instance.matchtoken(")");
                var cantidad = 0;
                while (true)
                {
                    if (Singleton.Instance.analizada == "if" || Singleton.Instance.analizada == "for" || Singleton.Instance.analizada == "return" || Singleton.Instance.analizada == "Print" || Singleton.Instance.analizada == "!" || Singleton.Instance.analizada == "-" || Singleton.Instance.analizada == "(" || Singleton.Instance.analizada == "ident" || Singleton.Instance.analizada == "this" || Singleton.Instance.analizada == "New(ident)" || Singleton.Instance.analizada == "intConstant" || Singleton.Instance.analizada == "doubleConstant" || Singleton.Instance.analizada == "boolConstant" || Singleton.Instance.analizada == "stringConstant" || Singleton.Instance.analizada == "null")
                    {
                        Stmt();
                        cantidad++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (cantidad == 0)
                {
                    Console.WriteLine("Syntax Error");
                }
            }
            else
            {
                Console.WriteLine("SYNTAX ERROR");
            }
        }

        //Terminada
        public void Stmt()
        {
            if (Singleton.Instance.analizada == "if")
            {
                IfStmt();
            }

            else if (Singleton.Instance.analizada == "for")
            {
                IfStmt();
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
                Console.WriteLine("SYSTAX ERROR");
            }
        }

        //Terminada
        public void Formals()
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
            if (cantidad <= 1)
            {
                Singleton.Instance.matchtoken(",");
            }
            else
            {
                Console.WriteLine("Syntax Error");
            }
        }

        //Terminada
        public void IfStmt()
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
        public void ForStmt()
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
        public void ReturnStmt()
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
        public void PrintStmt()
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
                Console.WriteLine("Syntax Error");
            }
        }

        //Terminada
        public void Expr()
        {
            ExprPrima();
            ExprBiprima();

        }
        //EXPR PRIMA 
        public void ExprPrima()
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
                Console.WriteLine("SYNTAX ERROR");
            }
        }

        //EXPR BIPRIMA 
        public void ExprBiprima()
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
                Console.WriteLine("SYNTAX ERROR");
            }

        }

        //Terminada
        public void Parse_EExp()
        {
            if (Singleton.Instance.analizada == "||")
            {
                Singleton.Instance.matchtoken("||");
                Parse_EExp();
            }
            else { }
        }

        //Terminada
        public void Parse_LValue()
        {
            if (Singleton.Instance.analizada == "ident")
            {
                Singleton.Instance.matchtoken("ident");
            }
            else
            {
                Console.WriteLine("SYSTAX ERROR");
            }
        }

        //Terminada
        public void Parse_Constant()
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
                Console.WriteLine("SYSTAX ERROR");
            }
        }

    }
}
